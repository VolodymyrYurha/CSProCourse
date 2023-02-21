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
        // Volume in Cubic meters
        public float Volume { get; set; }

        // Weight in KG
        public int Weight { get; set; }

        // Cargo's code
        public string Code { get; set; }

        // Class Constuctors
        public Cargo()
        {

        }

        public Cargo(float volume, int weight, string code)
        {
            Volume = volume;
            Weight = weight;
            Code = code;
        }
    }
}
