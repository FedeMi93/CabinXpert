using Microsoft.EntityFrameworkCore;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_1.ValueObjects
{
    [Owned]
    public class Capacity : IValidable
    {
        public int Value { get; set; }
        public Capacity() { }
        public Capacity(int value)
        {
            Value = value;
            IsValid();
        }

        public void IsValid()
        {
            if (Value <= 0) throw new CabinException("La capacidad de la cabaña debe ser un valor positivo.");
        }
    }
}
