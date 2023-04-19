using AutoFixture.Xunit2;
using Logistic.DAL.Interfaces;
using Logistic.Models;
using Logistic.Models.Enums;
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

        [Fact]
        public void Create_WhenDataIsValid_CallsExpectedMethod()
        {
            // Arrange
            var entity = new Vehicle(VehicleType.Car, 1000, 1000.0f, "BC 1000 BO");

            // Act
            service.Create(entity);

            // Assert
            repository.Received(1).Create(entity);
        }

        [Fact]
        public void Delete_WhenIDIsValid_CallsExpectedMethod()
        {
            // Arrange
            int id = 123;

            // Act
            service.Delete(id);

            // Assert
            repository.Received(1).Delete(id);
        }

        [Fact]
        public void GetAll_WhenDataRead_EqualVehicleLists()
        {
            // Arrange
            var expectedList = new List<Vehicle> { new Vehicle(), new Vehicle() };
            repository.ReadAll().Returns(expectedList);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Equal(expectedList, result);
        }

        [Fact]
        public void GetById_ReadVehicleWithMatchingId_EqualVehicles()
        {
            // Arrange
            int id = 123;
            var expectedVehicle = new Vehicle { Id = id };
            repository.Read(id).Returns(expectedVehicle);

            // Act
            var result = service.GetById(id);

            // Assert
            Assert.Equal(expectedVehicle, result);
        }

        [Fact]
        public void GetVehicleInformation_ReturnsVehicleToString_EqualStrings()
        {
            // Arrange
            var vehicle = new Vehicle(VehicleType.Car, 1000, 1000.0f, "BC 1000 BO");

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
            var ex = Assert.Throws<ArgumentException>(() => service.LoadCargo(cargo, vehicle.Id));
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
            var ex = Assert.Throws<ArgumentException>(() => service.LoadCargo(cargo, vehicle.Id));
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
            //var updatedVehicle = repository.Read(vehicle.Id);
            Cargo cargoById = vehicle.Cargoes.FirstOrDefault(c => c.Id == cargo.Id);
            Assert.Equal(default(Cargo), cargoById);
        }

    }
}