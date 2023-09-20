using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_1.Exceptions
{
    public class CabinException : Exception
    {

        public CabinException() { }

        public CabinException(string mensaje) : base(mensaje) { }
        public CabinException(string mensaje, Exception ex) : base(mensaje, ex) { }
    }
}
