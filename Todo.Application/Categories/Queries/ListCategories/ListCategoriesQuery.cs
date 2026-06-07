namespace Todo.Application.Categories.Queries.ListCategories;

using ErrorOr;

using MediatR;

public class ListCategoriesQuery : IRequest<ErrorOr<List<Category>>>
{
}
