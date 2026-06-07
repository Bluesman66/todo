namespace Todo.Presentation.Views;

using ErrorOr;

using Microsoft.Extensions.DependencyInjection;

using Todo.Domain.TodoItems;
using Todo.Presentation.Common;
using Todo.Presentation.Models;
using Todo.Presentation.Presenters;
using Todo.Presentation.Views.Contracts;

public partial class MainForm : Form, IMainView
{
    private readonly MainPresenter _presenter;
    private readonly IServiceScopeFactory _scopeFactory;
    private bool _suppressFilterEvents;

    public event EventHandler? ViewShown;
    public event EventHandler? RefreshRequested;
    public event EventHandler? FilterChanged;
    public event EventHandler? AddRequested;
    public event EventHandler? EditRequested;
    public event EventHandler? DeleteRequested;
    public event EventHandler? ChangeStatusRequested;

    public MainForm(MainPresenter presenter, IServiceScopeFactory scopeFactory)
    {
        _presenter = presenter;
        _scopeFactory = scopeFactory;
        InitializeComponent();
        InitializeFilters();
        _presenter.Attach(this);
        FormClosed += (_, _) => _presenter.Detach();
    }

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        ViewShown?.Invoke(this, EventArgs.Empty);
    }

    private void InitializeFilters()
    {
        statusFilterComboBox.Items.Add("Все статусы");
        statusFilterComboBox.Items.Add("Ожидает");
        statusFilterComboBox.Items.Add("В работе");
        statusFilterComboBox.Items.Add("Выполнена");
        statusFilterComboBox.Items.Add("Отменена");
        SetFilterIndexSilently(statusFilterComboBox, 0);

        categoryFilterComboBox.DisplayMember = "Name";
    }

    public MainViewFilterState GetFilterState()
    {
        Guid? categoryId = null;
        if (categoryFilterComboBox.SelectedItem is CategoryFilterItem categoryFilter && categoryFilter.Id.HasValue)
        {
            categoryId = categoryFilter.Id;
        }

        TodoItemStatus? status = statusFilterComboBox.SelectedIndex switch
        {
            1 => TodoItemStatus.Pending,
            2 => TodoItemStatus.InProgress,
            3 => TodoItemStatus.Completed,
            4 => TodoItemStatus.Cancelled,
            _ => null
        };

        return new MainViewFilterState(categoryId, status);
    }

    public TodoItemRow? GetSelectedTodoItem()
    {
        if (todoItemsGrid.CurrentRow?.DataBoundItem is TodoItemRow row)
        {
            return row;
        }

        return null;
    }

    public void BindCategories(IReadOnlyList<CategoryFilterItem> categories)
    {
        categoryFilterComboBox.Items.Clear();
        foreach (var category in categories)
        {
            categoryFilterComboBox.Items.Add(category);
        }

        SetFilterIndexSilently(categoryFilterComboBox, 0);
    }

    public void BindTodoItems(IReadOnlyList<TodoItemRow> items)
    {
        todoItemsGrid.DataSource = null;
        todoItemsGrid.DataSource = items.ToList();
    }

    public void SetBusy(bool isBusy)
    {
        UseWaitCursor = isBusy;
        SetButtonsEnabled(!isBusy);
    }

    public void ShowInfo(string message) =>
        MessageBox.Show(message, "Информация", MessageBoxButtons.OK, MessageBoxIcon.Information);

    public bool ShowConfirmation(string message) =>
        UiResults.ShowConfirmation(message);

    public void ShowErrors(IReadOnlyList<Error> errors) =>
        UiResults.ShowErrors(errors);

    public bool ShowCreateDialog()
    {
        using var scope = _scopeFactory.CreateScope();
        var form = scope.ServiceProvider.GetRequiredService<TodoItemForm>();
        form.PrepareCreate();
        return form.ShowDialog(this) == DialogResult.OK;
    }

    public bool ShowEditDialog(Guid id)
    {
        using var scope = _scopeFactory.CreateScope();
        var form = scope.ServiceProvider.GetRequiredService<TodoItemForm>();
        form.PrepareEdit(id);
        return form.ShowDialog(this) == DialogResult.OK;
    }

    public TodoItemStatus? ShowStatusChangeDialog()
    {
        using var scope = _scopeFactory.CreateScope();
        var dialog = scope.ServiceProvider.GetRequiredService<StatusChangeForm>();
        return dialog.ShowDialog(this) == DialogResult.OK ? dialog.SelectedStatus : null;
    }

    private void RefreshButton_Click(object sender, EventArgs e) =>
        RefreshRequested?.Invoke(this, EventArgs.Empty);

    private void FilterChangedHandler(object? sender, EventArgs e)
    {
        if (_suppressFilterEvents)
        {
            return;
        }

        FilterChanged?.Invoke(this, EventArgs.Empty);
    }

    private void AddButton_Click(object sender, EventArgs e) =>
        AddRequested?.Invoke(this, EventArgs.Empty);

    private void EditButton_Click(object sender, EventArgs e) =>
        EditRequested?.Invoke(this, EventArgs.Empty);

    private void DeleteButton_Click(object sender, EventArgs e) =>
        DeleteRequested?.Invoke(this, EventArgs.Empty);

    private void ChangeStatusButton_Click(object sender, EventArgs e) =>
        ChangeStatusRequested?.Invoke(this, EventArgs.Empty);

    private void TodoItemsGrid_CellMouseDown(object? sender, DataGridViewCellMouseEventArgs e)
    {
        if (e.Button != MouseButtons.Right || e.RowIndex < 0)
        {
            return;
        }

        todoItemsGrid.ClearSelection();
        todoItemsGrid.Rows[e.RowIndex].Selected = true;
        todoItemsGrid.CurrentCell = todoItemsGrid.Rows[e.RowIndex].Cells[Math.Max(e.ColumnIndex, 0)];
    }

    private void TodoItemsContextMenu_Opening(object? sender, System.ComponentModel.CancelEventArgs e)
    {
        var hasSelection = GetSelectedTodoItem() is not null;
        var actionsEnabled = editButton.Enabled;

        editMenuItem.Enabled = hasSelection && actionsEnabled;
        deleteMenuItem.Enabled = hasSelection && actionsEnabled;
        changeStatusMenuItem.Enabled = hasSelection && actionsEnabled;
    }

    private void SetFilterIndexSilently(ComboBox comboBox, int index)
    {
        _suppressFilterEvents = true;
        try
        {
            comboBox.SelectedIndex = index;
        }
        finally
        {
            _suppressFilterEvents = false;
        }
    }

    private void SetButtonsEnabled(bool enabled)
    {
        addButton.Enabled = enabled;
        editButton.Enabled = enabled;
        deleteButton.Enabled = enabled;
        changeStatusButton.Enabled = enabled;
        refreshButton.Enabled = enabled;
    }
}
