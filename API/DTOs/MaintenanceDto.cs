using Obligatorio_1.Entidades;
using Obligatorio_1.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTOs
{
    public class MaintenanceDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string NameWorker { get; set; }
        public int CabinId { get; set; }
      

        public MaintenanceDto() { }
        public MaintenanceDto(int id, DateTime date, string description, double cost, string nameWorker, int cabinId) 
        {
            this.Id = id;
            this.Date = date;
            this.Description = description;
            this.Cost = cost;
            this.NameWorker = nameWorker;
            this.CabinId = cabinId;
        }
        public MaintenanceDto(Maintenance m) 
        {
            this.Id = m.Id;
            this.Date = m.Date;
            this.Description = m.Description;
            this.Cost = m.Cost;
            this.NameWorker = m.NameOfWorker.Value;
            this.CabinId = m.CabinId;

        }

    }
}
