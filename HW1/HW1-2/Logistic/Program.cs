using Logistic.ConsoleClient;
using Logistic.ConsoleClient.Models;
using Logistic.ConsoleClient.Models.Enums;
using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;

Console.WriteLine("\t\tVehicle Loading application");

Main();

void Main()
{
    var appInfrastructure = new AppInfrastructureBuilder();
    appInfrastructure.CommandReader();
    //try
    //{
    //}
    //catch (Exception e)
    //{
    //    Console.WriteLine(e.Message);
    //}
}