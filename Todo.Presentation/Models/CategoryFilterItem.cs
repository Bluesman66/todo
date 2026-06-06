namespace Todo.Presentation.Models;

public sealed class CategoryFilterItem
{
    public Guid? Id { get; }
    public string Name { get; }

    public CategoryFilterItem(Guid? id, string name)
    {
        Id = id;
        Name = name;
    }

    public override string ToString() => Name;
}
