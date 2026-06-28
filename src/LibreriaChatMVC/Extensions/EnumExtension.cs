using System.ComponentModel;
using System.Reflection;

namespace LibreriaChatMVC.Extensions
{
    public static class EnumExtension
    {
        public static string GetDescription(this Enum value)
        {
            string output = value.ToString();
            FieldInfo? field = value.GetType()?.GetField(value.ToString());

            if (field is not null)
            {
                var attr = Attribute.GetCustomAttribute(field, typeof(DescriptionAttribute)) as DescriptionAttribute;
                if (attr is not null)
                    output = attr.Description;
            }

            return output;
        }
        // El constraint 'where T : Enum' asegura que solo le pases Enums
        public static T ObtenerDesdeDescripcion<T>(string descripcion) where T : Enum
        {
            foreach (var campo in typeof(T).GetFields())
            {
                // Buscamos si el campo tiene el atributo [Description]
                if (Attribute.GetCustomAttribute(campo, typeof(DescriptionAttribute)) is DescriptionAttribute atributo)
                {
                    if (atributo.Description == descripcion)
                    {
                        return (T)campo.GetValue(null);
                    }
                }
                else
                {
                    // Como plan de respaldo, si no tiene etiqueta, comparamos con el nombre normal de la variable
                    if (campo.Name == descripcion)
                    {
                        return (T)campo.GetValue(null);
                    }
                }
            }

            // Si el texto no coincide con nada, lanzas un error (o puedes retornar un valor por defecto)
            throw new ArgumentException($"No se encontró ningún Enum con la descripción: {descripcion}");
        }
    }
}
