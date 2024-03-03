using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.DAL;
using Logistic.Core;
using System.Windows.Controls;
using System;
using AutoMapper.Execution;

namespace DataGrid
{
    public partial class MainWindow : Window
    {
        public JsonRepository<Vehicle> jsonVehicleRepository;
        public XmlRepository<Vehicle> xmlVehicleRepository;
        public InMemoryRepository<Vehicle> inMemoryVehicleRepository;

        public VehicleService vehicleService;
        public ReportService<Vehicle> reportVehicleService;

        public Vehicle lastSelectedVehicle;

        public MainWindow()
        {
            InitializeComponent();

            jsonVehicleRepository = new JsonRepository<Vehicle>();
            xmlVehicleRepository = new XmlRepository<Vehicle>();
            inMemoryVehicleRepository = new InMemoryRepository<Vehicle>();

            vehicleService = new VehicleService(inMemoryVehicleRepository);
            reportVehicleService = new ReportService<Vehicle>(jsonVehicleRepository, xmlVehicleRepository);

            vehicleService.Create(new Vehicle(VehicleType.Car,  1000,    1000,   "BC 1111 AK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  1500,    1200,   "BC 2222 BK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  800,     800,    "BC 3333 CK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  1200,    1000,   "BC 4444 DK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            vehicleService.Create(new Vehicle(VehicleType.Car,  2000,    1500,   "BC 5555 EK"));
            //var vehicles = vehicleService.GetAll();

            //vehiclesDataGrid.ItemsSource = vehicles;
            UpdateGrid();
        }

        private bool IsMaximize = false;
        private void Border_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ClickCount == 2)
            {
                if (IsMaximize)
                {
                    this.WindowState = WindowState.Normal;
                    this.Width = 1080;
                    this.Height = 720;

                    IsMaximize = false;
                }
                else
                {
                    this.WindowState = WindowState.Maximized;

                    IsMaximize = true;
                }
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void RefreshList(ListView listView)
        {
            var vehicles = vehicleService.GetAll();
            listView.ItemsSource = vehicles;
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            var createVehicleWindow = new VehicleCreateWindow();
            createVehicleWindow.VehicleCreated += CreateVehicleWindow_VehicleCreated;
            createVehicleWindow.Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void CreateVehicleWindow_VehicleCreated(object sender, Vehicle e)
        {
            vehicleService.Create(e);
            UpdateGrid();
        }

        private void UpdateGrid()
        {
            var vehicles = vehicleService.GetAll();
            //vehiclesDataGrid.ItemsSource = vehicles;
            //VehiclesNumberTextBlock.Text = vehicles.Count.ToString();
        }
    }

    public class Member
    {
        public string Character { get; set; }
        public Brush BgColor { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }
        public string Position { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }
}