using Infrastructure.Database.EFContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.EFContext;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    public DbSet<UserEntity> Users { get; set; }
    public DbSet<CategoryEntity> Categories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
}
