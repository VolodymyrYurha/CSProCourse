using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models.Interfaces;

namespace Logistic.ConsoleClient.Models
{
    public class Warehouse : IEntity
    {
        public int Id { get; set; }

        public List<Cargo>? Stock { get; set; }
    }
}
