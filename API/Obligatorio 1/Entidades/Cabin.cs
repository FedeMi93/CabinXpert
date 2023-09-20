using Microsoft.EntityFrameworkCore;
using Obligatorio_1.Exceptions;
using Obligatorio_1.Interfaces;
using Obligatorio_1.ValueObjects;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;


namespace Obligatorio_1.Entidades
{

    [Index(nameof(Name), IsUnique = true)]
    [Index(nameof(NuRoom), IsUnique = true)]
    public class Cabin : IValidable
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [ForeignKey(nameof(Type))] public int TypeId { get; set; }
        public CabinType Type { get; set; }

        [Required]
        public string Name { get; set; }

        [StringLength(500, MinimumLength = 10, ErrorMessage = "El largo tiene que ser mayor a 10 y menor a 500 caracteres")]

        public string Description { get; set; }

        public bool JacuzziPriv { get; set; }

        public bool EnabledReservation { get; set; }

        public int NuRoom { get; set; }

        public Capacity Capacity { get; set; }
        public string Picture { get; set; }

        public Cabin() { }

        public Cabin(int id, CabinType type, string name, string description, bool jacuzziPriv, bool enabledReservation, int nuRoom, Capacity capacity)
        {
            Id = id;
            Type = type;
            Name = name;
            Description = description;
            JacuzziPriv = jacuzziPriv;
            EnabledReservation = enabledReservation;
            NuRoom = nuRoom;
            Capacity = capacity;           
        }


        public void IsValid()
        {
            ValidName();
            Capacity.IsValid();

        }

        private void ValidName()
        {
            Regex regex = new Regex("^(?!\\s)([A-Za-z ]*?)(?<!\\s)$");
            bool meetsExpression = regex.IsMatch(Name);

            if (!meetsExpression)
            {
                throw new CabinException("El nombre solo debe incluir caracteres alfabéticos y espacios embebidos.");
            }
        }
        public string CreatePictureName()
        {
            char[] pictureNameChars = Name.ToCharArray();           
            char toSearch = ' ';
            char toReplace = '_';

            for (int i = 0; i < pictureNameChars.Length; i++)
            {
                if (pictureNameChars[i] == toSearch)
                {
                    pictureNameChars[i] = toReplace;
                }
            }
            string pictureName = new string(pictureNameChars);
            pictureName = pictureName + "_001";
            return pictureName;
        }

    }
}
