
namespace Domain.Commands;

public sealed class UpdateCategoryCommand
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;
}
