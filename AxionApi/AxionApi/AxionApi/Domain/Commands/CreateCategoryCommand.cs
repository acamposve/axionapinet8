namespace Domain.Commands;

public sealed class CreateCategoryCommand(string name, string description, string image)
{
    public string Name { get; init; } = name;
    public string Description { get; init; } = description;
    public string Image { get; init; } = image;
}
