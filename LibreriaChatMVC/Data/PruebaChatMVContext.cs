using LibreriaChatMVC.Models;
using LibreriaChatMVC.Seeders;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LibreriaChatMVC.Data
{
    public class PruebaChatMVContext : DbContext
    {
        private readonly ISeeder<Roles> _rolesSeeder;
        private readonly ISeeder<EstadoDeConexion> _estadoSeeder;
        private readonly ISeeder<Estatus> _estatusSeeder;
        private readonly ISeeder<Usuario, Estatus[]> _usuarioSeeder;
        private readonly ISeeder<ChatGrupal, Usuario[]> _chatGrupalSeeder;
        private readonly ISeeder<CatalogoIntegrantes, Usuario[], Guid> _integrantesSeeder;
        private readonly ISeeder<ChatIndividual, Usuario[]> _chatIndividualSeeder;
        private readonly ISeeder<Mensajes, ChatBase[], Usuario[]> _mensajesSeeder;
        public DbSet<Roles> Roles { get; set; }
        public DbSet<EstadoDeConexion> EstadosDeConexion { get; set; }
        public DbSet<Estatus> Estatuses { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<ChatGrupal> ChatsGrupales { get; set; }
        public DbSet<CatalogoIntegrantes> Integrantes { get; set; }
        public DbSet<ChatIndividual> ChatsIndividuales { get; set; }
        public DbSet<Mensajes> Mensajes { get; set; }
        public PruebaChatMVContext(DbContextOptions<PruebaChatMVContext> options) : base(options)
        {
            _rolesSeeder = new RolesSeeder();
            _estadoSeeder = new EstadosDeConextionSeeder();
            _estatusSeeder = new EstatusSeeder();
            _usuarioSeeder = new UsuarioSeeder();
            _chatGrupalSeeder = new ChatGrupalSeeder();
            _integrantesSeeder = new IntegrantesSeeder();
            _chatIndividualSeeder = new ChatIndividualSeeder();
            _mensajesSeeder = new MensajesSeeder();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.EnableSensitiveDataLogging();
            optionsBuilder.ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var roles = _rolesSeeder.ApplySeed();
            var estados = _estadoSeeder.ApplySeed();
            var estatus = _estatusSeeder.ApplySeed();
            var usuarios = _usuarioSeeder.ApplySeed(estatus);
            var chatsGrupales = _chatGrupalSeeder.ApplySeed(usuarios);
            var integrantes = _integrantesSeeder.ApplySeed(usuarios, chatsGrupales.First().IdChat);
            var chatsIndividuales = _chatIndividualSeeder.ApplySeed(usuarios);
            var mensajes = _mensajesSeeder.ApplySeed(chatsIndividuales
                                                     .Concat<ChatBase>(chatsGrupales)
                                                     .ToArray(),
                                                     usuarios);

            modelBuilder.Entity<Roles>(e =>
            {
                e.HasKey(r => r.ID);
                e.Property(r => r.Sumary)
                .IsRequired()
                .HasMaxLength(30);
                e.Property(r => r.Description)
                .IsRequired()
                .HasMaxLength(50);
                AplicarSoftDelete(e);
                e.HasData(roles);
            });
            modelBuilder.Entity<EstadoDeConexion>(e =>
            {
                e.HasKey(ec => ec.Id);
                e.Property(ec => ec.Sumary)
                .IsRequired()
                .HasMaxLength(30);
                e.Property(ec => ec.Description)
                .IsRequired()
                .HasMaxLength(50);
                AplicarSoftDelete(e);
                e.HasData(estados);
            });
            modelBuilder.Entity<Estatus>(e =>
            {
                e.HasKey(es => es.Id);
                e.Property(es => es.UltimaConextion)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAddOrUpdate();
                e.HasOne(es => es.Rel_Estatus_Estado)
                .WithMany(ec => ec.Rel_Estado_Estatus)
                .HasForeignKey(es => es.IdEstatus);
                e.HasData(estatus);
            });
            modelBuilder.Entity<Usuario>(e =>
            {
                e.HasKey(u => u.IdUsuario);
                e.Property(u => u.UserName)
                .IsRequired()
                .HasMaxLength(50);
                e.Property(u => u.Password)
                .IsRequired()
                .HasMaxLength(16);
                e.HasOne(u => u.Rel_Usuario_Rol)
                  .WithMany(r => r.Rel_Roles_Usuarios)
                  .HasForeignKey(u => u.Rol);
                e.HasOne(u => u.Rel_Usuario_Estatus)
                .WithOne(e => e.Rel_Estatus_Usuario)
                .HasForeignKey<Usuario>(u => u.Estatus);
                AplicarSoftDelete(e);
                e.HasData(usuarios);
            });
            modelBuilder.Entity<CatalogoIntegrantes>(e =>
            {
                e.HasKey(ci => ci.Id);
                e.Property(ci => ci.FechaDeIngreso)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()")
                .ValueGeneratedOnAdd();
                e.HasOne(ci => ci.Rel_Integrantes_Usuario)
                .WithMany(u => u.Rel_Usuario_Integrantes)
                .HasForeignKey(ci => ci.IdUsuario);
                e.HasQueryFilter(ci => !ci.Rel_Integrantes_Usuario!.IsDeleted);
                e.HasData(integrantes);
            });
            modelBuilder.Entity<ChatBase>(e =>
            {
                e.HasKey(chi => chi.IdChat);
                e.UseTptMappingStrategy();
                AplicarSoftDelete(e);
            });
            modelBuilder.Entity<ChatGrupal>(e =>
            {
                e.HasMany(chg => chg.Rel_Chats_Integrantes)
                .WithOne(ci => ci.Rel_Integrantes_Chat)
                .HasForeignKey(ci => ci.IdChat);
                e.HasData(chatsGrupales);
            });
            modelBuilder.Entity<ChatIndividual>(e =>
            {
                e.HasOne(chi => chi.Rel_Emisor_Chat)
                .WithMany(em => em.Rel_Usuario_ChatIniciados)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(chi => chi.Emisor);
                e.HasOne(chi => chi.Rel_Receptor_Chat)
                .WithMany(em => em.Rel_Usuario_ChatRecibidos)
                .OnDelete(DeleteBehavior.Restrict)
                .HasForeignKey(chi => chi.Receptor);
                e.HasData(chatsIndividuales);
            });
            modelBuilder.Entity<Mensajes>(e =>
            {
                e.HasKey(m => m.IdMensaje);
                e.Property(m => m.Mensaje)
                .HasMaxLength(320);
                e.HasOne(m => m.Rel_Mensaje_Chat)
                .WithMany(ch => ch.Rel_Mensajes_Chat)
                .HasForeignKey(m => m.IdChat);
                e.HasOne(m => m.Rel_Mensaje_Usuario)
                .WithMany(u => u.Rel_Usuario_Mensajes)
                .HasForeignKey(m => m.IdEmisor);
                e.HasQueryFilter(m => !m.Rel_Mensaje_Chat!.IsDeleted);
                e.HasData(mensajes);
            });
            base.OnModelCreating(modelBuilder);
        }
        private void AplicarSoftDelete<T>(EntityTypeBuilder<T> e) where T : SoftDelete
        {
            e.Property(sf => sf.IsDeleted)
             .HasDefaultValue(false)
             .ValueGeneratedOnAdd();

            e.Property(sf => sf.CreateDate)
             .HasDefaultValueSql("GETDATE()")
             .ValueGeneratedOnAdd();

            e.HasQueryFilter(sf => !sf.IsDeleted);
        }
    }
}
