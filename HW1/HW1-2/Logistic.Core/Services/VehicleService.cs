﻿using Logistic.Models;
using Logistic.DAL.Interfaces;
using Logistic.Core.Interfaces;
using Logistic.Models.Models.Exceptions;

namespace Logistic.Core
{
    public class VehicleService : IEntityLoadingService<Vehicle>
    {
        public IInMemoryRepository<Vehicle> repository;

        public VehicleService(IInMemoryRepository<Vehicle> repository)
        {
            this.repository = repository;
        }

        public Vehicle Create(Vehicle entity)
        {
            return repository.Create(entity);
        }

        public Vehicle Update(int id, Vehicle entity)
        {
            return repository.Update(id, entity);
        }

        public Vehicle Delete(int id)
        {
            return repository.Delete(id);
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

        public Cargo LoadCargo(Cargo cargo, int idVehicle)
        {
            var vehicleToLoad = repository.Read(idVehicle);

            if (cargo.Weight > GetCargoWeightLeft(vehicleToLoad))
            {
                throw new CustomException($"Cargo's too heavy. Weight: {cargo.Weight} kg. Space: {GetCargoWeightLeft(vehicleToLoad)} kg. Vehicle id: {idVehicle}");
            }

            if (cargo.Volume > GetCargoVolumeLeft(vehicleToLoad))
            {
                throw new CustomException($"Cargo's too voluminous. Volume: {cargo.Volume} cub. m. Space: {GetCargoVolumeLeft(vehicleToLoad)} cub. m. Vehicle id: {idVehicle}");
            }
            cargo.Id = Guid.NewGuid();
            vehicleToLoad.Cargoes.Add(cargo);
            vehicleToLoad.CargoWeightCurrent += cargo.Weight;
            vehicleToLoad.CargoVolumeCurrent += cargo.Volume;
            repository.Update(idVehicle, vehicleToLoad);
            return cargo;
        }

        public Cargo UnloadCargo(Guid guidCargo, int idVehicle)
        {
            var vehicleToUnload = repository.Read(idVehicle);
            var cargoToDelete = vehicleToUnload.Cargoes.FirstOrDefault(c => c.Id == guidCargo);

            if (cargoToDelete == null)
            {
                throw new CustomException($"There is no cargo with id  [ {guidCargo} ] in vehicle with id {vehicleToUnload.Id}");
            }

            vehicleToUnload.Cargoes.Remove(cargoToDelete);
            vehicleToUnload.CargoWeightCurrent -= cargoToDelete.Weight;
            vehicleToUnload.CargoVolumeCurrent -= cargoToDelete.Volume;
            repository.Update(idVehicle, vehicleToUnload);
            return cargoToDelete;
        }

        public Cargo UnloadLastCargo(int idVehicle)
        {
            var vehicleToUnload = repository.Read(idVehicle);
            if (!(vehicleToUnload.Cargoes.Count > 0))
            {
                throw new CustomException($"There isn't any cargo in vehicle with id {vehicleToUnload.Id}");
            }

            var cargoToDelete = vehicleToUnload.Cargoes[vehicleToUnload.Cargoes.Count - 1];
            vehicleToUnload.Cargoes.Remove(cargoToDelete);
            vehicleToUnload.CargoWeightCurrent -= cargoToDelete.Weight;
            vehicleToUnload.CargoVolumeCurrent -= cargoToDelete.Volume;
            repository.Update(idVehicle, vehicleToUnload);
            return cargoToDelete;
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
