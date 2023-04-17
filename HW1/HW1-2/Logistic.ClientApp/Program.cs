using System;
using Logistic.ClientApp;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.DAL;
using Logistic.Core;

//namespace Logistic.ClientApp
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine("\t\tVehicle Loading application");
//            var appInfrastructure = new AppInfrastructureBuilder();
//            appInfrastructure.CommandReader();
//        }
//    }
//}

Main();

void Main()
{
    Console.WriteLine("\t\tVehicle Loading application");
    var appInfrastructure = new AppInfrastructureBuilder();
    appInfrastructure.CommandReader();
}