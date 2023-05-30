using Microsoft.Win32;
using System.Windows;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.DAL;
using Logistic.Core;
using System.Windows.Controls;
using System;
using Logistic.Models.Models.Exceptions;

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
        public MainWindow()
        {
            InitializeComponent();

            jsonVehicleRepository = new JsonRepository<Vehicle>();
            xmlVehicleRepository = new XmlRepository<Vehicle>();
            inMemoryVehicleRepository = new InMemoryRepository<Vehicle>();

            vehicleService = new VehicleService(inMemoryVehicleRepository);
            reportVehicleService = new ReportService<Vehicle>(jsonVehicleRepository, xmlVehicleRepository);

            RefreshList(vehicleListView);
        }

        private void LoadCargoButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastSelectedVehicle != null)
            {
                CargoWindow cargoWindow = new CargoWindow();
                cargoWindow.selectedVehicleId = lastSelectedVehicle.Id;
                cargoWindow.vehicleService = vehicleService;
                cargoWindow.RefreshCargoes();
                cargoWindow.ShowDialog();

                MessageBox.Show(cargoWindow.changesLog, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                showNotSelectedMessage();
            }
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputWeight.Text) || string.IsNullOrWhiteSpace(inputVolume.Text) || string.IsNullOrWhiteSpace(inputNumber.Text) || string.IsNullOrWhiteSpace(inputType.Text))
            {
                MessageBox.Show("You've not filled all data", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                VehicleType type = Enum.Parse<VehicleType>(inputType.SelectedItem.ToString());
                int maxCargoWeightKg = int.Parse(inputWeight.Text);
                float maxCargoVolume = float.Parse(inputVolume.Text);
                string number = inputNumber.Text;

                Vehicle vehicle = new Vehicle(type, maxCargoWeightKg, maxCargoVolume, number);
                vehicleService.Create(vehicle);
                RefreshList(vehicleListView);
            }
            catch (CustomException c)
            {
                MessageBox.Show(c.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void VehicleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vehicleListView.SelectedItem != null)
            {
                var selectedVehicle = vehicleListView.SelectedItem as Vehicle;
                var selectedId = selectedVehicle.Id;

                lastSelectedVehicle = vehicleService.GetById(selectedId);

                inputNumber.Text = lastSelectedVehicle.Number;
                inputWeight.Text = lastSelectedVehicle.MaxCargoWeightKg.ToString();
                inputVolume.Text = lastSelectedVehicle.MaxCargoVolume.ToString();
                inputType.SelectedItem = lastSelectedVehicle.Type.ToString();
            }
        }

        private void UpdateButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastSelectedVehicle != null)
            {
                lastSelectedVehicle.Type = Enum.Parse<VehicleType>(inputType.SelectedItem.ToString());
                lastSelectedVehicle.MaxCargoWeightKg = int.Parse(inputWeight.Text);
                lastSelectedVehicle.MaxCargoVolume = float.Parse(inputVolume.Text);
                lastSelectedVehicle.Number = inputNumber.Text;

                vehicleService.Update(lastSelectedVehicle.Id, lastSelectedVehicle);
                RefreshList(vehicleListView);
            }
            else
            {
                showNotSelectedMessage();
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
                RefreshList(vehicleListView);
            }
            else
            {
                showNotSelectedMessage();
            }
        }

        private void showNotSelectedMessage()
        {
            MessageBox.Show("You've not selected the Vehicle", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void ImportButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    var path = openFileDialog.FileName;
                    ImportTextBox.Text = path;
                    var vehicles = reportVehicleService.LoadReport(path);
                    vehicleReportListView.ItemsSource = vehicles;
                }
            }
            catch (CustomException c)
            {
                MessageBox.Show(c.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportButton_Click(object sender, RoutedEventArgs e)
        {
            if (inputExportType.SelectedItem == null)
            {
                MessageBox.Show("You've not selected export Type", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                var vehicles = vehicleService.GetAll();
                if (inputExportType.SelectedValue.ToString() == "Json")
                {
                    string jsonPath = reportVehicleService.CreateReport(ReportType.Json, vehicles);
                    RefreshList(vehicleExportListView);

                    ExportTextBox.Text = jsonPath;
                    MessageBox.Show("Successfully saved as Json", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                else if (inputExportType.SelectedValue.ToString() == "Xml")
                {
                    var xmlPath = reportVehicleService.CreateReport(ReportType.Xml, vehicles);
                    RefreshList(vehicleExportListView); 

                    ExportTextBox.Text = xmlPath;
                    MessageBox.Show("Successfully saved as Xml", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (CustomException c)
            {
                MessageBox.Show(c.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void RefreshList(ListView listView)
        {
            var vehicles = vehicleService.GetAll();
            listView.ItemsSource = vehicles;
        }
    }
}
