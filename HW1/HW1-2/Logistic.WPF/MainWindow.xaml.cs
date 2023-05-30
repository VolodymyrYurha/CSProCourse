using Microsoft.Win32;
using System.Windows;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.DAL;
using Logistic.Core;
using Logistic.ClientApp;
using System.Windows.Controls;
using System;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public JsonRepository<Vehicle> jsonVehicleRepository;
        public XmlRepository<Vehicle> xmlVehicleRepository;
        public InMemoryRepository<Vehicle> inMemoryVehicleRepository;

        public VehicleService vehicleService;
        public ReportService<Vehicle> reportVehicleService;

        public Vehicle lastSelectedVehicle;
        //AppInfrastructureBuilder appInfrastructure;
        public MainWindow()
        {
            InitializeComponent();

            jsonVehicleRepository = new JsonRepository<Vehicle>();
            xmlVehicleRepository = new XmlRepository<Vehicle>();
            inMemoryVehicleRepository = new InMemoryRepository<Vehicle>();

            vehicleService = new VehicleService(inMemoryVehicleRepository);
            reportVehicleService = new ReportService<Vehicle>(jsonVehicleRepository, xmlVehicleRepository);

            PopulateListView();
        }

        private void LoadCargoButton_Click(object sender, RoutedEventArgs e)
        {
            CargoWindow cargoWindow = new CargoWindow();
            cargoWindow.ShowDialog();

            if (cargoWindow.IsDataSet)
            {
                MessageBox.Show(cargoWindow.SomeData);
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                ImportTextBox.Text = openFileDialog.FileName;
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            VehicleType type = Enum.Parse<VehicleType>(inputType.SelectedItem.ToString());
            int maxCargoWeightKg = int.Parse(inputWeight.Text);
            float maxCargoVolume = float.Parse(inputVolume.Text);
            string number = inputNumber.Text;

            Vehicle vehicle = new Vehicle(type, maxCargoWeightKg, maxCargoVolume, number);
            vehicleService.Create(vehicle);
            PopulateListView();
        }

        private void vehicleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vehicleListView.SelectedItem != null)
            {
                ListViewItem selectedItem = (ListViewItem)vehicleListView.SelectedItem;
                lastSelectedVehicle = (Vehicle)selectedItem.Content;
                inputNumber.Text = lastSelectedVehicle.Number;
                inputWeight.Text = lastSelectedVehicle.MaxCargoWeightKg.ToString();
                inputVolume.Text = lastSelectedVehicle.MaxCargoVolume.ToString();
                inputType.SelectedItem = lastSelectedVehicle.Type.ToString();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            //ListViewItem selectedItem = (ListViewItem)vehicleListView.SelectedItem;
            //Vehicle selectedVehicle = (Vehicle)selectedItem.Content;
            if (lastSelectedVehicle != null)
            {
                lastSelectedVehicle.Type = Enum.Parse<VehicleType>(inputType.SelectedItem.ToString());
                lastSelectedVehicle.MaxCargoWeightKg = int.Parse(inputWeight.Text);
                lastSelectedVehicle.MaxCargoVolume = float.Parse(inputVolume.Text);
                lastSelectedVehicle.Number = inputNumber.Text;

                vehicleService.Update(lastSelectedVehicle.Id, lastSelectedVehicle);
                PopulateListView();
            }
        }
        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastSelectedVehicle != null)
            {
                inputNumber.Clear();
                inputWeight.Clear();
                inputVolume.Clear();
                inputType.ClearValue(ComboBox.SelectedItemProperty);
                vehicleService.Delete(lastSelectedVehicle.Id);
                PopulateListView();
            }
        }

        private void PopulateListView()
        {
            vehicleListView.Items.Clear();

            foreach (var vehicle in vehicleService.GetAll())
            {
                ListViewItem listViewItem = new ListViewItem();
                listViewItem.Content = vehicle;
                vehicleListView.Items.Add(listViewItem);
            }
        }

    }
}
