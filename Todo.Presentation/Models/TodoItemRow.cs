namespace Todo.Presentation.Models;

using System.ComponentModel;

using Todo.Domain.TodoItems;

public class TodoItemRow
{
    [Browsable(false)]
    public Guid Id { get; }

    [DisplayName("Название")]
    public string Title { get; }

    [DisplayName("Категория")]
    public string CategoryName { get; }

    [DisplayName("Приоритет")]
    public string Priority { get; }

    [DisplayName("Срок")]
    public string DueDate { get; }

    [DisplayName("Статус")]
    public string Status { get; }

    public TodoItemRow(
        Guid id,
        string title,
        string categoryName,
        string priority,
        string dueDate,
        string status)
    {
        Id = id;
        Title = title;
        CategoryName = categoryName;
        Priority = priority;
        DueDate = dueDate;
        Status = status;
    }

    public static TodoItemRow From(TodoItem item, string categoryName) =>
        new TodoItemRow(
            item.Id,
            item.Title,
            categoryName,
            GetPriorityText(item.Priority),
            item.DueDate?.ToString("dd.MM.yyyy") ?? "—",
            GetStatusText(item.Status));

    private static string GetPriorityText(TodoPriority priority) => priority switch
    {
        TodoPriority.Low => "Низкий",
        TodoPriority.Medium => "Средний",
        TodoPriority.High => "Высокий",
        _ => priority.ToString()
    };

    private static string GetStatusText(TodoItemStatus status) => status switch
    {
        TodoItemStatus.Pending => "Ожидает",
        TodoItemStatus.InProgress => "В работе",
        TodoItemStatus.Completed => "Выполнена",
        TodoItemStatus.Cancelled => "Отменена",
        _ => status.ToString()
    };
}
