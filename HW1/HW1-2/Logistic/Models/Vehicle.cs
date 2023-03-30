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
    public class Vehicle : IEntity
    {
        public int Id { get; set; }

        public VehicleType Type { get; set; }

        public string Number { get; set; }

        public int MaxCargoWeightKg { get; set; }

        public float? MaxCargoWeightLbs { get; set; }

        public float MaxCargoVolume { get; set; }

        public List<Cargo>? Cargoes { get; set; } = null;

        public float CargoWeightCurrent { get; set; } = 0;

        public float CargoVolumeCurrent { get; set; } = 0;

        private const float KoefKgToLbs = 2.2046f;

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
            string space20 = new string(' ', 20);
            string underscore52 = new string('_', 52);
            string dots52 = new string('.', 52);

            StringBuilder outputString = new StringBuilder(underscore52 + '\n');
            outputString.Append(space20 + "Vehicle info" + space20 + '\n');
            outputString.Append($"Vehicle type: {Type}\t ID: {Id}\n");
            outputString.Append($"Number: {Number}\n");
            outputString.Append($"Max. weight:  {MaxCargoWeightKg} kg.   ( {MaxCargoWeightLbs} lbs. )\n");
            outputString.Append($"Max. volume:  {MaxCargoVolume} cub. m.\n");
            outputString.Append(dots52 + '\n');
            outputString.Append(space20 + "Loaded cargoes info:\n");

            if (Cargoes.Count() == 0)
            {
                outputString.Append("there are no cargoes");
            }

            foreach (var cargo in Cargoes)
            {
                outputString.Append(cargo.ToString() + "\n");
            }

            return outputString.ToString();
        }
    }
}
