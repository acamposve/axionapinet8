using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Database.EFContext.Entities;

public class CategoryEntity
{
    public CategoryEntity()
    {
        Products = new HashSet<ProductEntity>();
    }

    [Key]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(500)]
    public string Description { get; set; }

    public string Image { get; set; }

    // Relación uno a muchos con Product
    public ICollection<ProductEntity> Products { get; set; }
}
