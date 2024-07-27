﻿using Infrastructure.Database.EFContext.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Database.EFContext;

internal class UserContext : DbContext
{
    public UserContext(DbContextOptions<UserContext> options)
        : base(options) { }

    public virtual DbSet<UserEntity> Users => Set<UserEntity>();
    public virtual DbSet<RoleEntity> Roles => Set<RoleEntity>();

    public virtual DbSet<CategoryEntity> Categories => Set<CategoryEntity>();
    public virtual DbSet<ProductEntity> Products => Set<ProductEntity>();
}
