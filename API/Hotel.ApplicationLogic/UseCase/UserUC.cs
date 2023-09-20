using Hotel.ApplicationLogic.InterfacesUseCaseUser;
using Hotel.WebApi.DTOs;
using Obligatorio_1.Entidades;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.UseCase
{
    public class UserUC : ILoginUC, ILoginDtoUC
    {
        private IUserRepository userRepository;

        public UserUC(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public Boolean Login(string mail, string password)
        {
            return userRepository.GetPassByMail(mail) == password;
        }

        public User LoginDto(UserDto user)
        {
            if(userRepository.GetPassByMail(user.Email) == user.Password)
            {
                var actualUser = userRepository.GetByEmail(user.Email);
                return actualUser;
            }
            throw new CabinException("Los datos de validacion no coiniden.");
        }
    }
}