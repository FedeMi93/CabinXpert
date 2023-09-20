using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationObligatorio.Models
{
    public class CabinModel
    {
        public int Id { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool JacuzziPriv { get; set; }
        public bool EnabledReservation { get; set; }
        public int NuRoom { get; set; }
        public int Capacity { get; set; }
        public string Picture { get; set; }
    }
}
