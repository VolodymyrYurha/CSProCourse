﻿using WpfGUI;
using Logistic.Core;
using Logistic.DAL;
using Logistic.Models.Enums;
using Logistic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Logistic.GUI.MVVM.Windows;
using Logistic.GUI.MVVM.Windows.Cargo;
using Logistic.Core.Interfaces;
using Logistic.Models.Interfaces;

namespace Logistic.GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for VehicleView.xaml
    /// </summary>
    public partial class VehicleView : UserControl
    {

        public VehicleService vehicleService;
        public ReportService<Vehicle> reportVehicleService;

        public Vehicle lastSelectedVehicle;

        public VehicleView()
        {
            InitializeComponent();
        }

        public VehicleView(VehicleService vehicleService)
        {
            InitializeComponent();
            this.vehicleService = vehicleService;
            UpdateGrid();
        }
        public void InitServices(VehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
            UpdateGrid();
        }
        private void EditVehicle_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((FrameworkElement)sender).DataContext;
            var vehicle = (Vehicle)selectedItem;
            //var deletedVehicleId = vehicle.Id;

            var editVehicleWindow = new VehicleEditWindow();
            editVehicleWindow.EditedVehicle = vehicle;
            editVehicleWindow.UpdateInputs();

            editVehicleWindow.VehicleCreated += EditVehicleWindow_VehicleEdited;
            MainWindow.Overlay.Visibility = Visibility.Visible;
            editVehicleWindow.Show();
        }

        private void DeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((FrameworkElement)sender).DataContext;
            var vehicle = (Vehicle)selectedItem;
            //var deletedVehicleId = vehicle.Id;

            var deleteVehicleWindow = new VehicleDeleteWindow();
            deleteVehicleWindow.DeletedVehicle = vehicle;
            //cargoVehicleWindow.UpdateInputs();

            deleteVehicleWindow.VehicleDeleted += DeleteVehicleWindow_VehicleDeleted;
            MainWindow.Overlay.Visibility = Visibility.Visible;
            deleteVehicleWindow.Show();
        }

        private void CargoVehicle_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((FrameworkElement)sender).DataContext;
            var vehicle = (Vehicle)selectedItem;

            var cargoVehicleWindow = new CargoViewWindow();
            cargoVehicleWindow.InitVehicle(vehicle, vehicleService);

            cargoVehicleWindow.cargoesManaged += CargoVehicleWindow_cargoesManaged;
            cargoVehicleWindow.Show();
        }

        private void CargoVehicleWindow_cargoesManaged(object sender, EventArgs e)
        {
            UpdateGrid();
        }

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            var createVehicleWindow = new VehicleCreateWindow();
            createVehicleWindow.VehicleCreated += CreateVehicleWindow_VehicleCreated;
            MainWindow.Overlay.Visibility = Visibility.Visible;
            createVehicleWindow.Show();
        }

        private void CreateVehicleWindow_VehicleCreated(object sender, Vehicle createdVehicle)
        {
            vehicleService.Create(createdVehicle);
            UpdateGrid();
        }

        private void EditVehicleWindow_VehicleEdited(object sender, Vehicle editedVehicle)
        {
            vehicleService.Update(editedVehicle.Id, editedVehicle);
            UpdateGrid();
        }
        private void DeleteVehicleWindow_VehicleDeleted(object sender, int deletedVehicleId)
        {
            vehicleService.Delete(deletedVehicleId);
            UpdateGrid();
        }

        public void UpdateGrid()
        {
            var vehicles = vehicleService.GetAll();
            vehiclesDataGrid.ItemsSource = vehicles;
            VehiclesNumberTextBlock.Text = vehicles.Count.ToString();
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((FrameworkElement)sender).DataContext;
        }

    }
}
