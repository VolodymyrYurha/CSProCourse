using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;

Console.WriteLine("\t\tVehicle Loading application");

Main();

void Main()
{
    try
    {
        var repoJsonWarehouse = new JsonRepository<Warehouse>();

        var repoDeserialized = repoJsonWarehouse.Read("Warehouse_2023-03-22_15-57-04.json");
        var repoInMemoryWarehouse = new InMemoryRepository<Warehouse>(repoDeserialized);
        var warehouseService = new WarehouseService(repoInMemoryWarehouse);
        var allWarehouses = warehouseService.GetAll();

        //warehouseService.Create(new Warehouse());
        //warehouseService.Create(new Warehouse());
        //warehouseService.Create(new Warehouse());

        //warehouseService.LoadCargo(new Cargo(100, 100, "#1001"), 1);
        //warehouseService.LoadCargo(new Cargo(200, 200, "#1002"), 1);
        //warehouseService.LoadCargo(new Cargo(300, 300, "#1003"), 1);

        //warehouseService.LoadCargo(new Cargo(50, 50, "#2001"), 2);
        //warehouseService.LoadCargo(new Cargo(100, 100, "#2002"), 2);
        //warehouseService.LoadCargo(new Cargo(150, 150, "#2003"), 2);

        //warehouseService.LoadCargo(new Cargo(100, 200, "#3001"), 3);
        //warehouseService.LoadCargo(new Cargo(200, 400, "#3002"), 3);
        //warehouseService.LoadCargo(new Cargo(300, 600, "#3003"), 3);
        //warehouseService.LoadCargo(new Cargo(1000000, 6000000, "This will be deleted"), 3);
        warehouseService.UnloadCargo(Guid.Parse("d05de88b-d56a-4a43-9601-2be71708631a"), 3);
        allWarehouses = warehouseService.GetAll();
        //repoJsonWarehouse.Create(allWarehouses);
        ;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}