namespace Todo.Presentation.Views;

partial class MainForm
{
    private System.ComponentModel.IContainer components = null;
    private TableLayoutPanel mainLayout;
    private TableLayoutPanel filterLayout;
    private FlowLayoutPanel buttonPanel;
    private DataGridView todoItemsGrid;
    private Button addButton;
    private Button editButton;
    private Button deleteButton;
    private Button changeStatusButton;
    private Button refreshButton;
    private ComboBox categoryFilterComboBox;
    private ComboBox statusFilterComboBox;
    private Label categoryFilterLabel;
    private Label statusFilterLabel;

    protected override void Dispose(bool disposing)
    {
        if (disposing && (components != null))
        {
            components.Dispose();
        }

        base.Dispose(disposing);
    }

    private void InitializeComponent()
    {
        components = new System.ComponentModel.Container();
        mainLayout = new TableLayoutPanel();
        filterLayout = new TableLayoutPanel();
        buttonPanel = new FlowLayoutPanel();
        todoItemsGrid = new DataGridView();
        addButton = new Button();
        editButton = new Button();
        deleteButton = new Button();
        changeStatusButton = new Button();
        refreshButton = new Button();
        categoryFilterComboBox = new ComboBox();
        statusFilterComboBox = new ComboBox();
        categoryFilterLabel = new Label();
        statusFilterLabel = new Label();
        mainLayout.SuspendLayout();
        filterLayout.SuspendLayout();
        buttonPanel.SuspendLayout();
        ((System.ComponentModel.ISupportInitialize)todoItemsGrid).BeginInit();
        SuspendLayout();

        mainLayout.ColumnCount = 1;
        mainLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
        mainLayout.Controls.Add(filterLayout, 0, 0);
        mainLayout.Controls.Add(todoItemsGrid, 0, 1);
        mainLayout.Controls.Add(buttonPanel, 0, 2);
        mainLayout.Dock = DockStyle.Fill;
        mainLayout.Padding = new Padding(12);
        mainLayout.RowCount = 3;
        mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
        mainLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        filterLayout.AutoSize = true;
        filterLayout.ColumnCount = 6;
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 45F));
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F));
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        filterLayout.ColumnStyles.Add(new ColumnStyle(SizeType.AutoSize));
        filterLayout.Controls.Add(categoryFilterLabel, 0, 0);
        filterLayout.Controls.Add(categoryFilterComboBox, 1, 0);
        filterLayout.Controls.Add(statusFilterLabel, 2, 0);
        filterLayout.Controls.Add(statusFilterComboBox, 3, 0);
        filterLayout.Controls.Add(refreshButton, 5, 0);
        filterLayout.Dock = DockStyle.Fill;
        filterLayout.Margin = new Padding(0, 0, 0, 8);
        filterLayout.RowCount = 1;
        filterLayout.RowStyles.Add(new RowStyle(SizeType.AutoSize));

        categoryFilterLabel.Anchor = AnchorStyles.Left;
        categoryFilterLabel.AutoSize = true;
        categoryFilterLabel.Margin = new Padding(0, 6, 8, 0);
        categoryFilterLabel.Text = "Категория:";

        categoryFilterComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        categoryFilterComboBox.Dock = DockStyle.Fill;
        categoryFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        categoryFilterComboBox.Margin = new Padding(0, 3, 8, 0);
        categoryFilterComboBox.SelectedIndexChanged += FilterChangedHandler;

        statusFilterLabel.Anchor = AnchorStyles.Left;
        statusFilterLabel.AutoSize = true;
        statusFilterLabel.Margin = new Padding(0, 6, 8, 0);
        statusFilterLabel.Text = "Статус:";

        statusFilterComboBox.Anchor = AnchorStyles.Left | AnchorStyles.Right;
        statusFilterComboBox.Dock = DockStyle.Fill;
        statusFilterComboBox.DropDownStyle = ComboBoxStyle.DropDownList;
        statusFilterComboBox.Margin = new Padding(0, 3, 8, 0);
        statusFilterComboBox.SelectedIndexChanged += FilterChangedHandler;

        refreshButton.AutoSize = true;
        refreshButton.Margin = new Padding(0, 2, 0, 0);
        refreshButton.Text = "Обновить";
        refreshButton.Click += RefreshButton_Click;

        todoItemsGrid.AllowUserToAddRows = false;
        todoItemsGrid.AllowUserToDeleteRows = false;
        todoItemsGrid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        todoItemsGrid.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
        todoItemsGrid.Dock = DockStyle.Fill;
        todoItemsGrid.Margin = new Padding(0, 0, 0, 8);
        todoItemsGrid.MultiSelect = false;
        todoItemsGrid.ReadOnly = true;
        todoItemsGrid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;

        buttonPanel.AutoSize = true;
        buttonPanel.Controls.Add(addButton);
        buttonPanel.Controls.Add(editButton);
        buttonPanel.Controls.Add(deleteButton);
        buttonPanel.Controls.Add(changeStatusButton);
        buttonPanel.Dock = DockStyle.Fill;
        buttonPanel.FlowDirection = FlowDirection.LeftToRight;
        buttonPanel.Margin = new Padding(0);
        buttonPanel.WrapContents = false;

        addButton.AutoSize = true;
        addButton.Margin = new Padding(0, 0, 8, 0);
        addButton.Text = "Добавить";
        addButton.Click += AddButton_Click;

        editButton.AutoSize = true;
        editButton.Margin = new Padding(0, 0, 8, 0);
        editButton.Text = "Редактировать";
        editButton.Click += EditButton_Click;

        deleteButton.AutoSize = true;
        deleteButton.Margin = new Padding(0, 0, 8, 0);
        deleteButton.Text = "Удалить";
        deleteButton.Click += DeleteButton_Click;

        changeStatusButton.AutoSize = true;
        changeStatusButton.Margin = new Padding(0);
        changeStatusButton.Text = "Сменить статус";
        changeStatusButton.Click += ChangeStatusButton_Click;

        AutoScaleMode = AutoScaleMode.Font;
        ClientSize = new Size(784, 451);
        Controls.Add(mainLayout);
        MinimumSize = new Size(640, 400);
        StartPosition = FormStartPosition.CenterScreen;
        Text = "Список дел";

        mainLayout.ResumeLayout(false);
        mainLayout.PerformLayout();
        filterLayout.ResumeLayout(false);
        filterLayout.PerformLayout();
        buttonPanel.ResumeLayout(false);
        buttonPanel.PerformLayout();
        ((System.ComponentModel.ISupportInitialize)todoItemsGrid).EndInit();
        ResumeLayout(false);
    }
}
