namespace Todo.Presentation.Views;

partial class TodoItemForm
{
    private TextBox titleTextBox;
    private TextBox descriptionTextBox;
    private ComboBox categoryComboBox;
    private ComboBox priorityComboBox;
    private ComboBox statusComboBox;
    private DateTimePicker dueDatePicker;
    private Button saveButton;
    private Button cancelButton;
    private Label titleLabel;
    private Label descriptionLabel;
    private Label categoryLabel;
    private Label priorityLabel;
    private Label statusLabel;
    private Label dueDateLabel;

    private void InitializeComponent()
    {
        titleLabel = new Label();
        titleTextBox = new TextBox();
        descriptionLabel = new Label();
        descriptionTextBox = new TextBox();
        categoryLabel = new Label();
        categoryComboBox = new ComboBox();
        priorityLabel = new Label();
        priorityComboBox = new ComboBox();
        statusLabel = new Label();
        statusComboBox = new ComboBox();
        dueDateLabel = new Label();
        dueDatePicker = new DateTimePicker();
        saveButton = new Button();
        cancelButton = new Button();
        SuspendLayout();

        titleLabel.AutoSize = true;
        titleLabel.Location = new Point(12, 15);
        titleLabel.Text = "Название:";

        titleTextBox.Location = new Point(120, 12);
        titleTextBox.Size = new Size(340, 23);

        descriptionLabel.AutoSize = true;
        descriptionLabel.Location = new Point(12, 48);
        descriptionLabel.Text = "Описание:";

        descriptionTextBox.Location = new Point(120, 45);
        descriptionTextBox.Multiline = true;
        descriptionTextBox.Size = new Size(340, 70);
        descriptionTextBox.ScrollBars = ScrollBars.Vertical;

        categoryLabel.AutoSize = true;
        categoryLabel.Location = new Point(12, 128);
        categoryLabel.Text = "Категория:";

        categoryComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        categoryComboBox.Location = new Point(120, 125);
        categoryComboBox.Size = new Size(200, 23);

        priorityLabel.AutoSize = true;
        priorityLabel.Location = new Point(12, 161);
        priorityLabel.Text = "Приоритет:";

        priorityComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        priorityComboBox.Location = new Point(120, 158);
        priorityComboBox.Size = new Size(200, 23);

        dueDateLabel.AutoSize = true;
        dueDateLabel.Location = new Point(12, 194);
        dueDateLabel.Text = "Срок:";

        dueDatePicker.Location = new Point(120, 191);
        dueDatePicker.Size = new Size(200, 23);

        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(12, 227);
        statusLabel.Text = "Статус:";

        statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        statusComboBox.Location = new Point(120, 224);
        statusComboBox.Size = new Size(200, 23);
        statusComboBox.Enabled = false;

        saveButton.Location = new Point(294, 265);
        saveButton.Size = new Size(80, 27);
        saveButton.Text = "Сохранить";
        saveButton.Click += SaveButton_Click;

        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(380, 265);
        cancelButton.Size = new Size(80, 27);
        cancelButton.Text = "Отмена";

        AcceptButton = saveButton;
        CancelButton = cancelButton;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(474, 308);
        Controls.Add(titleLabel);
        Controls.Add(titleTextBox);
        Controls.Add(descriptionLabel);
        Controls.Add(descriptionTextBox);
        Controls.Add(categoryLabel);
        Controls.Add(categoryComboBox);
        Controls.Add(priorityLabel);
        Controls.Add(priorityComboBox);
        Controls.Add(dueDateLabel);
        Controls.Add(dueDatePicker);
        Controls.Add(statusLabel);
        Controls.Add(statusComboBox);
        Controls.Add(saveButton);
        Controls.Add(cancelButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Новая задача";

        ResumeLayout(false);
        PerformLayout();
    }
}
