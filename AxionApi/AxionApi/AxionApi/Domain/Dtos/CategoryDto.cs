using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;

public class CategoryDto
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } = string.Empty;
    public string Image { get; init; } = string.Empty;
    public string ProductName { get; init; } = string.Empty;
    public string ProductImage { get; init; } = string.Empty;

    public CategoryDto()
    {


    }


}
