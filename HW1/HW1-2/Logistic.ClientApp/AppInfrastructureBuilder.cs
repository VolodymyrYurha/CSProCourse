using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.DAL;
using Logistic.Core;

namespace Logistic.ClientApp
{
    public class AppInfrastructureBuilder
    {
        public JsonRepository<Vehicle> jsonVehicleRepository;
        public JsonRepository<Warehouse> jsonWarehouseRepository;
        public XmlRepository<Vehicle> xmlVehicleRepository;
        public XmlRepository<Warehouse> xmlWarehouseRepository;
        public InMemoryRepository<Vehicle> inMemoryVehicleRepository;
        public InMemoryRepository<Warehouse> inMemoryWarehouseRepository;

        public VehicleService vehicleService;
        public WarehouseService warehouseService;
        public ReportService<Vehicle> reportVehicleService;
        public ReportService<Warehouse> reportWarehouseService;

        public AppInfrastructureBuilder()
        {
            jsonVehicleRepository = new JsonRepository<Vehicle>();
            jsonWarehouseRepository = new JsonRepository<Warehouse>();
            xmlVehicleRepository = new XmlRepository<Vehicle>();
            xmlWarehouseRepository = new XmlRepository<Warehouse>();
            inMemoryVehicleRepository = new InMemoryRepository<Vehicle>();
            inMemoryWarehouseRepository = new InMemoryRepository<Warehouse>();

            vehicleService = new VehicleService(inMemoryVehicleRepository);
            warehouseService = new WarehouseService(inMemoryWarehouseRepository);
            reportVehicleService = new ReportService<Vehicle>(jsonVehicleRepository, xmlVehicleRepository);
            reportWarehouseService = new ReportService<Warehouse>(jsonWarehouseRepository, xmlWarehouseRepository);
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

                case "unload-cargo":
                    {
                        CommandUnloadCargo(atributes);
                        break;
                    }

                case "create-report":
                    {
                        CommandCreateReport(atributes);
                        break;
                    }

                case "load-report":
                    {
                        CommandLoadReport(atributes);
                        break;
                    }

                default:
                    throw new Exception($"Uknown command \'{commandDeterminant}\'");
            }
        }

        private ArgumentException exceptionNumberAtributes = new ArgumentException("Incorrect atributes number");

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
                    vehicleService.Create(VehicleFiller());
                    Console.WriteLine("New vehicle is successfully added in memory");
                    break;

                case "warehouse":
                    warehouseService.Create(new Warehouse());
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
                    PrintVehicles(vehicleService.GetAll());
                    break;

                case "warehouse":
                    PrintWarehouses(warehouseService.GetAll());
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
                    vehicleService.LoadCargo(CargoFiller(), entityIdToLoad);
                    Console.WriteLine("Cargo is successfully loaded to vehicle");
                    break;

                case "warehouse":
                    warehouseService.LoadCargo(CargoFiller(), entityIdToLoad);
                    Console.WriteLine("Cargo is successfully loaded to warehouse");
                    break;

                default:
                    throw new Exception($"Uknown entity type \'{entityTypeToLoad}\'");
            }
        }

        private void CommandUnloadCargo(List<string> atributes)
        {
            var atributesCount = atributes.Count;
            if (atributesCount != 3 && atributesCount != 4)
            {
                throw exceptionNumberAtributes;
            }

            string entityTypeToLoad = atributes[1];
            int entityIdToUnload = int.Parse(atributes[2]);

            if (atributesCount == 3)
            {
                switch (entityTypeToLoad)
                {
                    case "vehicle":
                        vehicleService.UnloadLastCargo(entityIdToUnload);
                        Console.WriteLine("Cargo is successfully unloaded from the vehicle");
                        break;

                    case "warehouse":
                        warehouseService.UnloadLastCargo(entityIdToUnload);
                        Console.WriteLine("Cargo is successfully unloaded from the warehouse");
                        break;

                    default:
                        throw new Exception($"Uknown entity type \'{entityTypeToLoad}\'");
                }
            }

            if (atributesCount == 4)
            {
                Guid cargoGuidToUnload = Guid.Parse(atributes[3]);
                switch (entityTypeToLoad)
                {
                    case "vehicle":
                        vehicleService.UnloadCargo(cargoGuidToUnload, entityIdToUnload);
                        Console.WriteLine("Cargo is successfully unloaded from the vehicle");
                        break;

                    case "warehouse":
                        warehouseService.UnloadCargo(cargoGuidToUnload, entityIdToUnload);
                        Console.WriteLine("Cargo is successfully unloaded from the warehouse");
                        break;

                    default:
                        throw new Exception($"Uknown entity type \'{entityTypeToLoad}\'");
                }
            }
        }

        private void CommandCreateReport(List<string> atributes)
        {
            if (atributes.Count != 3)
            {
                throw exceptionNumberAtributes;
            }
            string entityTypeToReport = atributes[1];
            ReportType serializingTypeToReport;
            switch (atributes[2])
            {
                case "json":
                    serializingTypeToReport = ReportType.Json;
                    break;

                case "xml":
                    serializingTypeToReport = ReportType.Xml;
                    break;

                default:
                    throw new Exception($"Uknown serialing type: {atributes[2]}");
            }

            switch (entityTypeToReport)
            {
                case "vehicle":
                    reportVehicleService.CreateReport(serializingTypeToReport, vehicleService.GetAll());
                    Console.WriteLine("Vehicles report successfully saved");
                    break;

                case "warehouse":
                    reportWarehouseService.CreateReport(serializingTypeToReport, warehouseService.GetAll());
                    Console.WriteLine("Warehouses report successfully saved");
                    break;

                default:
                    throw new Exception($"Uknown entity type \'{entityTypeToReport}\'");
            }
        }

        private void CommandLoadReport(List<string> atributes)
        {
            if (atributes.Count != 2)
            {
                throw exceptionNumberAtributes;
            }

            string filename = atributes[1];

            if (filename.StartsWith("Vehicle"))
            {
                PrintVehicles(reportVehicleService.LoadReport(filename));
            }

            if (filename.StartsWith("Warehouse"))
            {
                PrintWarehouses(reportWarehouseService.LoadReport(filename));
            }
        }

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
            VehicleType vehicleType = (VehicleType)Enum.Parse(typeof(VehicleType), stringVehicleType);

            Console.WriteLine("Number:");
            string number = Console.ReadLine();

            Console.WriteLine("Max. cargo weight (kg):");
            int maxWeight = int.Parse(Console.ReadLine());

            Console.WriteLine("Max. cargo volume (cub. m):");
            float maxVolume = float.Parse(Console.ReadLine());

            return new Vehicle(vehicleType, maxWeight, maxVolume, number);
        }

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
