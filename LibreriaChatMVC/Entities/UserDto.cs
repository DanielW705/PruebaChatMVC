using LibreriaChatMVC.Enums;

namespace LibreriaChatMVC.Entities
{
    public record class UserDto(string Id, RolesType rol, string Nombre);
}
