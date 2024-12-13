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
            entity.Property(e => e.UserID).HasColumnName("id");
            entity.Property(e => e.Firstname).HasMaxLength(20).HasColumnName("nombre");
            entity.Property(e => e.Lastname).HasMaxLength(20).HasColumnName("apellido");
            entity.Property(e => e.Email).HasMaxLength(20).HasColumnName("correo");
            entity.Property(e => e.MobileNo).HasMaxLength(20).HasColumnName("telefono");
            entity.Property(e => e.Password).HasMaxLength(20).HasColumnName("password");
            entity.Property(e => e.ConfirmPassword).HasMaxLength(20).HasColumnName("confirmPassword");
            entity.Property(e => e.Salt).HasMaxLength(20).HasColumnName("salt");
            entity.Property(e => e.LastActiondatetime).HasMaxLength(20).HasColumnName("date");
            entity.Property(e => e.IsActive).HasMaxLength(20).HasColumnName("isActive");
            entity.Property(e => e.Role).HasMaxLength(20).HasColumnName("role");
        });
        OnModelCreatingPartial(modelBuilder);
    }
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}