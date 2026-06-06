namespace Todo.Presentation.Views.Contracts;

using ErrorOr;

using Todo.Presentation.Models;

public interface ITodoItemView
{
    event EventHandler? ViewShown;
    event EventHandler? SaveRequested;

    void SetWindowTitle(string title);
    void SetStatusEnabled(bool enabled);
    void BindCategories(IReadOnlyList<CategoryOption> categories);
    void BindFormData(TodoItemFormModel model);
    TodoItemFormModel GetFormData();

    void SetBusy(bool isBusy);
    void ShowValidationError(string message);
    void ShowErrors(IReadOnlyList<Error> errors);
    void CloseWithSuccess();
    void CloseWithCancel();
}
