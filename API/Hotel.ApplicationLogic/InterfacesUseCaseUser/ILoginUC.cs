using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCaseUser
{
    public interface ILoginUC
    {
        public bool Login(string username, string password);
    }
}
