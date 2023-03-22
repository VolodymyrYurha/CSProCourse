// See https://aka.ms/new-console-template for more information
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;

Console.WriteLine("\t\tVehicle Loading application");

Main();

// Functions:
void Main()
{
    // Тут обидві функції перевіряються на можливість виконання, одна з яких успішно виконається,
    // а іншу відловить обробник помилок та виведе відповідне повідомлення помилки
    try
    {
        var repoXmlVehicle = new XmlRepository<Vehicle>();
        var vehiclesDeserialized = repoXmlVehicle.Read("Vehicle_2023-03-22_14-53-26.xml");
        var repoInMemoryVehicle = new InMemoryRepository<Vehicle>(vehiclesDeserialized);

        var vehicleService = new VehicleService(repoInMemoryVehicle);
        //vehicleService.Create(new Vehicle(VehicleType.Ship, 100_000, 200_000, "Service ship #0000"));
        //vehicleService.Create(new Vehicle(VehicleType.Ship, 200_000, 100_000, "Service ship #9999"));
        var serviceAllVehicles = vehicleService.GetAll();
        //vehicleService.LoadCargo(new Cargo(100, 100, "cargo #0000"), 5);
        //vehicleService.LoadCargo(new Cargo(200, 150, "cargo #1111"), 5);
        //vehicleService.LoadCargo(new Cargo(300, 200, "cargo #2222"), 5);
        //serviceAllVehicles = vehicleService.GetAll();
        vehicleService.UnloadCargo(Guid.Parse("b6dee030-8a7b-43f7-a4a5-eb0b1b539199"), 5);
        serviceAllVehicles = vehicleService.GetAll();
        //repoXmlVehicle.Create(serviceAllVehicles);

        //var vehicleSearch2 = repoInMemoryVehicle.Read(6);
        //repoXmlVehicle.Create(vehicleRead);
        ;
    }
    catch (Exception e)
    {
        Console.WriteLine(e.Message);
    }
}