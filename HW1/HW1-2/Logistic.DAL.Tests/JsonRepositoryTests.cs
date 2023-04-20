using AutoFixture.Xunit2;
using FluentAssertions;
using FluentAssertions.Execution;
using Logistic.Models;

namespace Logistic.DAL.Tests
{
    public class JsonRepositoryTests
    {
        JsonRepository<Vehicle> repository;
        public JsonRepositoryTests()
        {
            repository = new JsonRepository<Vehicle>();
        }

        [Fact]
        public void Read_WhenFileNameValid_ReadVehicleList()
        {
            // Arrange
            string filename = "Vehicle_2023-03-24_17-09-18.json";

            // Act 
            var actualList = repository.Read(filename);

            // Assert
            Assert.Equal(2, actualList.Count());

            Assert.Equal(1, actualList[0].Id);
            Assert.Equal("4444", actualList[0].Number);
            Assert.Equal(2, actualList[0].Cargoes.Count());

            Assert.Equal(2, actualList[1].Id);
            Assert.Equal("1002", actualList[1].Number);
            Assert.Equal(3, actualList[1].Cargoes.Count());

        }

        [Theory]
        [AutoData]
        public void Create_WhenValidList_CreateJSON(List<Vehicle> vehicles2Serialize)
        {
            // Arrange

            // Act
            string createdJsonName = repository.Create(vehicles2Serialize);
            var actualList = repository.Read(createdJsonName);

            // Assert
            using (new AssertionScope())
            {
                actualList.Should().BeEquivalentTo(vehicles2Serialize);
            }
        }
    }
}
