using Obligatorio_1.Interfaces;
using Obligatorio_1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.ComponentModel.DataAnnotations.Schema;
using Obligatorio_1.ValueObjects;

namespace Obligatorio_1.Entidades
{
    [Index(nameof(Name), IsUnique = true)]
    public class CabinType : IValidable
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre de tipo de cabaña es requerido.")]
        [Column("Type Name")]  
        public string Name { get; set; }
        [StringLength(200, MinimumLength = 10, ErrorMessage = "La descripción del tipo de cabaña puede ser entre 10 y 200 caracteres.")]
        public string Description { get; set; }
        
        public CostPerPerson CostPerson { get; set; }
        
        public CabinType() { }
        public CabinType(int id, string name, string description, CostPerPerson costPerson)
        {
            Id = id;
            Name = name;
            Description = description;
            CostPerson = costPerson;
        }
        
       
        public void IsValid()
        {
            ValidName();
            CostPerson.IsValid();
            //ValidDescription();                           
                      
        }
        private void ValidName()
        {
            Regex regex = new Regex("^(?!\\s)([A-Za-z ]*?)(?<!\\s)$");
            bool meetsExpression = regex.IsMatch(Name);

            if (!meetsExpression) 
            {
                throw new CabinException("El nombre solo debe incluir caracteres alfabéticos y espacios embebidos.");
            }
        }
   
        private void ValidDescription()
        {
            if(Description.Length < 10 || Description.Length > 200 ) 
            {
                throw new CabinException("La descripción debe estar comprendida entre 10 y 200 caracteres");
            }
            
        }
    }
}
