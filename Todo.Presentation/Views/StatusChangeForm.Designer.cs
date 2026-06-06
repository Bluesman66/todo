namespace Todo.Presentation.Views;

partial class StatusChangeForm
{
    private ComboBox statusComboBox;
    private Button okButton;
    private Button cancelButton;
    private Label statusLabel;

    private void InitializeComponent()
    {
        statusLabel = new Label();
        statusComboBox = new ComboBox();
        okButton = new Button();
        cancelButton = new Button();
        SuspendLayout();

        statusLabel.AutoSize = true;
        statusLabel.Location = new Point(12, 18);
        statusLabel.Text = "Новый статус:";

        statusComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        statusComboBox.Location = new Point(110, 15);
        statusComboBox.Size = new Size(200, 23);

        okButton.Location = new Point(154, 55);
        okButton.Size = new Size(75, 27);
        okButton.Text = "ОК";
        okButton.Click += OkButton_Click;

        cancelButton.DialogResult = DialogResult.Cancel;
        cancelButton.Location = new Point(235, 55);
        cancelButton.Size = new Size(75, 27);
        cancelButton.Text = "Отмена";

        AcceptButton = okButton;
        CancelButton = cancelButton;
        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(324, 96);
        Controls.Add(statusLabel);
        Controls.Add(statusComboBox);
        Controls.Add(okButton);
        Controls.Add(cancelButton);
        FormBorderStyle = FormBorderStyle.FixedDialog;
        MaximizeBox = false;
        MinimizeBox = false;
        StartPosition = FormStartPosition.CenterParent;
        Text = "Смена статуса";

        ResumeLayout(false);
        PerformLayout();
    }
}
