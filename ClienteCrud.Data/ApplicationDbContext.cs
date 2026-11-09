using ClienteCrud.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ClienteCrud.Data;

/// <summary>
/// Contexto ORM (Entity Framework Core) para MySQL.
/// </summary>
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Cliente> Clientes => Set<Cliente>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("Clientes");
            entity.HasKey(c => c.Id);
            entity.Property(c => c.Nombre).IsRequired().HasMaxLength(80);
            entity.Property(c => c.Apellido).IsRequired().HasMaxLength(80);
            entity.Property(c => c.Email).IsRequired().HasMaxLength(120);
            entity.Property(c => c.Telefono).IsRequired().HasMaxLength(20);
            entity.Property(c => c.Direccion).IsRequired().HasMaxLength(200);
            entity.Property(c => c.FechaRegistro).IsRequired();
            entity.HasIndex(c => c.Email).IsUnique();
            entity.Ignore(c => c.NombreCompleto);
        });
    }
}
