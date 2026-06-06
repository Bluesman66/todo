namespace Todo.Domain.TodoItems;

using ErrorOr;

public static class TodoErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "TodoItem.NotFound",
        "Задача не найдена");

    public static readonly Error TitleRequired = Error.Validation(
        "TodoItem.TitleRequired",
        "Название задачи обязательно");

    public static readonly Error CategoryRequired = Error.Validation(
        "TodoItem.CategoryRequired",
        "Категория обязательна");

    public static readonly Error InvalidStatusTransition = Error.Validation(
        "TodoItem.InvalidStatusTransition",
        "Недопустимый переход статуса");

    public static readonly Error AlreadyCompleted = Error.Validation(
        "TodoItem.AlreadyCompleted",
        "Задача уже выполнена");
}
