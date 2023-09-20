using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCaseCabins
{
    public interface IGetAllCabinsUC
    {
        public IEnumerable<Cabin> GetAllCabins();
    }
}
