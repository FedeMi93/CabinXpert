using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Obligatorio_1.Entidades;

namespace Obligatorio_1.Interfaces
{
    public interface ICabinRepository : IRepository<Cabin>
    {
        public IEnumerable<CabinType> GetCabinTypes();
        public IEnumerable<Cabin> GetByName(string name);
        public IEnumerable<Cabin> GetByType(string cabinType);
        public IEnumerable<Cabin> GetByCapacity(int? capacity);
        public IEnumerable<Cabin> GetOnlyEnable();
        public string GetPictureName(Cabin cabin);
        public IEnumerable<Cabin> GetByCost(int cost);

    }
}
