using LibreriaChatMVC.Data;
using LibreriaChatMVC.Entities;
using LibreriaChatMVC.Enums;
using LibreriaChatMVC.Models;
using LibreriaChatMVC.Ports.Secondary;
using Microsoft.EntityFrameworkCore;

namespace PruebaChatMVC.Ports.Secundary
{
    public class ValidateUserRepository : IValidateUserRepository
    {
        private readonly PruebaChatMVContext _context;
        private readonly DbSet<Usuario> _usuarioEntity;
        private readonly ILogger _logger;
        public ValidateUserRepository(PruebaChatMVContext context, ILogger<ValidateUserRepository> logger)
        {
            _usuarioEntity = context.Set<Usuario>();
            _context = context;
            _logger = logger;
        }
        public async Task<UserDto?> ValidateUserExist(string username, string password)
        {
            UserDto? output = null;
            var user = await _usuarioEntity
                        .Include(u => u.Rel_Usuario_Rol)
                        .Include(u => u.Rel_Usuario_Estatus)
                        .FirstOrDefaultAsync(u => u.UserName.Equals(username)
                        && u.Password.Equals(password));

            if (user is not null)
            {
                user.Rel_Usuario_Estatus!.UltimaConextion = DateTime.Now;
                user.Rel_Usuario_Estatus.IdEstatus = 2;
                _usuarioEntity.Attach(user);
                _context.Entry(user).State = EntityState.Modified;
                _ = Task.Factory.StartNew(() =>
                {
                    try
                    {
                        _context.SaveChangesAsync();
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError("Error al cambiar el estatus", ex);
                    }
                }
                );
                output = new UserDto(user.UserName, (RolesType)user.Rol, user.UserName);
            }
            return output;

        }
    }
}
