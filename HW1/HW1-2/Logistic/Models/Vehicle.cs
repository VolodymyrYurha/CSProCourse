using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ConstrainedExecution;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Models
{
    // Vehicle class with all its properties and methods
    internal class Vehicle
    {
        public int Id { get; set; }

        public VehicleType Type { get; set; }

        public string? Number { get; set; }

        public int MaxCargoWeightKg { get; set; }

        public float? MaxCargoWeightLbs { get; set; }

        // Max cargo volume in Cubic meters
        public float MaxCargoVolume { get; set; }

        public List<Cargo>? Cargoes { get; set; }

        // Private measures
        private float CargoWeightCurrent { get; set; }

        private float CargoVolumeCurrent { get; set; }

        // Magic numbers
        private const float KoefKgToLbs = 2.2046f;

        // Class Constructors
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

        // Class Methods

        // Cargoes Weight methods
        // Реалізував методи поетапно, оскільки потім їх використовую і в інших функціях

        // Returns Weight in KG
        public float GetCargoWeightLeft()
        {
            return MaxCargoWeightKg - CargoWeightCurrent;
        }

        public string GetCargoWeightLeftInformation()

        // В завданні ця функція називалась GetCargoWeightLeft(), очікувалось щоб вона виводила інформацію,
        // проте для універсальності методів GetCargoWeightLeft() виводить числове значення, а повідомлення - GetCargoWeightLeftInformation()
        {
            return $"Vehicle has {GetCargoWeightLeft()} kg. space";
        }

        // Cargoes Volumes methods
        public float GetCargoVolumeLeft()
        {
            return MaxCargoVolume - CargoVolumeCurrent;
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
            s += $"Vehicle type: {Type}\t Number: {Number}\n";
            s += $"Max. weight:  {MaxCargoWeightKg} kg.   ( {MaxCargoWeightLbs} lbs. )\n";
            s += $"Max. volume:  {MaxCargoVolume} cub. m.\n";
            s += new string('.', 20 + 12 + 20) + '\n';
            s += new string(' ', 15) + "Loaded cargoes info:\n";
            s += $"Cargoes number: {Cargoes.Count} u.\n";
            s += $"Summary weight: {CargoWeightCurrent} kg.\n";
            s += $"Summary volume: {CargoVolumeCurrent} cub. m.\n";
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

            if (cargo.Volume > GetCargoVolumeLeft())
            {
                throw new Exception("[Exception!]\tCargo's too voluminous. [ " + cargo.GetInformation() + " ]\n" + GetInformation());
            }

            Cargoes.Add(cargo);
            CargoWeightCurrent += cargo.Weight;
            CargoVolumeCurrent += cargo.Volume;
        }
    }
}
