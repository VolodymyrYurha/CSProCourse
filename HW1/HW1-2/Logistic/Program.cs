using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Models.Enums;
using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;

Console.WriteLine("\t\tVehicle Loading application");

Main();

void Main()
{
    try
    {
        var repoJsonWarehouse = new JsonRepository<Warehouse>();
        var repoXmlWarehouse = new XmlRepository<Warehouse>();

        var serviceReportWarehouse = new ReportService<Warehouse>(repoJsonWarehouse, repoXmlWarehouse);

        var warehouseFromJson = serviceReportWarehouse.LoadReport("Warehouse_2023-03-22_15-57-04.json");
        //serviceReportWarehouse.CreateReport(ReportType.Xml, warehouseFromJson);

        var warehouseFromXml = serviceReportWarehouse.LoadReport("Warehouse_2023-03-22_17-37-09.xml");

        //var inMemoryRepo = new InMemoryRepository<Warehouse>(warehouseFromXml);

        //var repoDeserialized = repoJsonWarehouse.Read("Warehouse_2023-03-22_15-57-04.json");
        //var repoInMemoryWarehouse = new InMemoryRepository<Warehouse>(repoDeserialized);
        //var warehouseService = new WarehouseService(repoInMemoryWarehouse);
        //var allWarehouses = warehouseService.GetAll();

    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}