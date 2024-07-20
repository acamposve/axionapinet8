namespace Domain.Dtos;

public class ProductDto
{
    public string Name { get; init; } = string.Empty;

    public string Image { get; init; } = string.Empty;


    public int CategoryId { get; init; } = 0;
    public string Description { get; init; } = string.Empty;



    public decimal Price { get; init; } = 0;

    public bool IsFeatured { get; init; } = false;
}
