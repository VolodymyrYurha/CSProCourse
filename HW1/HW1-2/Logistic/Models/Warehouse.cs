using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Execution;
using Logistic.ConsoleClient.Models.Interfaces;

namespace Logistic.ConsoleClient.Models
{
    public class Warehouse : IEntity
    {
        public int Id { get; set; }

        public List<Cargo>? Stock { get; set; }

        public override string ToString()
        {
            string outputString = new string('_', 20 + 12 + 20) + '\n';
            outputString += new string(' ', 20) + "Warehouse info" + new string(' ', 20) + '\n';
            outputString += $"Id: {Id}\n";
            outputString += new string(' ', 15) + "Loaded cargoes info:\n";

            if (Stock.Count() == 0)
            {
                outputString += "there are no cargoes";
            }

            foreach (var cargo in Stock)
            {
                outputString += cargo.ToString() + "\n";
            }

            return outputString;
        }
    }
}
