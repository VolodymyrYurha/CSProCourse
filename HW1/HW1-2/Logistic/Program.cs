// See https://aka.ms/new-console-template for more information
using Logistic.ConsoleClient.Models;

Console.WriteLine("\t\tVehicle Loading application");

main();

// Functions:
void main()
{
    // Тут обидві функції перевіряються на можливість виконання, одна з яких успішно виконається,
    // а іншу відловить обробник помилок та виведе відповідне повідомлення помилки
    try
    {
        SuccessScenario();
        ExceptionScenario();
    }
    catch(Exception e)
    {
        Console.WriteLine(e.Message);
    }
}

void SuccessScenario()
{
    Console.WriteLine("Success Scenario:");

    // I    Створіть транспортний засіб Vehicle (дані на ваш розсуд)
    var vehicle = new Vehicle(VehicleType.Car, 1000, 2999.99f);
    vehicle.Number = "BC 7777 BO";

    // II   Виведіть інформацію GetInformation по транспортному засобу на консоль
    Console.WriteLine("Vehicle before loading:");
    Console.WriteLine(vehicle.GetInformation());

    // III  Створіть масив cargos з декількома (3-5) new Cargo
    var cargoes = new List<Cargo>
    {
        new Cargo(100,  99.9f, "#0001AA"),
        new Cargo(150, 199.9f, "#0001AB"),
        new Cargo(200, 299.9f, "#0002AA"),
        new Cargo(250, 399.9f, "#0003AA"),
        new Cargo(300, 499.9f, "#0001AC"),
    };

    // IV   У циклі додайте усі Cargo з массиву cargos до Vehicle за допомогою методу LoadCargo (Без переповнення)
    foreach(var cargo in cargoes)
    {
        vehicle.LoadCargo(cargo);
    }

    // V    Виведіть інформацію GetInformation по Vehicle на консоль
    Console.WriteLine("Vehicle after loading:");
    Console.WriteLine(vehicle.GetInformation());
}

void ExceptionScenario()
{
    Console.WriteLine("Exception Scenario:");

    // I    Створіть транспортний засіб Vehicle (дані на ваш розсуд)
    var vehicle = new Vehicle(VehicleType.Train, 100_000, 500_000.00f);
    vehicle.Number = "BC 0666 BO";

    // II   Виведіть інформацію GetInformation по транспортному засобу на консоль
    Console.WriteLine("Vehicle before loading:");
    Console.WriteLine(vehicle.GetInformation());

    // III  Створіть масив cargos з декількома (3-5) new Cargo
    var cargoes = new List<Cargo>
    {
        new Cargo(10000,  9900.9f, "#0001XA"),
        new Cargo(15000, 19900.9f, "#0001XB"),
        new Cargo(20000, 29900.9f, "#0002XA"),
        new Cargo(25000, 39900.9f, "#0003XA"),
        new Cargo(30000, 49900.9f, "#0001XC"),
        new Cargo(1,         2.0f, "#0001XS"),
    };

    // IV   У циклі додайте усі Cargo з массиву cargos до Vehicle за допомогою методу LoadCargo (Без переповнення)
    foreach (var cargo in cargoes)
    {
        vehicle.LoadCargo(cargo);
    }

    // V    Виведіть інформацію GetInformation по Vehicle на консоль
    // ПІСЛЯ ЗАВАНТАЖЕННЯ ОСТАННЬОГО ГРУЗУ ФУНКЦІЯ ВИКИНЕ ПОМИЛКУ, ТОМУ ЇЇ ВИКОНАННЯ ПРИПИНЕТЬСЯ
    // (ТОМУ ІНФО. ПРО НАВАНТАЖЕННЯ ТРАНСПОРТУ ПЕРЕНІС В САМЕ ПОВІДОМЛЕННЯ ПРО ПОМИЛКУ)
    Console.WriteLine("Vehicle after loading:");
    Console.WriteLine(vehicle.GetInformation());
}