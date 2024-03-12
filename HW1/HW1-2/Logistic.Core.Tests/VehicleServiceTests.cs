using AutoFixture.Xunit2;
using Logistic.DAL.Interfaces;
using Logistic.Models;
using Logistic.Models.Models.Exceptions;
using NSubstitute;

namespace Logistic.Core.Tests
{
    public class VehicleServiceTests
    {
        private IInMemoryRepository<Vehicle> repository;
        private VehicleService service;

        public VehicleServiceTests()
        {
            repository = Substitute.For<IInMemoryRepository<Vehicle>>();
            service = new VehicleService(repository);
        }

        [Theory]
        [AutoData]
        public void Create_WhenDataIsValid_CallsExpectedMethod(Vehicle vehicle)
        {
            // Arrange

            // Act
            service.Create(vehicle);

            // Assert
            repository.Received(1).Create(vehicle);
        }

        [Theory]
        [AutoData]
        public void Delete_WhenIDIsValid_CallsExpectedMethod(int id)
        {
            // Arrange

            // Act
            service.Delete(id);

            // Assert
            repository.Received(1).Delete(id);
        }

        [Theory]
        [AutoData]
        public void GetAll_WhenDataRead_EqualVehicleLists(List<Vehicle> vehicles)
        {
            // Arrange
            repository.ReadAll().Returns(vehicles);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Equal(vehicles, result);
        }

        [Theory]
        [AutoData]
        public void GetById_ReadVehicleWithMatchingId_EqualVehicles(Vehicle vehicle)
        {
            // Arrange
            repository.Read(vehicle.Id).Returns(vehicle);

            // Act
            var result = service.GetById(vehicle.Id);

            // Assert
            Assert.Equal(vehicle.Number, result.Number);
        }

        [Theory]
        [AutoData]
        public void GetVehicleInformation_ReturnsVehicleToString_EqualStrings(Vehicle vehicle)
        {
            // Arrange

            // Act
            var result = service.GetVehicleInformation(vehicle);

            // Assert
            Assert.Equal(vehicle.ToString(), result);
        }

        [Theory]
        [AutoData]
        public void LoadCargo_WhenCargoFits_CargoLoadedSuccessfully(Vehicle vehicle, Cargo cargo)
        {
            // Arrange
            vehicle.MaxCargoVolume = 1000;
            vehicle.MaxCargoWeightKg = 1000;
            vehicle.CargoVolumeCurrent = 0;
            vehicle.CargoWeightCurrent = 0;
            vehicle.Cargoes = new List<Cargo>();

            repository.Read(Arg.Any<int>()).Returns(vehicle);

            cargo.Weight = 1000;
            cargo.Volume = 1000;

            // Act
            service.LoadCargo(cargo, 1);

            // Assert
            Assert.Single(vehicle.Cargoes);
            Assert.Equal(cargo.Weight, vehicle.CargoWeightCurrent);
            Assert.Equal(cargo.Volume, vehicle.CargoVolumeCurrent);
            repository.Received(1).Update(Arg.Any<int>(), vehicle);
        }

        [Theory]
        [AutoData]
        public void LoadCargo_WhenCargoIsTooHeavy_ThrowHeavyException(Vehicle vehicle, Cargo cargo)
        {
            // Arrange
            vehicle.MaxCargoVolume = 1000;
            vehicle.MaxCargoWeightKg = 1000;
            vehicle.CargoVolumeCurrent = 0;
            vehicle.CargoWeightCurrent = 0;
            vehicle.Cargoes = new List<Cargo>();

            repository.Read(Arg.Any<int>()).Returns(vehicle);

            cargo.Weight = 1001;
            cargo.Volume = 1000;

            // Act + Assert
            var ex = Assert.Throws<CustomException>(() => service.LoadCargo(cargo, vehicle.Id));
            Assert.Equal(ex.Message, $"Cargo's too heavy. Weight: {cargo.Weight} kg. Space: {vehicle.MaxCargoWeightKg - vehicle.CargoWeightCurrent} kg. Vehicle id: {vehicle.Id}");
        }

        [Theory]
        [AutoData]
        public void LoadCargo_WhenCargoIsTooVoluminous_ThrowVolumeException(Vehicle vehicle, Cargo cargo)
        {
            // Arrange
            vehicle.MaxCargoVolume = 1000;
            vehicle.MaxCargoWeightKg = 1000;
            vehicle.CargoVolumeCurrent = 0;
            vehicle.CargoWeightCurrent = 0;
            vehicle.Cargoes = new List<Cargo>();

            repository.Read(Arg.Any<int>()).Returns(vehicle);

            cargo.Weight = 1000;
            cargo.Volume = 1001;

            // Act + Assert
            var ex = Assert.Throws<CustomException>(() => service.LoadCargo(cargo, vehicle.Id));
            Assert.Equal(ex.Message, $"Cargo's too voluminous. Volume: {cargo.Volume} cub. m. Space: {vehicle.MaxCargoVolume - vehicle.CargoVolumeCurrent} cub. m. Vehicle id: {vehicle.Id}");
        }

        [Theory]
        [AutoData]
        public void UnloadCargo_WhenCargoFits_CargoLoadedSuccessfully(Vehicle vehicle, Cargo cargo)
        {
            // Arrange
            vehicle.MaxCargoVolume = 1000;
            vehicle.MaxCargoWeightKg = 1000;
            vehicle.CargoVolumeCurrent = 0;
            vehicle.CargoWeightCurrent = 0;
            vehicle.Cargoes = new List<Cargo>();

            repository.Read(Arg.Any<int>()).Returns(vehicle);

            cargo.Weight = 100;
            cargo.Volume = 100;

            // Act
            service.LoadCargo(cargo, vehicle.Id);
            service.UnloadCargo(cargo.Id, vehicle.Id);

            // Assert
            Cargo cargoById = vehicle.Cargoes.FirstOrDefault(c => c.Id == cargo.Id);
            Assert.Equal(default(Cargo), cargoById);
        }

    }
}