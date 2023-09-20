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
    public class NameWorker : IValidable
    {
        public string Value { get; set; }

        public NameWorker() { }
        public NameWorker(string value)
        {
            Value = value;
            IsValid();
        }

        public void IsValid()
        {
            if (string.IsNullOrEmpty(Value)) throw new CabinException("Falta ingresar el nombre del operador que realizo el mantenimiento."); ;
        }
    }
}
