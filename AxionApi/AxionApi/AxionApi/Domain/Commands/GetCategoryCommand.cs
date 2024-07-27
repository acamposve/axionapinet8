namespace Domain.Commands;

public sealed class GetCategoryCommand(int id)
{
    public int Id { get; init; } = id;
}