using DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCaseMaintenance
{
    public interface IAddMaintenanceDtoUC
    {
        public void AddMaintenanceDto(MaintenanceDto maintenance);
    }
}
