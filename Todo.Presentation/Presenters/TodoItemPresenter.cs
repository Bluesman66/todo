namespace Todo.Presentation.Presenters;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

using Todo.Application.Categories.Queries.ListCategories;
using Todo.Application.TodoItems.Commands.ChangeTodoStatus;
using Todo.Application.TodoItems.Commands.CreateTodoItem;
using Todo.Application.TodoItems.Commands.UpdateTodoItem;
using Todo.Application.TodoItems.Queries.GetTodoItemById;
using Todo.Domain.TodoItems;
using Todo.Presentation.Models;
using Todo.Presentation.Views.Contracts;

public class TodoItemPresenter
{
    private readonly IServiceScopeFactory _scopeFactory;
    private ITodoItemView? _view;
    private Guid? _editId;

    public TodoItemPresenter(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void Attach(ITodoItemView view)
    {
        _view = view;
        _view.ViewShown += OnViewShown;
        _view.SaveRequested += OnSaveRequested;
    }

    public void Detach()
    {
        if (_view is null)
        {
            return;
        }

        _view.ViewShown -= OnViewShown;
        _view.SaveRequested -= OnSaveRequested;
        _view = null;
    }

    public void PrepareCreate()
    {
        _editId = null;
        _view?.SetWindowTitle("Новая задача");
        _view?.SetStatusEnabled(false);
    }

    public void PrepareEdit(Guid id)
    {
        _editId = id;
        _view?.SetWindowTitle("Редактирование задачи");
        _view?.SetStatusEnabled(true);
    }

    private async void OnViewShown(object? sender, EventArgs e)
    {
        await LoadCategoriesAsync();

        if (_editId.HasValue)
        {
            await LoadTodoItemAsync(_editId.Value);
        }
    }

    private async void OnSaveRequested(object? sender, EventArgs e)
    {
        if (_view is null)
        {
            return;
        }

        var formData = _view.GetFormData();

        if (string.IsNullOrWhiteSpace(formData.Title))
        {
            _view.ShowValidationError("Введите название задачи.");
            return;
        }

        if (formData.CategoryId == Guid.Empty)
        {
            _view.ShowValidationError("Выберите категорию.");
            return;
        }

        try
        {
            _view.SetBusy(true);

            using var scope = _scopeFactory.CreateScope();
            var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

            var title = formData.Title.Trim();
            var description = string.IsNullOrWhiteSpace(formData.Description)
                ? null
                : formData.Description.Trim();
            var dueDate = formData.HasDueDate ? formData.DueDate : null;

            if (_editId is null)
            {
                var createResult = await mediator.Send(new CreateTodoItemCommand(
                    title,
                    formData.CategoryId,
                    formData.Priority,
                    dueDate,
                    description));

                if (createResult.IsError)
                {
                    _view.ShowErrors(createResult.Errors);
                    return;
                }
            }
            else
            {
                var updateResult = await mediator.Send(new UpdateTodoItemCommand(
                    _editId.Value,
                    title,
                    formData.CategoryId,
                    formData.Priority,
                    dueDate,
                    description));

                if (updateResult.IsError)
                {
                    _view.ShowErrors(updateResult.Errors);
                    return;
                }

                if (formData.Status != updateResult.Value.Status)
                {
                    var statusResult = await mediator.Send(
                        new ChangeTodoStatusCommand(_editId.Value, formData.Status));

                    if (statusResult.IsError)
                    {
                        _view.ShowErrors(statusResult.Errors);
                        return;
                    }
                }
            }

            _view.CloseWithSuccess();
        }
        finally
        {
            _view.SetBusy(false);
        }
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

        var options = result.Value.Select(c => new CategoryOption(c.Id, c.Name)).ToList();
        _view.BindCategories(options);
    }

    private async Task LoadTodoItemAsync(Guid id)
    {
        if (_view is null)
        {
            return;
        }

        using var scope = _scopeFactory.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<ISender>();

        var result = await mediator.Send(new GetTodoItemByIdQuery(id));

        if (result.IsError)
        {
            _view.ShowErrors(result.Errors);
            _view.CloseWithCancel();
            return;
        }

        var item = result.Value;
        _view.BindFormData(new TodoItemFormModel
        {
            Title = item.Title,
            Description = item.Description ?? string.Empty,
            CategoryId = item.CategoryId,
            Priority = item.Priority,
            Status = item.Status,
            DueDate = item.DueDate,
            HasDueDate = item.DueDate.HasValue
        });
    }
}
