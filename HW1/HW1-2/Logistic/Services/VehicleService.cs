using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Services
{
    internal class VehicleService
    {
        private IRepository<Vehicle> repository;

        public VehicleService(IRepository<Vehicle> repository)
        {
            this.repository = repository;
        }
    }
}
