using Microsoft.Win32;
using System.Windows;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.DAL;
using Logistic.Core;
using Logistic.ClientApp;
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

            PopulateListView();
        }

        private void LoadCargoButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastSelectedVehicle != null)
            {
                CargoWindow cargoWindow = new CargoWindow();
                cargoWindow.selectedVehicle = lastSelectedVehicle;
                cargoWindow.selectedVehicleId = lastSelectedVehicle.Id;
                cargoWindow.vehicleService = vehicleService;
                cargoWindow.PopulateCargoes();
                cargoWindow.ShowDialog();

                MessageBox.Show(cargoWindow.SomeData, "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                showNotSelectedMessage();
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    ImportTextBox.Text = openFileDialog.FileName;
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
                PopulateListView();
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

        private void vehicleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (vehicleListView.SelectedItem != null)
            {
                ListViewItem selectedItem = (ListViewItem)vehicleListView.SelectedItem;
                var selectedVehicleList = (Vehicle)selectedItem.Content;
                var selectedId = selectedVehicleList.Id;

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
                PopulateListView();
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
                PopulateListView();
            }
            else
            {
                showNotSelectedMessage();
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

        private void showNotSelectedMessage()
        {
            MessageBox.Show("You've not selected the Vehicle", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
        }
    }
}
