using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_1.Interfaces
{
    public interface IUserRepository: IRepository<User>
    {
        public void Login(string email, string password);

        public string GetPassByMail(string mail);

        public User GetByEmail(string email);
        

    }
}
