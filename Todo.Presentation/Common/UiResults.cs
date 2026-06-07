namespace Todo.Presentation.Common;

using ErrorOr;

public static class UiResults
{
    public static void ShowErrors(IReadOnlyList<Error> errors)
    {
        var message = string.Join(Environment.NewLine, errors.Select(e => e.Description));
        MessageBox.Show(message, "Ошибка", MessageBoxButtons.OK, MessageBoxIcon.Warning);
    }

    public static bool ShowConfirmation(string message, string title = "Подтверждение")
    {
        return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
    }
}
