using Obligatorio_1.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class CabinTypeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public  double CostPerPerson { get; set; }

        public CabinTypeDto() { }
        public CabinTypeDto(int id, string name, string description, double costPerPerson)
        {
            Id = id;
            Name = name;
            Description = description;
            CostPerPerson = costPerPerson;
        }
        public CabinTypeDto(CabinType ct) 
        {
            Id = ct.Id;
            Name = ct.Name;
            Description = ct.Description;
            CostPerPerson = ct.CostPerson.Value; 
        }
    }
}
