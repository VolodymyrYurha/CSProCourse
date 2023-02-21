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
        {
            var WeightLeft = GetCargoWeightLeft();
            return $"Vehicle has {WeightLeft} kg space";
        }

        // Cargoes Volumes methods
        public float GetCargoVolumeCurrent()
        {
            float cargoVolume = 0;
            foreach(var cargo in Cargoes)
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
            return "Vehicle has ... m³ volume space";
        }

        // Vehicle Information
        public string GetInformation()
        {
            string s = new string(' ', 20) + "Vehicle info" + new string(' ', 20) + '\n';
            s += new string('-', 20 + 12 + 20) + '\n';
            s += $"Vehicle type: {Type}\t Number: {Number}\t .........."; //Have to add all info
            return s;
        }

        public void LoadCargo(Cargo cargo)
        {
            // Here I'll load a new one cargo
        }
    }
}
