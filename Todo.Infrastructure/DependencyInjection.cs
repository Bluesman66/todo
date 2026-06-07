namespace Todo.Infrastructure;

using Microsoft.Extensions.DependencyInjection;

using Todo.Infrastructure.Categories.Persistence;
using Todo.Infrastructure.Common.Persistence;
using Todo.Infrastructure.TodoItems.Persistence;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddSingleton<ISqliteConnectionFactory>(_ => new SqliteConnectionFactory(connectionString));
        services.AddScoped<UnitOfWork>();
        services.AddScoped<IUnitOfWork>(sp => sp.GetRequiredService<UnitOfWork>());
        services.AddScoped<ITodoItemsRepository, TodoItemsRepository>();
        services.AddScoped<ICategoriesRepository, CategoriesRepository>();
        services.AddSingleton<DbInitializer>();

        return services;
    }
}
