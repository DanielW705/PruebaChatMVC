using LibreriaChatMVC.Entities;
using LibreriaChatMVC.Extensions;
using LibreriaChatMVC.Ports.Primary;
using LibreriaChatMVC.Ports.Secondary;
using LibreriaChatMVC.ViewModels;
using ROP;

namespace PruebaChatMVC.Ports.Primary
{
    public class LoginServices : ILoginServices
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly IValidateUserRepository _validateUserRepository;
        public LoginServices(IIdentityRepository identityRepository, IValidateUserRepository validateUserRepository)
        {
            _identityRepository = identityRepository;
            _validateUserRepository = validateUserRepository;
        }
        public async Task<Result<UserDto>> TryToLogin(UsuarioViewModel user)
        {
            var output = new Result<UserDto>();
            var userDb = await _validateUserRepository.ValidateUserExist(user.UserName!, user.Password!);
            if (userDb is not null)
            {
                await _identityRepository.SignInUser(userDb.Id, userDb.Nombre, userDb.rol.GetDescription());
                output = userDb;
            }
            else
                output = Result.NotFound<UserDto>("El usuario no fue encontrado, vuelva a intentar iniciar sesion o cree un nuevo usario");
            return output;

        }
    }
}
