using PruebaChatMVC.Dto;
using PruebaChatMVC.Models;

namespace PruebaChatMVC.Extensions
{
    public static class UsersExtension
    {
        public static UserDto ToDto(this Users value) => value is not null ? new UserDto(value.IdUser, value.UserName) : null;
    }
}
