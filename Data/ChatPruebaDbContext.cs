using Microsoft.EntityFrameworkCore;
using PruebaChatMVC.Models;
using PruebaChatMVC.Seeders;
using System;

namespace PruebaChatMVC.Data
{
    public class ChatPruebaDbContext : DbContext
    {
        private readonly UserSeeder _userSeeder = new UserSeeder();
        private readonly ChatSeeder _chatSeeder = new ChatSeeder();

        public DbSet<User> Usuario { get; set; }
        public DbSet<UserChat> UsuarioChat { get; set; }

        public DbSet<Messages> MensajesEnviados { get; set; }
        public ChatPruebaDbContext(DbContextOptions<ChatPruebaDbContext> options) : base(options)
        {
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableDetailedErrors();
            optionsBuilder.LogTo(Console.WriteLine);
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            User[] users = _userSeeder.ApplySeed();

            UserChat[] chats = _chatSeeder.ApplySeed(users);
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(d => d.id);
                entity.Property(p => p.UserName)
                .IsRequired();
                entity.Property(p => p.pasword)
                .IsRequired();
                entity.Property(u => u.isDelete)
                .IsRequired();
                entity.Property(u => u.Created)
                .IsRequired();
                entity.HasQueryFilter(u => !u.isDelete);
                entity.HasData(users);
            });
            modelBuilder.Entity<UserChat>(entity =>
            {
                entity.HasKey(d => d.IdChat);
                entity.Property(c => c.IdChat)
                .ValueGeneratedOnAdd();
                entity.HasOne(a => a.rel_User1_User2)
                .WithMany(b => b.relChat_User1)
                .HasForeignKey(f => f.idUser1)
                .HasConstraintName("FK_RelacionUsuario2Chat")
                .OnDelete(DeleteBehavior.Restrict);
                entity.HasOne(a => a.rel_User2_User1)
                .WithMany(b => b.relChat_User2)
                .HasForeignKey(f => f.idUser2)
                .HasConstraintName("FK_RelacionUsuario1Chat")
                .OnDelete(DeleteBehavior.Cascade);
                entity.Property(uc => uc.isDelete)
                .IsRequired()
                .HasDefaultValue(false);

                entity.Property(uc => uc.Created)
                .HasDefaultValueSql("getdate()");

                entity.HasData(chats);
            });

            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(d => d.idMensaje);
                entity.HasOne(s => s.relSender_User)
                       .WithMany(u => u.relUser_Sender)
                       .HasConstraintName("FK_RelacionUsuarioEmisor")
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasForeignKey(f => f.Sender);
                entity.HasOne(r => r.relReciver_User)
                       .WithMany(u => u.relUser_Reciber)
                       .HasConstraintName("FK_RelacionUsuarioReceptor")
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasForeignKey(f => f.Reciber);
                entity.HasOne(m => m.relMensaje_Chat)
                       .WithMany(ch => ch.Messages)
                       .HasConstraintName("FK_RelacionMensajesChat")
                       .OnDelete(DeleteBehavior.Cascade)
                       .HasForeignKey(f => f.IdChatSended);
                entity.Property(m => m.isDelete)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.Property(m => m.Created)
                      .HasDefaultValueSql("getdate()");


            });
        }
    }
}
