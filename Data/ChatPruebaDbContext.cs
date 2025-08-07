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
        private readonly ParticipantsSeeder _participantsSeeder = new ParticipantsSeeder();
        public DbSet<Users> Usuario { get; set; }
        public DbSet<Chats> Chats { get; set; }

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
            Users[] users = _userSeeder.ApplySeed();

            Chats chats = _chatSeeder.ApplySeed();

            Participants[] participants = _participantsSeeder.ApplySeed(users, chats);
            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(d => d.IdUser);
                entity.Property(p => p.UserName)
                .IsRequired();
                entity.Property(p => p.pasword)
                .IsRequired();
                entity.Property(u => u.isDelete)
                .IsRequired();
                entity.Property(u => u.Created)
                .IsRequired();
                entity.HasQueryFilter(u => !u.isDelete);
                entity.Property(uc => uc.isDelete)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.Property(uc => uc.Created)
                       .HasDefaultValueSql("getdate()");
                entity.HasData(users);
            });

            modelBuilder.Entity<Chats>(entity =>
            {
                entity.HasKey(c => c.IdChat);

                entity.HasQueryFilter(u => !u.isDelete);

                entity.Property(c => c.ChatName);

                entity.Property(uc => uc.isDelete)
               .IsRequired()
               .HasDefaultValue(false);

                entity.Property(uc => uc.Created)
                .HasDefaultValueSql("getdate()");

                entity.HasData(chats);
            });


            modelBuilder.Entity<Messages>(entity =>
            {
                entity.HasKey(d => d.idMessage);
                entity.HasOne(s => s.UserSended)
                       .WithMany(u => u.UserSendMessage)
                       .HasConstraintName("FK_RelacionUsuarioEmisor")
                       .OnDelete(DeleteBehavior.Restrict)
                       .HasForeignKey(f => f.IdUserSender);
                entity.HasOne(m => m.ChatSended)
                       .WithMany(ch => ch.MessagesSendedToThisChat)
                       .HasConstraintName("FK_RelacionChatMensajes")
                       .OnDelete(DeleteBehavior.Cascade)
                       .HasForeignKey(f => f.IdChatSended);
                entity.HasQueryFilter(u => !u.isDelete);

                entity.Property(m => m.isDelete)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.Property(m => m.Created)
                      .HasDefaultValueSql("getdate()");
            });

            modelBuilder.Entity<Participants>(entity =>
            {
                entity.HasKey(p => p.IdParticipants);

                entity.HasOne(p => p.UserInTheChat)
                      .WithMany(u => u.ParticipantInChat)
                      .HasForeignKey(f => f.IdUser)
                      .HasConstraintName("FK_RelacionParticipantesChat")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasOne(p => p.Chat)
                      .WithMany(c => c.ChatParticipants)
                      .HasForeignKey(f => f.IdChat)
                      .HasConstraintName("ChatParticipants")
                      .OnDelete(DeleteBehavior.Cascade);
                entity.HasQueryFilter(u => !u.isDelete);

                entity.Property(m => m.isDelete)
                      .IsRequired()
                      .HasDefaultValue(false);

                entity.Property(m => m.Created)
                      .HasDefaultValueSql("getdate()");

                entity.HasData(participants);
            });
        }
    }
}
