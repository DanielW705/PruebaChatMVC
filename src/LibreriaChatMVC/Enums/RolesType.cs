using System.ComponentModel;

namespace LibreriaChatMVC.Enums
{
    public enum RolesType
    {
        [Description("ADMIN")]
        ADMIN = 1,
        [Description("DEV")]
        DEV = 2,
        [Description("USER")]
        USER = 3
    }
}
