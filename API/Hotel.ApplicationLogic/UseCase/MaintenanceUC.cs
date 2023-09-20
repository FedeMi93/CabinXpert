using DTOs;
using Hotel.ApplicationLogic.InterfacesUseCaseMaintenance;
using Obligatorio_1.Entidades;
using Obligatorio_1.Interfaces;
using Obligatorio_1.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.ApplicationLogic.UseCase
{
    public class MaintenanceUC : IAddMaintenanceUC, IGetMaintenanceByDateUC, IGetMaintenancesByCabinCapacityUC, IAddMaintenanceDtoUC,
        IGetMaintenancesDtoByCabinCapacity, IGetMaintenancesDtoByDateUC
    {
        private IMaintenanceRepository mainRepository;
        

        public MaintenanceUC(IMaintenanceRepository mainRepository)
        {
            this.mainRepository = mainRepository;
        }

        public void Add(Maintenance maintenance)
        {
            mainRepository.Add(maintenance);
        }

        public void AddMaintenanceDto(MaintenanceDto maintenance)
        {
            Maintenance newMain = new Maintenance();
            newMain.Date = maintenance.Date;
            newMain.Description = maintenance.Description;
            newMain.Cost = maintenance.Cost;
            newMain.NameOfWorker = new NameWorker(maintenance.NameWorker);
            newMain.CabinId = maintenance.CabinId;

            mainRepository.Add(newMain);
        }
        
        public IEnumerable<Maintenance> GetMaintenancesByCabinCapacity(int value1, int value2)
        {
            return mainRepository.GetMainByCapacity(value1,value2);
        }
        public IEnumerable<MaintenanceDto> GetMaintenancesDtoByCabinCapacity(int value1, int value2)
        {
            var maintenances = mainRepository.GetMainByCapacity(value1, value2);
            var maintenancesDto = new List<MaintenanceDto>();
            foreach (var item in maintenances) 
            {
                maintenancesDto.Add(new MaintenanceDto(item));
            }
            return maintenancesDto;
        }

        //public Cabin GetCabinById(int id)
        //{
        //    return mainRepository.GetCabinById(id);
        //}

        public IEnumerable<Maintenance> GetMaintenancesByDate(int id, DateTime initialDate, DateTime lastDate)
        {
            return mainRepository.GetMainByDates(id, initialDate, lastDate);
        }
        public IEnumerable<MaintenanceDto> GetMaintenancesDtoByDate(int id, DateTime initialDate, DateTime lastDate)
        {
            var maintenances =  mainRepository.GetMainByDates(id, initialDate, lastDate);
            var maintenancesDto = new List<MaintenanceDto>();
            foreach (var item in maintenances)
            {
                maintenancesDto.Add(new MaintenanceDto(item));
            }
            return maintenancesDto;
        }
    }
}
