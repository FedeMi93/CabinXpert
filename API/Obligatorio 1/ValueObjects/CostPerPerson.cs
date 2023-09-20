using Microsoft.EntityFrameworkCore;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_1.ValueObjects
{
    [Owned]
    public class CostPerPerson : IValidable
    {
        [Range(0, double.MaxValue, ErrorMessage = "El costo debe ser mayor a 0.")]
        public double Value { get; set; }
        public CostPerPerson() { }
        public CostPerPerson(double value)
        {
            Value = value;
            IsValid();
        }
        public void IsValid()
        {
            if (Value <= 0)
            {
                throw new CabinException("El costo no puede ser menor o igual a 0.");
            }
        }
    }
}
