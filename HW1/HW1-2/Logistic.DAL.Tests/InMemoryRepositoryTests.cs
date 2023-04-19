using AutoFixture.Xunit2;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.Models.Interfaces;
using NSubstitute;

namespace Logistic.DAL.Tests
{
    public class InMemoryRepositoryTests
    {
        private InMemoryRepository<Vehicle> repository;
        public InMemoryRepositoryTests()
        {
            repository = new InMemoryRepository<Vehicle>();
        }

        [Theory]
        [AutoData]
        public void Create_WhenValidVehicle_AddsVehicleToRepository(Vehicle vehicle)
        {
            // Arrange
            vehicle.Id = 1;

            // Act
            repository.Create(vehicle);

            // Assert
            var actualVehicle = repository.entities.FirstOrDefault(v => v.Id == vehicle.Id);
            Assert.Equal(vehicle.Number, actualVehicle.Number);
            Assert.Equal(vehicle.Cargoes.Count(), actualVehicle.Cargoes.Count());
        }

        [Theory]
        [AutoData]
        public void Delete_WhenVehicleExist_RemoveFromRepository(Vehicle vehicle)
        {
            // Arrange
            repository.Create(vehicle);
            vehicle.Id = 1;

            // Act
            repository.Delete(vehicle.Id);

            // Assert
            var actualVehicle = repository.entities.FirstOrDefault(v => v.Id == vehicle.Id);
            Assert.Equal(default, actualVehicle);
        }

        [Theory]
        [AutoData]
        public void Delete_WhenVehicleDoesntExist_ThrowException(Vehicle vehicle)
        {
            // Arrange
            vehicle.Id = 1;

            // Act + Assert
            var ex = Assert.Throws<Exception>(() => repository.Delete(vehicle.Id));
            Assert.Equal(ex.Message, $"Vehicle entity with this Id doesn't exist\t[Delete operation | id = {vehicle.Id}]");
        }

        [Theory]
        [AutoData]
        public void Read_WhenVehicleExist_ReadFromRepository(Vehicle vehicle)
        {
            // Arrange
            repository.Create(vehicle);
            vehicle.Id = 1;

            // Act
            var actualVehicle = repository.Read(vehicle.Id);

            // Assert
            Assert.Equal(vehicle.Number, actualVehicle.Number);
            Assert.Equal(vehicle.Cargoes.Count(), actualVehicle.Cargoes.Count());
        }

        [Theory]
        [AutoData]
        public void Read_WhenVehicleDoesntExist_ThrowException(Vehicle vehicle)
        {
            // Arrange
            vehicle.Id = 1;

            // Act + Assert
            var ex = Assert.Throws<Exception>(() => repository.Read(vehicle.Id));
            Assert.Equal(ex.Message, $"Vehicle entity with this Id doesn't exist\t[Read by id operation | id = {vehicle.Id}]");
        }

        [Theory, AutoData]
        public void ReadAll_WhenVehicleListValid_ReadVehicles(Vehicle vehicle1, Vehicle vehicle2)
        {
            // Arrange 
            repository.Create(vehicle1);
            repository.Create(vehicle2);
            vehicle1.Id = 1;
            vehicle2.Id = 2;

            // Act 
            var actualList = repository.ReadAll();

            // Assert
            Assert.Equal(vehicle1.Number, actualList[0].Number);
            Assert.Equal(vehicle1.Cargoes.Count(), actualList[0].Cargoes.Count());

            Assert.Equal(vehicle2.Number, actualList[1].Number);
            Assert.Equal(vehicle2.Cargoes.Count(), actualList[1].Cargoes.Count());
        }

        [Theory, AutoData]
        public void Update_WhenVehicle2UpdateExist_UpdateVehicle(Vehicle vehicle2update, Vehicle newVehicle)
        {
            // Arrange 
            repository.Create(vehicle2update);
            vehicle2update.Id = 1;

            // Act 
            repository.Update(vehicle2update.Id, newVehicle);

            // Assert
            var actualVehicle = repository.Read(vehicle2update.Id);
            Assert.Equal(newVehicle.Number, actualVehicle.Number);
            Assert.Equal(newVehicle.Cargoes.Count(), actualVehicle.Cargoes.Count());
        }
    }
}