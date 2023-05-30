using Logistic.Models;
using System.Collections.Generic;
using System.Windows;
using Logistic.Core;
using System.Windows.Controls;
using Logistic.Models.Models.Exceptions;
using System;

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for Cargo.xaml
    /// </summary>
    public partial class CargoWindow : Window
    {
        public string SomeData = "Changes've been successfully applied.";
        public bool IsDataChanged;
        public Vehicle selectedVehicle { get; set; }
        public int selectedVehicleId { get; set; }
        public VehicleService vehicleService;
        public Cargo lastSelectedCargo;
        private int loadedCargoes, unloadedCargoes;

        public CargoWindow()
        {
            InitializeComponent();
            var vehicles = new List<string>();
            DataContext = this;
            IsDataChanged = false;
            loadedCargoes = 0;
            unloadedCargoes = 0;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(inputWeight.Text) || string.IsNullOrWhiteSpace(inputVolume.Text)  || string.IsNullOrWhiteSpace(inputCode.Text))
            {
                MessageBox.Show("You've not filled all data", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            try
            {
                int Weight = int.Parse(inputWeight.Text);
                float Volume = float.Parse(inputVolume.Text);
                string Code = inputCode.Text;

                Cargo newCargo = new Cargo(Weight, Volume, Code);
                vehicleService.LoadCargo(newCargo, selectedVehicleId);
                IsDataChanged = true;
                loadedCargoes++;

                PopulateCargoes();
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
        private void UnloadButton_Click(object sender, RoutedEventArgs e)
        {
            if (lastSelectedCargo != null)
            {
                vehicleService.UnloadCargo(lastSelectedCargo.Id, selectedVehicleId);
                inputCode.Clear();
                inputVolume.Clear();
                inputWeight.Clear();
                IsDataChanged = true;
                unloadedCargoes++;

                PopulateCargoes();
            }
            else
            {
                MessageBox.Show("You've not select the Cargo", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
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
            if (IsDataChanged)
            {
                SomeData += $"\n(+{loadedCargoes}) and (-{unloadedCargoes}) cargoes";
            }
            else
            {
                SomeData = "Nothing's been changed";
            }
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
            }
        }
    }
}
