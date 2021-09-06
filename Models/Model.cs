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
                entity.HasMany(a => a.relUser_Reciber)
                .WithOne(b => b.relReciver_User)
                .HasForeignKey(d => d.Reciber)
                .HasConstraintName("Relacion_Usuario_Receptor")
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasMany(a => a.relUser_Sender)
                .WithOne(b => b.relSender_User)
                .HasForeignKey(d => d.Sender)
                .HasConstraintName("Relacion_Usuario_Emisor")
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasData(
                    new User { id = Guid.NewGuid(), Pasword = "123", UserName = "Daniel" },
                    new User { id = Guid.NewGuid(), Pasword = "456", UserName = "Julio" });
            });
            modelBuilder.Entity<UserChat>(entity =>
            {
                entity.HasKey(d => d.idUser);
                entity.HasOne(a => a.relChat_User)
                .WithOne(b => b.relChat_User)
                .HasForeignKey<UserChat>(f => f.idUser)
                .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<MessageSended>(entity =>
            {
                entity.HasKey(d => d.idMensaje);
            });
        }
        public DbSet<User> Usuario { get; set; }
        public DbSet<UserChat> UsuarioChat { get; set; }

        public DbSet<MessageSended> MensajesEnviados { get; set; }

    }
}
