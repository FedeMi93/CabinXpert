using DTOs;
using Hotel.ApplicationLogic.InterfacesUseCaseCabins;
using Obligatorio_1.Entidades;
using Obligatorio_1.Interfaces;
using Obligatorio_1.ValueObjects;

namespace Hotel.ApplicationLogic.UsesCases
{
    public class CabinUC : IGetAllCabinsUC, IGetByCapacityCabinUC, IGetByNameCabinUC, IGetByTypeCabinUC, IGetOnlyEnableCabinUC,
        IAddCabinUC, IGetPictureNameUC, IDeleteCabinUC, IAddCabinDtoUC, IGetCabinByCostUC, IGetByIdCabinUC, IGetAllCabinsDtoUC, IGetCabinDtoByCostUC,
        IGetByNameCabinDtoUC,IGetByTypeCabinDtoUC, IGetOnlyEnableCabinDtoUC, IGetByCapacityCabinsDtoUC
    {
        private ICabinRepository cabinRepository;

        public CabinUC(ICabinRepository cabinRepository)
        {
            this.cabinRepository = cabinRepository;
        }

        public void Add(Cabin cabin)
        {
            cabinRepository.Add(cabin);
        }
        public void AddDto(CabinDto cabin) 
        {
            Cabin newCabin = new Cabin();
            newCabin.Name = cabin.Name;
            newCabin.TypeId = cabin.typeId;
            newCabin.Description = cabin.Description;
            newCabin.JacuzziPriv = cabin.JacuzziPriv;
            newCabin.EnabledReservation = cabin.EnabledReservation;
            newCabin.NuRoom = cabin.NuRoom;
            newCabin.Capacity = new Capacity(cabin.Capacity);
            newCabin.Picture = cabin.Picture;

            cabinRepository.Add(newCabin);
        }        

        public void Delete(Cabin cabin)
        {
            cabinRepository.Delete(cabin);
        }

        public IEnumerable<Cabin> GetAllCabins()
        {
            return cabinRepository.GetAll();
        }
        public IEnumerable<CabinDto> GetAllCabinDto() 
        {
            var cabins = cabinRepository.GetAll();
            var cabinsDto = new List<CabinDto>();
            foreach (var cabin in cabins) 
            {
                cabinsDto.Add(new CabinDto(cabin));
            }
            return cabinsDto;
        }

        public Cabin GetById(int id)
        {
            return cabinRepository.GetById(id);
        }

        public IEnumerable<Cabin> GetCabinByCostUC(int cost)
        {
            return cabinRepository.GetByCost(cost);
        }
        public IEnumerable<CabinDto> GetCabinDtoByCostUC(int cost) 
        {
            var cabins = cabinRepository.GetByCost(cost);
            var cabinsDto = new List<CabinDto>();
            foreach (var cabin in cabins) 
            {
                cabinsDto.Add(new CabinDto(cabin));
            }
            return cabinsDto;
        }

        public IEnumerable<Cabin> GetCabinsByCapacity(int? capacity)
        {
            return cabinRepository.GetByCapacity(capacity);
        }
        public IEnumerable<CabinDto> GetCabinsDtoByCapacity(int? capacity)
        {
            var cabins = cabinRepository.GetByCapacity(capacity);
            var cabinsDto = new List<CabinDto>();
            foreach (var cabin in cabins)
            {
                cabinsDto.Add(new CabinDto(cabin));
            }
            return cabinsDto;

        }


        public IEnumerable<Cabin> GetCabinsByName(string name)
        {
            return cabinRepository.GetByName(name);
        }
        public IEnumerable<CabinDto> GetCabinsDtoByName(string name)
        {
            var cabins = cabinRepository.GetByName(name);
            var cabinsDto = cabins.Select(x => new CabinDto(x));
            //foreach (var cabin in cabins)
            //{
            //    cabinsDto.Add(new CabinDto(cabin));
            //}
            return cabinsDto;
        }

        public IEnumerable<Cabin> GetCabinsByType(string cabinType)
        {
            return cabinRepository.GetByType(cabinType);
        }
        public IEnumerable<CabinDto> GetCabinsDtoByType(string cabinType)
        {
            var cabins = cabinRepository.GetByType(cabinType);
            var cabinsDto = cabins.Select(x => new CabinDto(x));
            return cabinsDto;
        }
    

        public IEnumerable<Cabin> GetCabinsOnlyEnable()
        {
            return cabinRepository.GetOnlyEnable();
        }
        public IEnumerable<CabinDto> GetCabinsDtoOnlyEnable()
        {
            var cabins = cabinRepository.GetOnlyEnable();
            var cabinsDto = new List<CabinDto>();
            foreach (var cabin in cabins)
            {
                cabinsDto.Add(new CabinDto(cabin));
            }
            return cabinsDto;
        }

        public string GetPictureName(string cabinName)
        {
            char[] pictureNameChars = cabinName.ToCharArray();
            char toSearch = ' ';
            char toReplace = '_';

            for (int i = 0; i < pictureNameChars.Length; i++)
            {
                if (pictureNameChars[i] == toSearch)
                {
                    pictureNameChars[i] = toReplace;
                }
            }
            string pictureName = new string(pictureNameChars);
            pictureName = pictureName + "_001";
            return pictureName;            
        }
    }
}