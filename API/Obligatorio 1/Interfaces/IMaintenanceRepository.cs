using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_1.Interfaces
{
    public interface IMaintenanceRepository : IRepository<Maintenance>
    {
        public IEnumerable<int> GetCabinsId();
        public Cabin GetCabinById(int id);
        public IEnumerable<Maintenance> GetMainByDates(int id, DateTime fromDate, DateTime toDate);
        public IEnumerable<Maintenance> GetMainByCapacity(int value1, int value2);
    }
}
