using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCaseCabinType
{
    public interface IGetByIdCabinTypeDtoUC
    {
        public CabinTypeDto GetCTDtoById(int id);
    }
}
