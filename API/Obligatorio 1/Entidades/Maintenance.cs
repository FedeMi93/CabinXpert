using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using Obligatorio_1.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Obligatorio_1.Entidades
{
    public class Maintenance : IValidable
    {        
        public int Id { get; set; }
        public DateTime Date { get; set; }

        [StringLength(200, MinimumLength = 10, ErrorMessage = "La descripción del tipo de cabaña puede ser entre 10 y 200 caracteres.")]
        public string Description { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "El costo debe ser mayor a 0.")]
        public double Cost { get; set; }
        [Required]
        public NameWorker NameOfWorker { get; set; }
        [ForeignKey(nameof(Cabin))] public int CabinId { get; set; }
        public Cabin Cabin { get; set; }

        

        public Maintenance() { }
        public Maintenance(int id, DateTime date, string description, double cost, NameWorker nameOfWorker, Cabin cabin)
        {
            Id = id;
            Date = date;
            Description = description;
            Cost = cost;
            NameOfWorker = nameOfWorker;
            Cabin = cabin;
            
        }
      

        public void IsValid()
        {
            if(Date == null)throw new CabinException ("Falta ingresar la fecha del mantenimiento.");
            if (string.IsNullOrEmpty(Description)) throw new CabinException("Falto ingresar la descripcion del mantenimiento.");            
            if (this.Cost < 0) throw new CabinException("EL costo del mantenimiento no puede ser menor a 0.");
            NameOfWorker.IsValid();
            ValidDate();
        }
        private void ValidDate() 
        {
            DateTime currentDate = DateTime.Now;
            if (Date.Date > currentDate.Date) 
            {
                throw new CabinException("La fecha ingresada supera al día actual.");
            }
        }
    }
}
