using Logistic.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;
using Logistic.Core;
using Logistic.Models;
using System.Windows.Controls;
using System.Data;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Cargo.xaml
    /// </summary>
    public partial class CargoWindow : Window
    {
        //private ObservableCollection<Cargo> Cargoes { get; set; } = new ObservableCollection<Cargo>();

        public string SomeData = "Changes've been successfully applied";
        public bool IsDataChanged;
        public Vehicle selectedVehicle { get; set; }
        public int selectedVehicleId { get; set; }
        public VehicleService vehicleService;
        public Cargo lastSelectedCargo;

        public CargoWindow()
        {
            InitializeComponent();
            var vehicles = new List<string>();
            DataContext = this;
            IsDataChanged = false;
            //selectedVehicleId = selectedVehicle.Id;
            //cargoesListView.ItemsSource = Cargoes;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            //default
            //int Weight = 10;
            //float Volume = 10.2f;
            //string Code = "#4444";

            int Weight = int.Parse(inputWeight.Text);
            float Volume = float.Parse(inputVolume.Text);
            string Code = inputCode.Text;

            Cargo newCargo = new Cargo(Weight, Volume, Code);
            vehicleService.LoadCargo(newCargo, selectedVehicleId);
            IsDataChanged = true;
            PopulateCargoes();
            //IsDataChanged = true;
            //Close();
        }

        public void PopulateCargoes()
        {
            cargoesListView.Items.Clear();

            foreach (var cargo in vehicleService.GetById(selectedVehicleId).Cargoes)
            {
                cargoesListView.Items.Add(cargo);
            }
        }

        private void OkeyButton_Click(object sender, RoutedEventArgs e)
        {
            //IsDataChanged = true;
            Close();
        }
        private void cargoesListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cargoesListView.SelectedItem != null)
            {
                lastSelectedCargo = (Cargo)cargoesListView.SelectedItem;
                inputCode.Text = lastSelectedCargo.Code;
                inputVolume.Text = lastSelectedCargo.Volume.ToString();
                inputWeight.Text = lastSelectedCargo.Weight.ToString();
                // Do something with the selected cargo
            }
        }

        private void UnloadButton_Click(object sender, RoutedEventArgs e)
        {
            //Cargo selectedCargo = new Cargo();
            if(lastSelectedCargo != null)
            {
                vehicleService.UnloadCargo(lastSelectedCargo.Id, selectedVehicleId);
                inputCode.Clear();
                inputVolume.Clear();
                inputWeight.Clear();
                IsDataChanged = true;
                PopulateCargoes();
            }
        }
    }
}
