using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models.Enums;
using Logistic.ConsoleClient.Models.Interfaces;

namespace Logistic.ConsoleClient.Models
{
    // Vehicle class with all its properties and methods
    public class Vehicle : IEntity
    {
        public int Id { get; set; }

        public VehicleType Type { get; set; }

        public string Number { get; set; } = string.Empty;

        public int MaxCargoWeightKg { get; set; }

        public float? MaxCargoWeightLbs { get; set; }

        // Max cargo volume in Cubic meters
        public float MaxCargoVolume { get; set; }

        public List<Cargo>? Cargoes { get; set; } = null;

        // Private measures
        // private float CargoWeightCurrent = 0;
        // private float CargoVolumeCurrent = 0;
        public float CargoWeightCurrent { get; set; } = 0;

        public float CargoVolumeCurrent { get; set; } = 0;

        // Magic numbers
        private const float KoefKgToLbs = 2.2046f;

        // Class Constructors
        public Vehicle()
        {
        }

        public Vehicle(VehicleType type, int maxCargoWeightKg, float maxCargoVolume, string number)
        {
            Type = type;
            MaxCargoWeightKg = maxCargoWeightKg;
            MaxCargoVolume = maxCargoVolume;
            Cargoes = new List<Cargo>();
            MaxCargoWeightLbs = MaxCargoWeightKg * KoefKgToLbs;
            Number = number;
            CargoWeightCurrent = 0;
            CargoVolumeCurrent = 0;
        }

        public override string ToString()
        {
            string outputString = new string('_', 20 + 12 + 20) + '\n';
            outputString += new string(' ', 20) + "Vehicle info" + new string(' ', 20) + '\n';
            outputString += $"Vehicle type: {Type}\t ID: {Id}\n";
            outputString += $"Number: {Number}\n";
            outputString += $"Max. weight:  {MaxCargoWeightKg} kg.   ( {MaxCargoWeightLbs} lbs. )\n";
            outputString += $"Max. volume:  {MaxCargoVolume} cub. m.\n";
            outputString += new string('.', 20 + 12 + 20) + '\n';
            outputString += new string(' ', 15) + "Loaded cargoes info:\n";

            if (Cargoes.Count() == 0)
            {
                outputString += "there are no cargoes";
            }

            foreach (var cargo in Cargoes)
            {
                outputString += cargo.ToString() + "\n";
            }

            return outputString;
        }
    }
}
