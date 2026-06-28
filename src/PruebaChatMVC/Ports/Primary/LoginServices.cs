using LibreriaChatMVC.Entities;
using LibreriaChatMVC.Extensions;
using LibreriaChatMVC.Ports.Primary;
using LibreriaChatMVC.Ports.Secondary;
using LibreriaChatMVC.ViewModels;
using ROP;
using System.Security.Authentication;

namespace PruebaChatMVC.Ports.Primary
{
    public class LoginServices : ILoginServices
    {
        private readonly IIdentityRepository _identityRepository;
        private readonly ISignUserRepository _SignUserRepository;
        private readonly ILogger _logger;
        public LoginServices(IIdentityRepository identityRepository, ISignUserRepository validateUserRepository, ILogger<LoginServices> logger)
        {
            _identityRepository = identityRepository;
            _SignUserRepository = validateUserRepository;
            _logger = logger;
        }

        public async Task LogOutAsync(string id, CancellationToken ctoken)
        {
            await _identityRepository.SingOutAsync();
            await _SignUserRepository.SignOutUserAsync(id, ctoken);

        }

        public async Task<Result<UserDto>> RegisterAndLogInAsync(UsuarioViewModel user, CancellationToken ctoken)
        {
            var output = new Result<UserDto>();
            try
            {
                var userDb = await _SignUserRepository.CreateNewUserAsync(user.UserName, user.Password, ctoken);
                await _identityRepository.SignInUserAsync(userDb.Id, userDb.Nombre, userDb.rol.GetDescription());
                output = userDb;
            }
            catch (InvalidCredentialException ex) when (ex is InvalidCredentialException)
            {
                output = Result.Conflict<UserDto>("Las credenciales dadas no son correctas, intentelo de nuevo");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Hubo una excepcion al guardar en BD");
                output = Result.Failure<UserDto>("Hubo un error al guardar den BD, hablar con equipo de TI");
            }
            return output;
        }

        public async Task<Result<UserDto>> TryToLoginAsync(UsuarioViewModel user, CancellationToken ctoken)
        {
            var output = new Result<UserDto>();
            var userDb = await _SignUserRepository.SignInUserAsync(user.UserName, user.Password, ctoken);
            if (userDb is not null)
            {
                await _identityRepository.SignInUserAsync(userDb.Id, userDb.Nombre, userDb.rol.GetDescription());
                output = userDb;
            }
            else
                output = Result.NotFound<UserDto>("El usuario no fue encontrado, vuelva a intentar iniciar sesion o cree un nuevo usario");
            return output;
        }
    }
}
