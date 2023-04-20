using AutoFixture.Xunit2;
using FluentAssertions;
using FluentAssertions.Execution;
using Logistic.Models;

namespace Logistic.DAL.Tests
{
    public class XmlRepositoryTests : Xunit.IClassFixture<XmlRepository<Vehicle>>
    {
        XmlRepository<Vehicle> repository;
        public XmlRepositoryTests(XmlRepository<Vehicle> repo)
        {
            repository = repo;
        }

        [Fact]
        public void Read_WhenFileNameValid_ReadVehicleList()
        {
            // Arrange
            string filename = "Vehicle_2023-03-24_17-09-05.xml";

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
            string createdXmlName = repository.Create(vehicles2Serialize);
            var actualList = repository.Read(createdXmlName);

            // Assert
            using (new AssertionScope())
            {
                actualList.Should().BeEquivalentTo(vehicles2Serialize);
            }
        }
    }
}
