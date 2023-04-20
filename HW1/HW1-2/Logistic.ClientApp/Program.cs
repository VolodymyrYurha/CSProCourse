namespace Logistic.ClientApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("\t\tVehicle Loading application");
            var appInfrastructure = new AppInfrastructureBuilder();
            appInfrastructure.CommandReader();
        }
    }
}