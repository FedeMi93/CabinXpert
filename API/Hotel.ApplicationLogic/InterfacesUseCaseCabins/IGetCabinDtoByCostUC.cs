using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCaseCabins
{
    public interface IGetCabinDtoByCostUC
    {
        public IEnumerable<CabinDto> GetCabinDtoByCostUC(int cost);
    }
}
