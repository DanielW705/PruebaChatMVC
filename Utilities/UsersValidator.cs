using PruebaChatMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PruebaChatMVC.Utilities
{
    public class UsersValidator : ValidationAttribute
    {
        private static List<string> usuarios = new List<string> { "Daniel", "Julio" };
        public override bool IsValid(object value)
        {
            if (!(value == null))
            {

                int noExistente = (from usuario in usuarios
                                   where usuario == value.ToString()
                                   select usuario).Count();
                if (noExistente > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

    }
    public class PasswordValidation : ValidationAttribute
    {
        private static List<string> contraseñas = new List<string> { "123", "1234" };
        public override bool IsValid(object value)
        {
            if (!(value == null))
            {
                int noExistente = (from contraseña in contraseñas
                                   where contraseña == value.ToString()
                                   select contraseña).Count();

                if (noExistente > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
