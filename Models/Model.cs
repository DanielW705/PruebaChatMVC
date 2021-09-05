using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace PruebaChatMVC.Models
{
    public class Model : DbContext
    {
        public Model(DbContextOptions<Model> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(d => d.id);
                entity.Property(p => p.UserName).IsRequired();
                entity.HasOne(a => a.relChat_User)
                .WithOne(b => b.relChat_User)
                .HasForeignKey<User>(f => f.id)
                .HasConstraintName("Relacion_Usuario_Chat")
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasData(new User { id = Guid.NewGuid(), Pasword = "123", UserName = "Daniel" });
            });
            modelBuilder.Entity<UserChat>(entity =>
            {
                entity.HasKey(d => d.idUser);
                entity.HasOne(a => a.relChat_User)
                .WithOne(b => b.relChat_User)
                .HasForeignKey<UserChat>(f => f.idUser)
                .OnDelete(DeleteBehavior.Restrict);
            });


        }
        public DbSet<User> Usuario { get; set; }
        public DbSet<UserChat> UsuarioChat { get; set; }

    }
}
