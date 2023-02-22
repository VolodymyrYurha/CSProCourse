using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Models
{
    // Vehicle class with all its properties and methods
    internal class Vehicle
    {
        public VehicleType Type { get; set; }

        public string? Number { get; set; }

        public int MaxCargoWeightKg { get; set; }

        public float? MaxCargoWeightLbs { get; set; }

        // Max cargo volume in Cubic meters
        public float MaxCargoVolume { get; set; }

        public List<Cargo>? Cargoes { get; set; }

        // Class Constructors
        public Vehicle(VehicleType type, int maxCargoWeightKg, float maxCargoVolume)
        {
            Type = type;
            MaxCargoWeightKg = maxCargoWeightKg;
            MaxCargoVolume = maxCargoVolume;
            Cargoes = new List<Cargo>(100);
            MaxCargoWeightLbs = MaxCargoWeightKg * 2.2046f;
        }

        // Class Methods

        // Cargoes Weight methods
        // Реалізував методи поетапно, оскільки потім їх використовую і в інших функціях
        public float GetCargoWeightCurrent()
        {
            float cargoWeight = 0;
            foreach (var cargo in Cargoes)
            {
                cargoWeight += cargo.Weight;
            }

            return cargoWeight;
        }

        // Returns Weight in KG
        public float GetCargoWeightLeft()
        {
            return MaxCargoWeightKg - GetCargoWeightCurrent();
        }

        public string GetCargoWeightLeftInformation()
        // В завданні ця функція називалась GetCargoWeightLeft(), очікувалось щоб вона виводила інформацію,
        // проте для універсальності методів GetCargoWeightLeft() виводить числове значення, а повідомлення - GetCargoWeightLeftInformation()
        {
            return $"Vehicle has {GetCargoWeightLeft()} kg. space";
        }

        // Cargoes Volumes methods
        public float GetCargoVolumeCurrent()
        {
            float cargoVolume = 0;
            foreach (var cargo in Cargoes)
            {
                cargoVolume += cargo.Volume;
            }

            return cargoVolume;
        }

        public float GetCargoVolumeLeft()
        {
            return MaxCargoVolume - GetCargoVolumeCurrent();
        }

        public string GetCargoVolumeLeftInformation()
        {
            return $"Vehicle has {GetCargoVolumeLeft()} cub. m. volume space";
        }

        // Vehicle Information
        public string GetInformation()
        {
            string s = new string('_', 20 + 12 + 20) + '\n';
            s += new string(' ', 20) + "Vehicle info" + new string(' ', 20) + '\n';
            s += new string('.', 20 + 12 + 20) + '\n';
            s += $"Vehicle type: {Type}\t Number: {Number}\n";
            s += $"Max. weight:  {MaxCargoWeightKg} kg.   ( {MaxCargoWeightLbs} lbs. )\n";
            s += $"Max. volume:  {MaxCargoVolume} cub. m.\n";
            s += new string('.', 20 + 12 + 20) + '\n';
            s += new string(' ', 15) + "Loaded cargoes info:\n";
            //s += new string('.', 20 + 12 + 20) + '\n';
            s += $"Cargoes number: {Cargoes.Count} u.\n";
            s += $"Summary weight: {GetCargoWeightCurrent()} kg.\n";
            s += $"Summary volume: {GetCargoVolumeCurrent()} cub. m.\n";
            s += new string('_', 20 + 12 + 20) + '\n';
            return s;
        }

        // Here I load a new one cargo
        public void LoadCargo(Cargo cargo)
        {
            // Exceptions displays what cargo can't be loaded & Vehicle loading state
            if (cargo.Weight > GetCargoWeightLeft())
            {
                throw new Exception("[Exception!]\tCargo's too heavy. [ " + cargo.GetInformation() + " ]\n" + GetInformation());
            }
            else if (cargo.Volume > GetCargoVolumeLeft())
            {
                throw new Exception("[Exception!]\tCargo's too voluminous. [ " + cargo.GetInformation() + " ]\n" + GetInformation());
            }
            else
            {
                // If cargo doesn't overload vehicle, It is added to vehicle storage
                Cargoes.Add(cargo);
            }
        }
    }
}
