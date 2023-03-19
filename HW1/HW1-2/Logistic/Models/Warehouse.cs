using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Models
{
    internal class Warehouse
    {
        public int Id { get; set; }

        public List<Cargo>? Stock { get; set; }
    }
}
