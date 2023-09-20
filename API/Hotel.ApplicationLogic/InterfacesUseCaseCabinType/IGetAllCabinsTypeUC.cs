using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCabinType
{
    public interface IGetAllCabinsTypeUC
    {
        public IEnumerable<CabinType> GetAllCabinsType();
    }
}
