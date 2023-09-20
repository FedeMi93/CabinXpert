using Microsoft.EntityFrameworkCore;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Obligatorio_1.ValueObjects
{
    [Owned]
    public class Password : IValidable
    {
        public string Value { get; set; }
        public Password() { }
        public Password(string value)
        {
            Value = value;
            IsValid();
        }

        public void IsValid()
        {
            if (string.IsNullOrEmpty(Value)) throw new CabinException("Falta ingresar un password para el usuario.");
            Regex regex = new Regex("^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])[a-zA-Z0-9]{6,}$");
            bool meetsExpression = regex.IsMatch(Value);

            if (!meetsExpression)
            {
                throw new CabinException("El password debe contener al menos 6 caracteres que incluyan letras mayúsculas y minúsculas (al menos una de cada una) y dígitos (0 al 9).");
            }
        }
    }
}
