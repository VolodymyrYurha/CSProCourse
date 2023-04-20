using AutoFixture.Xunit2;
using Logistic.DAL;
using Logistic.Models;
using Logistic.Models.Enums;
using NSubstitute;

namespace Logistic.Core.Tests
{
    public class ReportServiceTests
    {
        private JsonRepository<Vehicle> jsonRepository;
        private XmlRepository<Vehicle> xmlRepository;
        private ReportService<Vehicle> service;
        public ReportServiceTests()
        {
            jsonRepository = Substitute.For<JsonRepository<Vehicle>>();
            xmlRepository = Substitute.For<XmlRepository<Vehicle>>();
            service = new ReportService<Vehicle>(jsonRepository, xmlRepository);
        }

        [Theory]
        [AutoData]
        public void CreateReport_WhenCreateJSONReport_SummonJSONRepositoryCreate(List<Vehicle> Vehicles)
        {
            // Arrange
            ReportType type = ReportType.Json;

            // Act
            service.CreateReport(type, Vehicles);

            // Assert
            jsonRepository.Received(1).Create(Vehicles);
        }

        [Theory]
        [AutoData]
        public void CreateReport_WhenCreateXMLReport_SummonXMLRepositoryCreate(List<Vehicle> Vehicles)
        {
            // Arrange
            ReportType type = ReportType.Xml;

            // Act
            service.CreateReport(type, Vehicles);

            // Assert
            xmlRepository.Received(1).Create(Vehicles);
        }

        [Fact]
        public void LoadReport_WhenExistJsonFile_OpensSuccessfuly()
        {
            // Arrange
            string filename = "Vehicle_2023-03-24_17-09-18.json";

            // Act
            service.LoadReport(filename);

            // Assert
            jsonRepository.Received(1).Read(filename);
        }

        [Fact]
        public void LoadReport_WhenExistXMLFile_OpensSuccessfuly()
        {
            // Arrange
            string filename = "Vehicle_2023-03-24_17-09-05.xml";

            // Act
            service.LoadReport(filename);

            // Assert
            xmlRepository.Received(1).Read(filename);
        }

        [Fact]
        public void LoadReport_WhenNotExistFile_ThrowException()
        {
            // Arrange
            string filename = "not existing file.json";

            // Act + Assert
            Assert.Throws<FileNotFoundException>(() => service.LoadReport(filename));
        }
    }
}
