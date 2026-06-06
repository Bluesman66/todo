namespace Todo.Presentation.Views;

using ErrorOr;

using Todo.Domain.TodoItems;
using Todo.Presentation.Common;
using Todo.Presentation.Models;
using Todo.Presentation.Presenters;
using Todo.Presentation.Views.Contracts;

public partial class TodoItemForm : Form, ITodoItemView
{
    private readonly TodoItemPresenter _presenter;

    public event EventHandler? ViewShown;
    public event EventHandler? SaveRequested;

    public TodoItemForm(TodoItemPresenter presenter)
    {
        _presenter = presenter;
        InitializeComponent();
        InitializeComboBoxes();
        _presenter.Attach(this);
        FormClosed += (_, _) => _presenter.Detach();
    }

    public void PrepareCreate() => _presenter.PrepareCreate();

    public void PrepareEdit(Guid id) => _presenter.PrepareEdit(id);

    protected override void OnShown(EventArgs e)
    {
        base.OnShown(e);
        ViewShown?.Invoke(this, EventArgs.Empty);
    }

    private void InitializeComboBoxes()
    {
        priorityComboBox.Items.AddRange(new object[] { "Низкий", "Средний", "Высокий" });
        priorityComboBox.SelectedIndex = 1;

        statusComboBox.Items.AddRange(new object[] { "Ожидает", "В работе", "Выполнена", "Отменена" });
        statusComboBox.SelectedIndex = 0;

        dueDatePicker.Format = DateTimePickerFormat.Short;
        dueDatePicker.ShowCheckBox = true;
        dueDatePicker.Checked = false;
    }

    public void SetWindowTitle(string title) => Text = title;

    public void SetStatusEnabled(bool enabled) => statusComboBox.Enabled = enabled;

    public void BindCategories(IReadOnlyList<CategoryOption> categories)
    {
        categoryComboBox.DataSource = null;
        categoryComboBox.DisplayMember = nameof(CategoryOption.Name);
        categoryComboBox.ValueMember = nameof(CategoryOption.Id);
        categoryComboBox.DataSource = categories.ToList();

        if (categories.Count > 0)
        {
            categoryComboBox.SelectedIndex = 0;
        }
    }

    public void BindFormData(TodoItemFormModel model)
    {
        titleTextBox.Text = model.Title;
        descriptionTextBox.Text = model.Description;
        categoryComboBox.SelectedValue = model.CategoryId;
        priorityComboBox.SelectedIndex = (int)model.Priority;
        statusComboBox.SelectedIndex = (int)model.Status;

        if (model.HasDueDate && model.DueDate.HasValue)
        {
            dueDatePicker.Checked = true;
            dueDatePicker.Value = model.DueDate.Value;
        }
        else
        {
            dueDatePicker.Checked = false;
        }
    }

    public TodoItemFormModel GetFormData()
    {
        var categoryId = categoryComboBox.SelectedValue is Guid id ? id : Guid.Empty;

        return new TodoItemFormModel
        {
            Title = titleTextBox.Text,
            Description = descriptionTextBox.Text,
            CategoryId = categoryId,
            Priority = (TodoPriority)priorityComboBox.SelectedIndex,
            Status = (TodoItemStatus)statusComboBox.SelectedIndex,
            DueDate = dueDatePicker.Checked ? dueDatePicker.Value.Date : null,
            HasDueDate = dueDatePicker.Checked
        };
    }

    public void SetBusy(bool isBusy)
    {
        saveButton.Enabled = !isBusy;
        UseWaitCursor = isBusy;
    }

    public void ShowValidationError(string message) =>
        MessageBox.Show(message, "Проверка", MessageBoxButtons.OK, MessageBoxIcon.Warning);

    public void ShowErrors(IReadOnlyList<Error> errors) =>
        UiResults.ShowErrors(errors);

    public void CloseWithSuccess()
    {
        DialogResult = DialogResult.OK;
        Close();
    }

    public void CloseWithCancel()
    {
        DialogResult = DialogResult.Cancel;
        Close();
    }

    private void SaveButton_Click(object sender, EventArgs e) =>
        SaveRequested?.Invoke(this, EventArgs.Empty);
}
