namespace Todo.Presentation.Views.Contracts;

using ErrorOr;

using Todo.Domain.TodoItems;
using Todo.Presentation.Models;

public interface IMainView
{
    event EventHandler? ViewShown;
    event EventHandler? RefreshRequested;
    event EventHandler? FilterChanged;
    event EventHandler? AddRequested;
    event EventHandler? EditRequested;
    event EventHandler? DeleteRequested;
    event EventHandler? ChangeStatusRequested;

    MainViewFilterState GetFilterState();
    TodoItemRow? GetSelectedTodoItem();

    void BindCategories(IReadOnlyList<CategoryFilterItem> categories);
    void BindTodoItems(IReadOnlyList<TodoItemRow> items);
    void SetBusy(bool isBusy);

    void ShowInfo(string message);
    bool ShowConfirmation(string message);
    void ShowErrors(IReadOnlyList<Error> errors);

    bool ShowCreateDialog();
    bool ShowEditDialog(Guid id);
    TodoItemStatus? ShowStatusChangeDialog();
}
