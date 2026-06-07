namespace Todo.Presentation;
using System.IO;
using System.Windows.Forms;
using Microsoft.Extensions.DependencyInjection;
using Todo.Application;
using Todo.Infrastructure;
using Todo.Infrastructure.Common.Persistence;
using Todo.Presentation.Views;
static class Program
{
    [STAThread]
    static void Main()
    {
        var dbPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Todo.db");
        var connectionString = $"Data Source={dbPath}";

        var services = new ServiceCollection();

        services.AddApplication();
        services.AddInfrastructure(connectionString);
        services.AddPresentation();

        var provider = services.BuildServiceProvider();
        provider.GetRequiredService<DbInitializer>().Initialize();

        Application.EnableVisualStyles();
        Application.SetCompatibleTextRenderingDefault(false);
        Application.Run(provider.GetRequiredService<MainForm>());
    }
}
