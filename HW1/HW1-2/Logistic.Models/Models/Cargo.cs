using System.Text;

namespace Logistic.Models
{
    public class Cargo
    {
        public Guid Id { get; set; }

        public int Weight { get; set; }

        public float Volume { get; set; }

        public string Code { get; set; }

        public Invoice? Invoice { get; set; }

        public Cargo(int weight, float volume, string code)
        {
            Id = Guid.NewGuid();
            Volume = volume;
            Weight = weight;
            Code = code;
        }

        public override string ToString()
        {
            string dots52 = new string('.', 52);

            StringBuilder outputString = new StringBuilder(dots52 + '\n');
            outputString.Append("Cargo id:\t" + Id.ToString() + "\n");
            outputString.Append( $"Weight:  {Weight} kg. | Volume:  {Volume} cub. m. | Code:  {Code}\n");
            outputString.Append(dots52 + '\n');
            return outputString.ToString();
        }
    }
}
