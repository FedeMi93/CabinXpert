using Obligatorio_1.Entidades;
using Obligatorio_1.Interfaces;
using Obligatorio_1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccessData.Repositories
{
    public class SqlRepositoryUsers : IUserRepository
    {

        public HotelContext context { get; set; }
        public SqlRepositoryUsers()
        {
            context = new HotelContext();
        }


        public void Add(User user)
        {
            try
            {
                user.IsValid();
                context.Users.Add(user);
                context.SaveChanges();
            } catch (CabinException ce)
            {
                throw ce;
            } catch (Exception ex) 
            {
                throw new CabinException(ex.Message);
            }
        }

        public void Delete(User user)
        {
            context.Users.Remove(user);
        }

        public IEnumerable<User> GetAll()
        {
            return context.Users;
        }

        public User GetById(int id)
        {
            return context.Users.ToList()[id];
        }

        public void Update(User user)
        {
            context.Users.Update(user);
        }
        public void Login(string email, string password) 
        {
            /*
            try
            {
                User user = context.Users.Where(u => u.Email == email).FirstOrDefault();
                if (user == null) { throw new CabinException("No se encontro el usuario."); }
                if (user.Password.Value != password) { throw new CabinException("La contraseña ingresada no es valida."); }
            }
            catch (CabinException ce) 
            {
                throw ce;
            }
            catch (Exception ex)
            {
                throw new CabinException(ex.Message);
            }
            */
        }

        public string GetPassByMail(string mail)
        {
            User user = context.Users.Where(funcionario => funcionario.Email == mail).FirstOrDefault();
            return user.Password.Value;
        }

        public User GetByEmail(string email)
        {
            return context.Users.Where(x => x.Email == email).FirstOrDefault();
        }
    }
}
