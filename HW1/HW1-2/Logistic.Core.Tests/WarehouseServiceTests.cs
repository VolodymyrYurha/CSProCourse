using Logistic.DAL.Interfaces;
using Logistic.Models;
using NSubstitute;
using AutoFixture.Xunit2;

namespace Logistic.Core.Tests
{
    public class WarehouseServiceTests
    {
        private IInMemoryRepository<Warehouse> repository;
        private WarehouseService service;

        public WarehouseServiceTests()
        {
            repository = Substitute.For<IInMemoryRepository<Warehouse>>();
            service = new WarehouseService(repository);
        }

        [Theory]
        [AutoData]
        public void Create_WhenDataIsValid_CallsExpectedMethod(Warehouse warehouse)
        {
            // Arrange

            // Act
            service.Create(warehouse);

            // Assert
            repository.Received(1).Create(warehouse);
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
        public void GetAll_WhenDataRead_EqualWarehouseLists(List<Warehouse> warehouses)
        {
            // Arrange
            repository.ReadAll().Returns(warehouses);

            // Act
            var result = service.GetAll();

            // Assert
            Assert.Equal(warehouses, result);
        }

        [Theory]
        [AutoData]
        public void GetById_ReadWarehouseWithMatchingId_EqualWarehouses(Warehouse warehouse)
        {
            // Arrange
            repository.Read(warehouse.Id).Returns(warehouse);

            // Act
            var result = service.GetById(warehouse.Id);

            // Assert
            Assert.Equal(warehouse, result);
        }

        [Theory]
        [AutoData]
        public void LoadCargo_WhenCargoIsValid_AddsCargoToStockCallsExpectedMethod(Cargo cargo, Warehouse warehouse)
        {
            // Arrange
            repository.Read(warehouse.Id).Returns(warehouse);
            service.Create(warehouse);

            // Act
            service.LoadCargo(cargo, warehouse.Id);

            // Assert
            var loadedCargo = warehouse.Cargoes.FirstOrDefault(c => c.Id == cargo.Id);
            Assert.Equal(cargo, loadedCargo);
            repository.Received(1).Update(warehouse.Id, warehouse);
        }

        [Theory]
        [AutoData]
        public void UnloadCargo_WhenCargoInWarehouse_RemovesCargoCallsExpectedMethod(Cargo cargo, Warehouse warehouse)
        {
            // Arrange
            repository.Read(warehouse.Id).Returns(warehouse);
            warehouse.Cargoes.Add(cargo);
            service.Create(warehouse);

            // Act
            service.UnloadCargo(cargo.Id, warehouse.Id);

            // Assert
            var unloadedCargo = warehouse.Cargoes.FirstOrDefault(c => c.Id == cargo.Id);
            Assert.Equal(default, unloadedCargo);
            repository.Received(1).Update(warehouse.Id, warehouse);
        }

        [Theory]
        [AutoData]
        public void UnloadCargo_WhenCargoIsNotInWarehouse_ThrowsException(Cargo cargo, Warehouse warehouse)
        {
            // Arrange
            repository.Read(warehouse.Id).Returns(warehouse);
            service.Create(warehouse);

            // Act + Assert
            var ex = Assert.Throws<Exception>(() => service.UnloadCargo(cargo.Id, warehouse.Id));
            Assert.Equal(ex.Message, $"There is no cargo with id [ {cargo.Id} ] in warehouse with id {warehouse.Id}");

        }
    }
}
