using Obligatorio_1.Entidades;
using Obligatorio_1.Interfaces;
using Obligatorio_1.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace Hotel.AccessData.Repositories
{
    public class SqlRepositoryCabinType : ICabinTypeRepository
    {
        public HotelContext context { get; set; }
        public SqlRepositoryCabinType()
        {
            context = new HotelContext();
        }
        public void Add(CabinType item)
        {
            try
            {
                item.IsValid();
                context.CabinTypes.Add(item);
                context.SaveChanges();
            }
            catch (CabinException ce)
            {
                throw ce;
            }
            catch (DbUpdateException dbEx)
            {
                throw new CabinException("El nombre de tipo de cabaña debe ser único.");
            }
            catch (Exception e)
            {
                throw new CabinException(e.Message);
            }
            
            
        }

        public void Delete(CabinType item)
        {
            try 
            {
                bool inUse = context.Cabins.Any(c => c.Type == item);
                if (!inUse) 
                {
                    context.CabinTypes.Remove(item);
                    context.SaveChanges();
                }
                else 
                {
                    throw new CabinException("El tipo de cabaña esta en uso.");
                }
            }catch (CabinException ce) 
            {
                throw ce;
            } catch (Exception e) 
            {
                throw new CabinException(e.Message);
            }

        }

        public IEnumerable<CabinType> GetAll()
        {
            return context.CabinTypes;
           
        }

        public CabinType GetById(int id)
        {
            return context.CabinTypes.Where(ct => ct.Id == id).FirstOrDefault();
        }

        public void Update(CabinType item, string description, int costPerson)
        {
            try 
            {
                item.CostPerson.Value = costPerson;
                item.Description = description;
                item.IsValid();
                context.SaveChanges();
            }
            catch (CabinException ce)
            {
                throw ce;
            }
            catch (Exception e)
            {
                throw new CabinException(e.Message);
            }
        }
        public CabinType GetByName(string name) 
        {
            return context.CabinTypes.Where(t => t.Name == name).FirstOrDefault();
        }

        public void Update(CabinType item)
        {
            throw new NotImplementedException();
        }
    }
}

        
