using Microsoft.EntityFrameworkCore;
using Obligatorio_1.Entidades;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using Obligatorio_1.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.AccessData.Repositories
{
    public class SqlRepositoryMaintenance : IMaintenanceRepository
    {

        public HotelContext context { get; set; }
        public SqlRepositoryMaintenance()
        {
            context = new HotelContext();
        }


        public void Add(Maintenance maintenance)
        {
            try
            {
                var maintenanceDate = context.Maintenances.Where(m => m.CabinId == maintenance.CabinId && m.Date.Date == maintenance.Date.Date).ToList();
                if (maintenanceDate.Count() >= 3)
                {
                    throw new CabinException("Ya se realizaron 3 mantenimientos en el día.");
                }
                
                    maintenance.IsValid();                
                    context.Maintenances.Add(maintenance);
                    context.SaveChanges();
            }
            catch (CabinException ce)
            {
                throw ce;
            }
            catch (Exception ex)
            {
                throw new CabinException(ex.Message);
            }

        }

        public void Delete(Maintenance maintenance)
        {
            context.Maintenances.Remove(maintenance);
        }

        public IEnumerable<Maintenance> GetAll()
        {
            return context.Maintenances;
        }

        public Maintenance GetById(int id)
        {
            return context.Maintenances.ToList()[id];
        }

        public void Update(Maintenance maintenance)
        {
            context.Maintenances.Update(maintenance);
        }
        public IEnumerable<int> GetCabinsId()
        {
            return context.Cabins.Select(c => c.Id).ToList();
        }

        public Cabin GetCabinById(int id)
        {
            return context.Cabins.Where(x => x.Id == id).Include("Type").FirstOrDefault();
        }

        public IEnumerable<Maintenance> GetMainByDates(int id, DateTime fromDate, DateTime toDate)
        {
            return context.Maintenances.Where(m => m.CabinId == id && m.Date.Date >= fromDate.Date && m.Date.Date <= toDate.Date).OrderByDescending(m => m.Cost).Include("Cabin").ToList();
        }

        public IEnumerable<Maintenance> GetMainByCapacity(int value1, int value2) 
        {
            int capacity1;
            int capacity2;
            if (value1 > value2)
            {
                capacity1 = value2;
                capacity2 = value1;
            }
            else 
            {
                capacity1 = value1;
                capacity2 = value2;
            }
            return context.Maintenances.Where(m => m.Cabin.Capacity.Value >= capacity1 && m.Cabin.Capacity.Value <= capacity2)
                .GroupBy(main => main.NameOfWorker.Value)
                .Select(group => new Maintenance { NameOfWorker = new NameWorker { Value = group.Key },  Cost = group.Sum(m => m.Cost) }).ToList();
        }
    }
}
