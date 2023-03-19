using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Models
{
    // Cargo class for all cargo's properties
    internal class Cargo
    {
        public Guid Id { get; set; }

        // Weight in KG
        public int Weight { get; set; }

        // Volume in Cubic meters
        public float Volume { get; set; }

        // Cargo's code
        public string Code { get; set; }

        public Invoice? Invoice { get; set; }

        // Class Constuctors
        public Cargo(int weight, float volume, string code)
        {
            Id = Guid.NewGuid();
            Volume = volume;
            Weight = weight;
            Code = code;
        }

        // Class methods
        public string GetInformation()
        {
            return $"Cargo's ( {Code} ) measures: {Weight} kg. {Volume} cub. m.";
        }
    }
}
