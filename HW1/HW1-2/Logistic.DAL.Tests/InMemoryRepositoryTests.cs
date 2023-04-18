using AutoMapper.Execution;
using Logistic.Core;
using Logistic.DAL;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.Models.Interfaces;
using System.Xml;

namespace Logistic.DAL.Tests
{
    public class InMemoryRepositoryTests
    {
        [Fact]
        public void Create_WhenValidEntity_AddsEntityToRepo()
        {
            // Arrange
            var repo = new InMemoryRepository<Vehicle>();
            var vehicle = new Vehicle(VehicleType.Car, 1000, 1000.0f, "BC 1000 BO");
            var expectedList = new List<Vehicle>() { vehicle };

            // Act
            repo.Create(vehicle);
            var actualList = repo.ReadAll();

            // Assert
            Assert.Equal(expectedList.Count(), actualList.Count());
        }
    }
}