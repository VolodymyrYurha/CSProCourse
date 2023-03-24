using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Models.Enums;
using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;

namespace Logistic.ConsoleClient
{
    public class AppInfrastructureBuilder
    {
        // Repositories
        private JsonRepository<Vehicle> repoJsonVehicle;
        private JsonRepository<Warehouse> repoJsonWarehouse;
        private XmlRepository<Vehicle> repoXmlVehicle;
        private XmlRepository<Warehouse> repoXmlWarehouse;
        private InMemoryRepository<Vehicle> repoIMVehicle;
        private InMemoryRepository<Warehouse> repoIMWarehouse;

        // Services
        private VehicleService serviceVehicle;
        private WarehouseService serviceWarehouse;
        private ReportService<Vehicle> serviceReportVehicle;
        private ReportService<Warehouse> serviceReportWarehouse;

        public AppInfrastructureBuilder()
        {
            repoJsonVehicle = new JsonRepository<Vehicle>();
            repoJsonWarehouse = new JsonRepository<Warehouse>();
            repoXmlVehicle = new XmlRepository<Vehicle>();
            repoXmlWarehouse = new XmlRepository<Warehouse>();
            repoIMVehicle = new InMemoryRepository<Vehicle>();
            repoIMWarehouse = new InMemoryRepository<Warehouse>();

            serviceVehicle = new VehicleService(repoIMVehicle);
            serviceWarehouse = new WarehouseService(repoIMWarehouse);
            serviceReportVehicle = new ReportService<Vehicle>(repoJsonVehicle, repoXmlVehicle);
            serviceReportWarehouse = new ReportService<Warehouse>(repoJsonWarehouse, repoXmlWarehouse);
        }

        public void CommandReader()
        {
            string inputCommand;
            while (true)
            {
                inputCommand = Console.ReadLine();
                if (inputCommand == "exit")
                {
                    return;
                }

                try
                {
                    List<string> commandAtributes = inputCommand.Split(' ').ToList();
                    CommandParser(commandAtributes);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }

        private void CommandParser(List<string> atributes)
        {
            var commandDeterminant = atributes[0];
            switch (commandDeterminant)
            {
                case "":
                    {
                        return;
                    }

                case "add":
                    {
                        CommandAdd(atributes);
                        break;
                    }

                case "get-all":
                    {
                        CommandGetAll(atributes);
                        break;
                    }

                case "load-cargo":
                    {
                        CommandLoadCargo(atributes);
                        break;
                    }

                default:
                    throw new Exception($"Uknown command \'{commandDeterminant}\'");
            }
        }

        // Commands implementators typical Exceptions
        private Exception exceptionNumberAtributes = new Exception("Incorrect atributes number");

        // Commands implementators
        private void CommandAdd(List<string> atributes)
        {
            if (atributes.Count != 2)
            {
                throw exceptionNumberAtributes;
            }

            string entityTypeToAdd = atributes[1];

            switch (entityTypeToAdd)
            {
                case "vehicle":
                    serviceVehicle.Create(VehicleFiller());
                    Console.WriteLine("New vehicle is successfully added in memory");
                    break;

                case "warehouse":
                    serviceWarehouse.Create(new Warehouse());
                    Console.WriteLine("New warehouse is successfully added in memory");
                    break;

                default:
                    throw new Exception($"Uknown entity type \'{entityTypeToAdd}\'");
            }
        }

        private void CommandGetAll(List<string> atributes)
        {
            if (atributes.Count != 2)
            {
                throw exceptionNumberAtributes;
            }

            string entityTypeToGet = atributes[1];

            switch (entityTypeToGet)
            {
                case "vehicle":
                    PrintVehicles(serviceVehicle.GetAll());
                    break;

                case "warehouse":
                    PrintWarehouses(serviceWarehouse.GetAll());
                    break;

                default:
                    throw new Exception($"Uknown entity type \'{entityTypeToGet}\'");
            }
        }

        private void CommandLoadCargo(List<string> atributes)
        {
            if (atributes.Count != 3)
            {
                throw exceptionNumberAtributes;
            }

            string entityTypeToLoad = atributes[1];
            int entityIdToLoad = int.Parse(atributes[2]);

            switch (entityTypeToLoad)
            {
                case "vehicle":
                    serviceVehicle.LoadCargo(CargoFiller(), entityIdToLoad);
                    Console.WriteLine("Cargo is successfully loaded to vehicle");
                    break;

                case "warehouse":
                    serviceWarehouse.LoadCargo(CargoFiller(), entityIdToLoad);
                    Console.WriteLine("Cargo is successfully loaded to warehouse");
                    break;

                default:
                    throw new Exception($"Uknown entity type \'{entityTypeToLoad}\'");
            }
        }

        // Entities Filler
        private Cargo CargoFiller()
        {
            Console.WriteLine("Enter Cargo's data\n");
            var cargo = new Cargo();

            cargo.Id = Guid.NewGuid();

            Console.WriteLine("Weight (kg):");
            cargo.Weight = int.Parse(Console.ReadLine());

            Console.WriteLine("Volume (cub. m):");
            cargo.Volume = float.Parse(Console.ReadLine());

            Console.WriteLine("Cargo Code:");
            cargo.Code = Console.ReadLine();

            Console.WriteLine("Do you want to fill invoice? (Y/n)");
            if (Console.ReadLine() == "Y")
            {
                var invoice = new Invoice();

                invoice.Id = Guid.NewGuid();
                Console.WriteLine("Recipient Address:");
                invoice.RecipientAddress = Console.ReadLine();

                Console.WriteLine("Recipient Phone Number:");
                invoice.RecipientPhoneNumber = Console.ReadLine();

                Console.WriteLine("Sender Address:");
                invoice.SenderAddress = Console.ReadLine();

                Console.WriteLine("Sender Phone Number:");
                invoice.SenderPhoneNumber = Console.ReadLine();

                cargo.Invoice = invoice;
            }

            return cargo;
        }

        private Vehicle VehicleFiller()
        {
            Console.WriteLine("Enter Vehicle's data\n");

            Console.WriteLine("Type (Car/Ship/Plane/Train):");
            string stringVehicleType = Console.ReadLine();
            VehicleType vehicleType;
            switch (stringVehicleType)
            {
                case "Car":
                    vehicleType = VehicleType.Car;
                    break;

                case "Ship":
                    vehicleType = VehicleType.Ship;
                    break;

                case "Plane":
                    vehicleType = VehicleType.Plane;
                    break;

                case "Train":
                    vehicleType = VehicleType.Train;
                    break;

                default:
                    throw new Exception("Uknown vehicle type");
            }

            Console.WriteLine("Number:");
            string number = Console.ReadLine();

            Console.WriteLine("Max. cargo weight (kg):");
            int maxWeight = int.Parse(Console.ReadLine());

            Console.WriteLine("Max. cargo volume (cub. m):");
            float maxVolume = float.Parse(Console.ReadLine());

            return new Vehicle(vehicleType, maxWeight, maxVolume, number);
        }

        // Collection printer
        private void PrintVehicles(List<Vehicle> vehicles)
        {
            if (vehicles.Count == 0)
            {
                Console.WriteLine("There are no vehicles");
                return;
            }

            foreach (var vehicle in vehicles)
            {
                Console.WriteLine(vehicle.ToString());
            }
        }

        private void PrintWarehouses(List<Warehouse> warehouses)
        {
            if (warehouses.Count == 0)
            {
                Console.WriteLine("There are no warehouses");
                return;
            }

            foreach (var warehouse in warehouses)
            {
                Console.WriteLine(warehouse.ToString());
            }
        }
    }
}
