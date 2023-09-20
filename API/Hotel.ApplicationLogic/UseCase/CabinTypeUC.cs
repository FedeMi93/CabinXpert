using DTOs;
using Hotel.ApplicationLogic.InterfacesUseCabinType;
using Hotel.ApplicationLogic.InterfacesUseCaseCabins;
using Hotel.ApplicationLogic.InterfacesUseCaseCabinType;
using Microsoft.IdentityModel.Tokens;
using Obligatorio_1.Entidades;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using Obligatorio_1.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.UseCase
{
    public class CabinTypeUC : IGetAllCabinsTypeUC, IGetByIdCabinTypeUC, IGetByNameCabinTypeUC, IAddCabinTypeDtoUC,
        IAddCabinTypeUC, IUpdateCabinTypeUC, IDeleteCabinTypeUC, IGetAllCabinTypeDtoUC, IGetByIdCabinTypeDtoUC, IGetByNameCabinTypeDtoUC
    {
        private ICabinTypeRepository cTypeRepository;

        public CabinTypeUC(ICabinTypeRepository cTypeRepository)
        {
            this.cTypeRepository = cTypeRepository;
        }

        public void Add(CabinType cabinType)
        {
            cTypeRepository.Add(cabinType);
        }
        public void AddDto(CabinTypeDto cabinType)
        {
            CabinType newCabinType = new CabinType();
            newCabinType.Name = cabinType.Name;
            newCabinType.Description = cabinType.Description;
            newCabinType.CostPerson = new CostPerPerson(cabinType.CostPerPerson);
            
            cTypeRepository.Add(newCabinType);
        }

        public void Delete(CabinType cabinType)
        {
            cTypeRepository.Delete(cabinType);
        }

        public IEnumerable<CabinType> GetAllCabinsType()
        {
            return cTypeRepository.GetAll();
        }
        public IEnumerable<CabinTypeDto> GetAllCabinTypeDto()
        {
            var cabinsType = cTypeRepository.GetAll();
            var cabinsTypeDto = new List<CabinTypeDto>();
            if (!cabinsType.IsNullOrEmpty())
            {
                foreach (var c in cabinsType)
                {
                    cabinsTypeDto.Add(new CabinTypeDto(c));
                }
                return cabinsTypeDto;
            }
            throw new CabinException("No se encontraron tipos de cabaña.");
        }

        public CabinType GetByName(string name)
        {
            return cTypeRepository.GetByName(name);
        }
        public CabinTypeDto GetByNameDto(string name)
        {
            CabinType cT = cTypeRepository.GetByName(name);
            if (cT != null)
            {
                CabinTypeDto cabinTypeDto = new CabinTypeDto(cT);
                return cabinTypeDto;
            }
            throw new CabinException("No se encontraron tipos de cabaña con ese nombre.");
        }

        public CabinType GetCTById(int id)
        {
            return cTypeRepository.GetById(id);
        }
        public CabinTypeDto GetCTDtoById(int id)
        {
            CabinType ct = cTypeRepository.GetById(id);
            if (ct != null)
            {
                return new CabinTypeDto(ct);
            }
            throw new CabinException("No hubo coincidencias con el id ingresado.");
        }

        public void Update(CabinType item, string description, int costPerson)
        {
            cTypeRepository.Update(item, description, costPerson);
        }
    }
}
