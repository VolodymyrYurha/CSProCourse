using Logistic.ConsoleClient;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.ConsoleClient.Repositories;
using Logistic.ConsoleClient.Services;

Console.WriteLine("\t\tVehicle Loading application");

Main();

void Main()
{
    var appInfrastructure = new AppInfrastructureBuilder();
    appInfrastructure.CommandReader();
}