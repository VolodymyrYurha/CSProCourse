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
    public class VehicleService : IEntityLoadingService<Vehicle>
    {
        private IInMemoryRepository<Vehicle> repository;

        public VehicleService(IInMemoryRepository<Vehicle> repository)
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

        public string GetVehicleInformation(Vehicle vehicle)
        {
            return vehicle.ToString();
        }

        public void LoadCargo(Cargo cargo, int idVehicle)
        {
            var vehicleToLoad = repository.Read(idVehicle);

            if (cargo.Weight > GetCargoWeightLeft(vehicleToLoad))
            {
                throw new ArgumentException($"Cargo's too heavy. Weight: {cargo.Weight} kg. Space: {GetCargoWeightLeft(vehicleToLoad)} kg. Vehicle id: {idVehicle}");
            }

            if (cargo.Volume > GetCargoVolumeLeft(vehicleToLoad))
            {
                throw new ArgumentException($"Cargo's too voluminous. Volume: {cargo.Volume} cub. m. Space: {GetCargoVolumeLeft(vehicleToLoad)} cub. m. Vehicle id: {idVehicle}");
            }

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
                throw new Exception($"There is no cargo with id  [ {guidCargo} ] in vehicle with id {vehicleToUnload.Id}");
            }

            vehicleToUnload.Cargoes.Remove(cargoToDelete);
            vehicleToUnload.CargoWeightCurrent -= cargoToDelete.Weight;
            vehicleToUnload.CargoVolumeCurrent -= cargoToDelete.Volume;
            repository.Update(idVehicle, vehicleToUnload);
        }

        public void UnloadLastCargo(int idVehicle)
        {
            var vehicleToUnload = repository.Read(idVehicle);
            if (!(vehicleToUnload.Cargoes.Count > 0))
            {
                throw new Exception($"There isn't any cargo in vehicle with id {vehicleToUnload.Id}");
            }

            var cargoToDelete = vehicleToUnload.Cargoes[vehicleToUnload.Cargoes.Count - 1];
            vehicleToUnload.Cargoes.Remove(cargoToDelete);
            vehicleToUnload.CargoWeightCurrent -= cargoToDelete.Weight;
            vehicleToUnload.CargoVolumeCurrent -= cargoToDelete.Volume;
            repository.Update(idVehicle, vehicleToUnload);
        }

        private float GetCargoWeightLeft(Vehicle vehicle)
        {
            return vehicle.MaxCargoWeightKg - vehicle.CargoWeightCurrent;
        }

        private float GetCargoVolumeLeft(Vehicle vehicle)
        {
            return vehicle.MaxCargoVolume - vehicle.CargoVolumeCurrent;
        }
    }
}
