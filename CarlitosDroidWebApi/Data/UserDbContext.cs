using CarlitosDroidWebApi.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace CarlitosDroidWebApi.Data;

public partial class UserDbContext : DbContext
{
    public virtual DbSet<User> Users { get; set; } = null!;

    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>(entity =>
        {
            entity.ToTable("Usuario");
            entity.Property(e => e.Id).ValueGeneratedNever().HasColumnName("id");
            entity.Property(e => e.Nombre).HasMaxLength(20).HasColumnName("name");
            entity.Property(e => e.Apellido).HasMaxLength(20).HasColumnName("lastname");
            entity.Property(e => e.Telefono).HasColumnName("phone");
        });
        OnModelCreatingPartial(modelBuilder);

    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
