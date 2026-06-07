namespace Todo.Domain.Categories;

using ErrorOr;

public static class CategoryErrors
{
    public static readonly Error NotFound = Error.NotFound(
        "Category.NotFound",
        "Категория не найдена");
}
