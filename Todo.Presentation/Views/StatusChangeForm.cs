namespace Todo.Presentation.Views;

using Todo.Domain.TodoItems;

public partial class StatusChangeForm : Form
{
    public TodoItemStatus SelectedStatus { get; private set; }

    public StatusChangeForm()
    {
        InitializeComponent();
        statusComboBox.Items.AddRange(new object[]
        {
            "Ожидает",
            "В работе",
            "Выполнена",
            "Отменена"
        });
        statusComboBox.SelectedIndex = 0;
    }

    private void OkButton_Click(object? sender, EventArgs e)
    {
        SelectedStatus = statusComboBox.SelectedIndex switch
        {
            0 => TodoItemStatus.Pending,
            1 => TodoItemStatus.InProgress,
            2 => TodoItemStatus.Completed,
            3 => TodoItemStatus.Cancelled,
            _ => TodoItemStatus.Pending
        };

        DialogResult = DialogResult.OK;
        Close();
    }
}
