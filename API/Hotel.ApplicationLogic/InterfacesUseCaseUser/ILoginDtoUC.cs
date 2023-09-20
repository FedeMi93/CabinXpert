using Hotel.WebApi.DTOs;
using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.InterfacesUseCaseUser
{
    public interface ILoginDtoUC
    {
        public User LoginDto(UserDto user);
    }
}
