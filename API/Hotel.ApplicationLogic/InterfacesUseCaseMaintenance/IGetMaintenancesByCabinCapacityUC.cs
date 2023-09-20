using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCaseMaintenance
{
    public interface IGetMaintenancesByCabinCapacityUC
    {
        public IEnumerable<Maintenance> GetMaintenancesByCabinCapacity(int value1, int value2);
    }
}
