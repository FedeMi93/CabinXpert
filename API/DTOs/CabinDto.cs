using Obligatorio_1.Entidades;
using Obligatorio_1.ValueObjects;

namespace DTOs
{
    public class CabinDto
    {
        public int Id { get; set; }
        public int typeId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool JacuzziPriv { get; set; }
        public bool EnabledReservation { get; set; }
        public int NuRoom { get; set; }
        public int Capacity { get; set; }
        public string Picture { get; set; }

        public CabinDto() { }
        public CabinDto(int id, int typeId, string name, string description, bool jacuzziPriv, bool enabledReservation, int nuRoom, int capacity, string picture)
        {
            Id = id;
            this.typeId = typeId;
            Name = name;
            Description = description;
            JacuzziPriv = jacuzziPriv;
            EnabledReservation = enabledReservation;
            NuRoom = nuRoom;
            Capacity = capacity;
            Picture = picture;
        }
        public CabinDto(Cabin c) 
        {
            Id = c.Id;
            this.typeId = c.TypeId;
            Name = c.Name;
            Description = c.Description;
            JacuzziPriv = c.JacuzziPriv;
            EnabledReservation= c.EnabledReservation;
            NuRoom = c.NuRoom;
            Capacity = c.Capacity.Value;
            Picture = c.Picture;
        }
    }
}