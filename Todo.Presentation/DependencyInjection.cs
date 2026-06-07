namespace Todo.Presentation;

using Microsoft.Extensions.DependencyInjection;

using Todo.Presentation.Presenters;
using Todo.Presentation.Views;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentation(this IServiceCollection services)
    {
        services.AddTransient<MainPresenter>();
        services.AddTransient<TodoItemPresenter>();
        services.AddTransient<MainForm>();
        services.AddTransient<TodoItemForm>();
        services.AddTransient<StatusChangeForm>();

        return services;
    }
}
