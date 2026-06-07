namespace Todo.Domain.Categories;

public class Category
{
    public Guid Id { get; }
    public string Name { get; private set; } = null!;

    public Category(string name, Guid? id = null)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Название категории не может быть пустым.", nameof(name));
        }

        Id = id ?? Guid.NewGuid();
        Name = name;
    }

    public static Category FromPersistence(Guid id, string name) =>
        new Category(name, id);

    private Category() { }
}
