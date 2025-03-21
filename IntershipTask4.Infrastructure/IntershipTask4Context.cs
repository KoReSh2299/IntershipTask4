using System;
using System.Collections.Generic;
using IntershipTask4.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace IntershipTask4.Infrastructure;

public partial class IntershipTask4Context : DbContext
{
    public IntershipTask4Context()
    {
    }

    public IntershipTask4Context(DbContextOptions<IntershipTask4Context> options)
        : base(options)
    {
    }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC071F133038");

            entity.HasIndex(e => e.Email, "EmailIndex").IsUnique();

            entity.Property(e => e.Email).HasMaxLength(255);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.LastLoginTime).HasColumnType("datetime");
            entity.Property(e => e.Name).HasMaxLength(30);
            entity.Property(e => e.PasswordHash).HasMaxLength(128);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
