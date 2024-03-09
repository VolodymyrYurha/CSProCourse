using Logistic.Models.Interfaces;

namespace Logistic.Models
{
    public class Warehouse : IEntity
    {
        public int Id { get; set; }

        public List<Cargo>? Cargoes { get; set; }

        public int CargoCount => Cargoes?.Count ?? 0;

        public override string ToString()
        {
            string outputString = new string('_', 20 + 12 + 20) + '\n';
            outputString += new string(' ', 20) + "Warehouse info" + new string(' ', 20) + '\n';
            outputString += $"Id: {Id}\n";
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
