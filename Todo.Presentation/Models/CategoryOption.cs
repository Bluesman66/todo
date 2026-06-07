namespace Todo.Presentation.Models;

public sealed class CategoryOption
{
    public Guid Id { get; }
    public string Name { get; }

    public CategoryOption(Guid id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;
}
