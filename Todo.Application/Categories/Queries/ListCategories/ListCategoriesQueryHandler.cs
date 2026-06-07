namespace Todo.Application.Categories.Queries.ListCategories;

using ErrorOr;

using MediatR;

public class ListCategoriesQueryHandler : IRequestHandler<ListCategoriesQuery, ErrorOr<List<Category>>>
{
    private readonly ICategoriesRepository _categoriesRepository;

    public ListCategoriesQueryHandler(ICategoriesRepository categoriesRepository)
    {
        _categoriesRepository = categoriesRepository;
    }

    public async Task<ErrorOr<List<Category>>> Handle(ListCategoriesQuery query, CancellationToken cancellationToken)
    {
        var categories = await _categoriesRepository.ListAsync();
        return categories;
    }
}
