using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories.Interfaces;
using Logistic.ConsoleClient.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.ConsoleClient.Services
{
    internal class VehicleService : IVehicleService
    {
        private IRepositorySerialize<Vehicle> repository;

        public VehicleService(IRepositorySerialize<Vehicle> repository)
        {
            this.repository = repository;
        }
    }
}
