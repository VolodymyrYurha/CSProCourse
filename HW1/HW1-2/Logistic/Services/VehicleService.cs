using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper.Execution;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories.Interfaces;
using Logistic.ConsoleClient.Services.Interfaces;

namespace Logistic.ConsoleClient.Services
{
    public class VehicleService : IServiceLoading<Vehicle>
    {
        private IRepositoryInMemory<Vehicle> repository;

        public VehicleService(IRepositoryInMemory<Vehicle> repository)
        {
            this.repository = repository;
        }

        public void Create(Vehicle entity)
        {
            repository.Create(entity);
        }

        public void Delete(int id)
        {
            repository.Delete(id);
        }

        public List<Vehicle> GetAll()
        {
            return repository.ReadAll();
        }

        public Vehicle GetById(int id)
        {
            return repository.Read(id);
        }

        public string GetVehicleInformation(int id)
        {
            var vehicle = repository.Read(id);

            string s = new string('_', 20 + 12 + 20) + '\n';
            s += new string(' ', 20) + "Vehicle info" + new string(' ', 20) + '\n';
            s += $"Vehicle type: {vehicle.Type}\t Number: {vehicle.Number}\n";
            s += $"Max. weight:  {vehicle.MaxCargoWeightKg} kg.   ( {vehicle.MaxCargoWeightLbs} lbs. )\n";
            s += $"Max. volume:  {vehicle.MaxCargoVolume} cub. m.\n";
            s += new string('.', 20 + 12 + 20) + '\n';
            s += new string(' ', 15) + "Loaded cargoes info:\n";
            s += $"Cargoes number: {vehicle.Cargoes.Count} u.\n";
            s += $"Summary weight: {vehicle.CargoWeightCurrent} kg.\n";
            s += $"Summary volume: {vehicle.CargoVolumeCurrent} cub. m.\n";
            s += new string('_', 20 + 12 + 20) + '\n';
            return s;
        }

        public void LoadCargo(Cargo cargo, int idVehicle)
        {
            if (cargo.Weight > GetCargoWeightLeft(idVehicle))
            {
                throw new Exception("[Exception!]\tCargo's too heavy. [ " + cargo.GetInformation() + " ]\n" + GetVehicleInformation(idVehicle));
            }

            if (cargo.Volume > GetCargoVolumeLeft(idVehicle))
            {
                throw new Exception("[Exception!]\tCargo's too voluminous. [ " + cargo.GetInformation() + " ]\n" + GetVehicleInformation(idVehicle));
            }

            var vehicleToLoad = repository.Read(idVehicle);
            vehicleToLoad.Cargoes.Add(cargo);
            vehicleToLoad.CargoWeightCurrent += cargo.Weight;
            vehicleToLoad.CargoVolumeCurrent += cargo.Volume;
            repository.Update(idVehicle, vehicleToLoad);
        }

        public void UnloadCargo(Guid guidCargo, int idVehicle)
        {
            var vehicleToUnload = repository.Read(idVehicle);
            var cargoToDelete = vehicleToUnload.Cargoes.FirstOrDefault(c => c.Id == guidCargo);

            if (cargoToDelete == null)
            {
                throw new Exception($"There is no cargo with id {guidCargo}");
            }

            vehicleToUnload.Cargoes.Remove(cargoToDelete);
            vehicleToUnload.CargoWeightCurrent -= cargoToDelete.Weight;
            vehicleToUnload.CargoVolumeCurrent -= cargoToDelete.Volume;
            repository.Update(idVehicle, vehicleToUnload);
        }

        // Private methods
        // Methods for getting info about certain vehicle by its id
        private float GetCargoWeightLeft(int id)
        {
            var vehicle = repository.Read(id);
            return vehicle.MaxCargoWeightKg - vehicle.CargoWeightCurrent;
        }

        private string GetCargoWeightLeftInformation(int id)
        {
            return $"Vehicle [ id : {id} ] has {GetCargoWeightLeft(id)} kg. space";
        }

        private float GetCargoVolumeLeft(int id)
        {
            var vehicle = repository.Read(id);
            return vehicle.MaxCargoVolume - vehicle.CargoVolumeCurrent;
        }

        private string GetCargoVolumeLeftInformation(int id)
        {
            return $"Vehicle [ id : {id} ] has {GetCargoVolumeLeft(id)} cub. m. volume space";
        }

    }
}
