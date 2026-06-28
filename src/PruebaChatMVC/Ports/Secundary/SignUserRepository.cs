using LibreriaChatMVC.Data;
using LibreriaChatMVC.Entities;
using LibreriaChatMVC.Enums;
using LibreriaChatMVC.Models;
using LibreriaChatMVC.Ports.Secondary;
using Microsoft.EntityFrameworkCore;
using System.Security.Authentication;

namespace PruebaChatMVC.Ports.Secundary
{
    public class SignUserRepository : ISignUserRepository
    {
        private readonly PruebaChatMVContext _context;
        private readonly DbSet<Usuario> _usuarioEntity;
        private readonly ILogger _logger;
        public SignUserRepository(PruebaChatMVContext context, ILogger<SignUserRepository> logger)
        {
            _usuarioEntity = context.Set<Usuario>();
            _context = context;
            _logger = logger;
        }

        public async Task<UserDto> CreateNewUserAsync(string username, string password, CancellationToken ctoken)
        {
            var exist = await _usuarioEntity.AnyAsync(u => u.UserName.Equals(username)
                        && u.Password.Equals(password), ctoken);
            if (exist)
                throw new InvalidCredentialException();
            var newUser = new Usuario()
            {
                UserName = username,
                Password = password,
                Rol = 3,
                Rel_Usuario_Estatus = new Estatus()
                {
                    IdEstatus = 2,
                    UltimaConextion = DateTime.Now
                }
            };
            await _usuarioEntity.AddAsync(newUser);
            await _context.SaveChangesAsync(ctoken);
            return new UserDto(newUser.IdUsuario.ToString(), (RolesType)newUser.Rol, newUser.UserName);
        }

        public async Task<UserDto?> SignInUserAsync(string username, string password, CancellationToken ctoken)
        {
            UserDto? output = null;
            var user = await _usuarioEntity
                        .FirstOrDefaultAsync(u => u.UserName.Equals(username)
                        && u.Password.Equals(password));

            if (user is not null)
            {
                user.Rel_Usuario_Estatus.IdEstatus = 2;
                _usuarioEntity.Attach(user);
                _context.Entry(user).State = EntityState.Modified;
                try
                {
                    await _context.SaveChangesAsync(ctoken);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error al cambiar el estatus");
                }
                output = new UserDto(user.IdUsuario.ToString(), (RolesType)user.Rol, user.UserName);
            }
            return output;
        }
        public async Task<bool> SignOutUserAsync(string id, CancellationToken ctoken)
        {
            Usuario user = await _usuarioEntity
                            .FirstAsync(u => u.IdUsuario.Equals(Guid.Parse(id)));
            user.Rel_Usuario_Estatus.IdEstatus = 2;
            user.Rel_Usuario_Estatus.UltimaConextion = DateTime.Now;
            _usuarioEntity.Attach(user);
            _context.Entry(user).State = EntityState.Modified;
            var output = false;
            try
            {
                await _context.SaveChangesAsync(ctoken);
                output = true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error al cambiar el estatus");
            }
            return output;
        }
    }

}

