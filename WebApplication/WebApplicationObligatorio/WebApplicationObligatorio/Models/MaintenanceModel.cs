using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationObligatorio.Models
{
    public class MaintenanceModel
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public string NameWorker { get; set; }
        public int CabinId { get; set; }
        
    }
}
