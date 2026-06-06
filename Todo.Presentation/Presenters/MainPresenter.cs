namespace Todo.Presentation.Presenters;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Todo.Application.Categories.Queries.ListCategories;
using Todo.Application.TodoItems.Commands.ChangeTodoStatus;
using Todo.Application.TodoItems.Commands.DeleteTodoItem;
using Todo.Application.TodoItems.Queries.ListTodoItems;
using Todo.Presentation.Models;
using Todo.Presentation.Views.Contracts;

public class MainPresenter
{
    private readonly IServiceScopeFactory _scopeFactory;
    private IMainView? _view;
    private Dictionary<Guid, string> _categoryNames = new();

    public MainPresenter(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Attach(IMainView view)
    {
        _view = view;
        _view.ViewShown += OnViewShown;
        _view.RefreshRequested += OnRefreshRequested;
        _view.FilterChanged += OnFilterChanged;
        _view.AddRequested += OnAddRequested;
        _view.EditRequested += OnEditRequested;
        _view.DeleteRequested += OnDeleteRequested;
        _view.ChangeStatusRequested += OnChangeStatusRequested;
    }

    public void Detach()
    {
        if (_view is null)
        {
            return;
        }

        _view.ViewShown -= OnViewShown;
        _view.RefreshRequested -= OnRefreshRequested;
        _view.FilterChanged -= OnFilterChanged;
        _view.AddRequested -= OnAddRequested;
        _view.EditRequested -= OnEditRequested;
        _view.DeleteRequested -= OnDeleteRequested;
        _view.ChangeStatusRequested -= OnChangeStatusRequested;
        _view = null;
    }

    private async void OnViewShown(object? sender, EventArgs e)
    {
        await LoadCategoriesAsync();
        await RefreshTodoItemsAsync();
    }

    private async void OnRefreshRequested(object? sender, EventArgs e) =>
        await RefreshTodoItemsAsync();

    private async void OnFilterChanged(object? sender, EventArgs e) =>
        await RefreshTodoItemsAsync();

    private async void OnAddRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        if (_view.ShowCreateDialog())
        {
            await RefreshTodoItemsAsync();
        }
    }

    private async void OnEditRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        var selected = _view.GetSelectedTodoItem();
        if (selected is null)
        {
            _view.ShowInfo("Выберите задачу для редактирования.");
            return;
        }

        if (_view.ShowEditDialog(selected.Id))
        {
            await RefreshTodoItemsAsync();
        }
    }

    private async void OnDeleteRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        var selected = _view.GetSelectedTodoItem();
        if (selected is null)
        {
            _view.ShowInfo("Выберите задачу для удаления.");
            return;
        }

        if (!_view.ShowConfirmation($"Удалить задачу «{selected.Title}»?"))
        {
            return;
        }

        await RunWithBusyAsync(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var result = await mediator.Send(new DeleteTodoItemCommand(selected.Id));

            if (result.IsError)
            {
                _view.ShowErrors(result.Errors);
                return;
            }

            await RefreshTodoItemsAsync();
        });
    }

    private async void OnChangeStatusRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        var selected = _view.GetSelectedTodoItem();
        if (selected is null)
        {
            _view.ShowInfo("Выберите задачу для смены статуса.");
            return;
        }

        var newStatus = _view.ShowStatusChangeDialog();
        if (newStatus is null)
        {
            return;
        }

        await RunWithBusyAsync(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var result = await mediator.Send(new ChangeTodoStatusCommand(selected.Id, newStatus.Value));

            if (result.IsError)
            {
                _view.ShowErrors(result.Errors);
                return;
            }

            await RefreshTodoItemsAsync();
        });
    }

    private async Task LoadCategoriesAsync()
    {
        if (_view is null)
        {
            return;
        }

        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var result = await mediator.Send(new ListCategoriesQuery());

        if (result.IsError)
        {
            _view.ShowErrors(result.Errors);
            return;
        }

        _categoryNames = result.Value.ToDictionary(c => c.Id, c => c.Name);

        var filterItems = new List<CategoryFilterItem> { new(null, "Все категории") };
        filterItems.AddRange(result.Value.Select(c => new CategoryFilterItem(c.Id, c.Name)));

        _view.BindCategories(filterItems);
    }

    private async Task RefreshTodoItemsAsync()
    {
        if (_view is null)
        {
            return;
        }

        await RunWithBusyAsync(async () =>
        {
            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var filters = _view.GetFilterState();
            var result = await mediator.Send(new ListTodoItemsQuery(filters.CategoryId, filters.Status));

            if (result.IsError)
            {
                _view.ShowErrors(result.Errors);
                return;
            }

            var rows = result.Value
                .Select(item => TodoItemRow.From(
                    item,
                    _categoryNames.TryGetValue(item.CategoryId, out var name) ? name : "—"))
                .ToList();

            _view.BindTodoItems(rows);
        });
    }

    private async Task RunWithBusyAsync(Func<Task> action)
    {
        if (_view is null)
        {
            return;
        }

        try
        {
            _view.SetBusy(true);
            await action();
        }
        finally
        {
            _view.SetBusy(false);
        }
    }
}
