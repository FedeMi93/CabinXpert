using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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
    public class SqlRepositoryCabin : ICabinRepository
    {
        // CREO EL CONTEXTO -- Lo utilizo para hacer las firmas
        public HotelContext context { get; set; }
        public SqlRepositoryCabin()
        {
            context = new HotelContext();
        }


        public void Add(Cabin cabin)
        {
            try
            {
                cabin.IsValid();
                ValidNumRoom(cabin);
                context.Cabins.Add(cabin);
                context.SaveChanges();
            }
            catch (CabinException ce)
            {
                throw ce;
            }            
            catch (Exception)
            {
                throw new CabinException("El nombre de la cabaña no se puede repetir.");
            }
        }
        private void ValidNumRoom(Cabin cabin)
        {
            if (context.Cabins.Count() != 0)
            {
                int maxNum = context.Cabins.Max(c => c.NuRoom);
                if (cabin.NuRoom != maxNum + 1)
                {
                    throw new CabinException($"El número de habitacion maximo encontrado es {maxNum}, debe ingresar uno continuo.");
                }
            }
            else if (cabin.NuRoom != 1) 
            {
                throw new CabinException("Aun no se han registrado otras cabañas, esta debe tener el numero de habitacion 1.");
            }

        }

        public void Delete(Cabin cabin)
        {
            context.Cabins.Remove(cabin);
            context.SaveChanges();
        }

        public IEnumerable<Cabin> GetAll()
        {
            return context.Cabins.OrderBy(cabin => cabin.Capacity.Value).Include("Type").OrderBy(c => c.NuRoom).ToList();
        }

        public Cabin GetById(int id)
        {
            return context.Cabins.Where(c => c.Id == id).FirstOrDefault();
        }

        public void Update(Cabin cabin)
        {
            context.Cabins.Update(cabin);
        }
        public IEnumerable<CabinType> GetCabinTypes()
        {
            return context.CabinTypes.Select(ct => new CabinType { Id = ct.Id, Name = ct.Name }).ToList();
        }
        public IEnumerable<Cabin> GetByName(string name)
        {
            return context.Cabins.Where(t => t.Name.Contains(name)).Include("Type").ToList();
        }
        public IEnumerable<Cabin> GetByType(string cabinType)
        {
            int idCabinType = int.Parse(cabinType);
            return context.Cabins.Where(t => t.TypeId == idCabinType).Include("Type").ToList();
        }

        public IEnumerable<Cabin> GetByCapacity(int? capacity)
        {
            return context.Cabins.Where(t => t.Capacity.Value >= capacity).Include("Type").ToList();
        }

        public IEnumerable<Cabin> GetOnlyEnable()
        {
            return context.Cabins.Where(t => t.EnabledReservation == true).Include("Type").ToList();
        }
        public string GetPictureName(Cabin cabin) 
        {
            return cabin.CreatePictureName();
        }

        public IEnumerable<Cabin> GetByCost(int cost) 
        {
            var cabins = context.Cabins.Where(c => c.Type.CostPerson.Value < cost && c.EnabledReservation && c.JacuzziPriv)
                .Select(cabin => new Cabin { Name = cabin.Name, Capacity = new Capacity {Value = cabin.Capacity.Value } }).ToList();
            return cabins;
        }        
       
    }
}
