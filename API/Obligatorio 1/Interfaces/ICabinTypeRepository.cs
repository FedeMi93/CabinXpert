using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_1.Interfaces
{
    public interface ICabinTypeRepository: IRepository<CabinType>
    {
        public CabinType GetByName(string name);
        public void Update(CabinType item, string description, int costPerson);

    }
}
