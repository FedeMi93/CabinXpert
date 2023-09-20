using Microsoft.EntityFrameworkCore;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using Obligatorio_1.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Obligatorio_1.Entidades
{
    [Index(nameof(Email), IsUnique = true)]
    public class User : IValidable
    {
        public int Id { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public Password Password { get; set; }

        public User() { }

        public User(int id, string email, Password password)
        {
            Id = id;            
            Email = email;
            Password = password;
        }

        public void IsValid()
        {
            if(string.IsNullOrEmpty(Email))throw new Exception("Falta ingresar un mail para el usuario.");
            Password.IsValid();
            ValidEmail();
        }
       
        private void ValidEmail() 
        {
            Regex regex = new Regex("^[a-zA-Z0-9._-]+@[a-zA-Z]+\\.[a-zA-Z]{2,}$");
            bool meetsExpression = regex.IsMatch(Email);

            if (!meetsExpression)
            {
                throw new CabinException("El email ingresado no es reconocido como un mail valido.");
            }
        }
    }
}
