﻿using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCaseCabins
{
    public interface IGetByTypeCabinUC
    {
        public IEnumerable<Cabin> GetCabinsByType(string cabinType);
    }
}
