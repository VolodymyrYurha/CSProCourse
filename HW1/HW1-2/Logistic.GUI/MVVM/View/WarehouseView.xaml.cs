using Logistic.Core;
using Logistic.GUI.MVVM.Windows;
using Logistic.GUI.MVVM.Windows.Warehouse;
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
using WpfGUI;

namespace Logistic.GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for WarehouseView.xaml
    /// </summary>
    public partial class WarehouseView : UserControl
    {
        public WarehouseService warehouseService;
        public WarehouseView()
        {
            InitializeComponent();
        }

        public WarehouseView( WarehouseService warehouseService)
        {
            InitializeComponent();
            this.warehouseService = warehouseService;
            warehouseService.Create(new Warehouse());
            warehouseService.LoadCargo(new Cargo(), 1);
            warehouseService.LoadCargo(new Cargo(), 1);
            warehouseService.LoadCargo(new Cargo(), 1);
            UpdateGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vehicles = warehouseService.GetAll();
        }

        private void CreateWarehouseWindow_WarehouseCreated(object sender, Warehouse createdWarehouse)
        {
            warehouseService.Create(createdWarehouse);
            UpdateGrid();
        }

        private void EditWarehouseWindow_WarehouseEdited(object sender, Warehouse editedWarehouse)
        {
            warehouseService.Update(editedWarehouse.Id, editedWarehouse);
            UpdateGrid();
        }
        private void DeleteWarehouseWindow_WarehouseDeleted(object sender, int deletedWarehouseId)
        {
            warehouseService.Delete(deletedWarehouseId);
            UpdateGrid();
        }
        private void UpdateGrid()
        {
            var warehouses = warehouseService.GetAll();
            warehouseDataGrid.ItemsSource = warehouses;
            //warehouseDataGrid.ItemsSource+= warehouses[0].
            WarehousesNumberTextBlock.Text = warehouses.Count.ToString();
        }


        private void EditWarehouse_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItem = ((FrameworkElement)sender).DataContext;
            //var vehicle = (Vehicle)selectedItem;
            ////var deletedVehicleId = vehicle.Id;

            //var editVehicleWindow = new VehicleEditWindow();
            //editVehicleWindow.EditedVehicle = vehicle;
            //editVehicleWindow.UpdateInputs();

            //editVehicleWindow.VehicleCreated += EditVehicleWindow_VehicleEdited;
            //MainWindow.Overlay.Visibility = Visibility.Visible;
            //editVehicleWindow.Show();
        }

        private void DeleteWarehouse_Click(object sender, RoutedEventArgs e)
        {
            //var selectedItem = ((FrameworkElement)sender).DataContext;
            //var vehicle = (Vehicle)selectedItem;
            ////var deletedVehicleId = vehicle.Id;

            //var deleteVehicleWindow = new VehicleDeleteWindow();
            //deleteVehicleWindow.DeletedVehicle = vehicle;
            ////deleteVehicleWindow.UpdateInputs();

            //deleteVehicleWindow.VehicleDeleted += DeleteVehicleWindow_VehicleDeleted;
            //MainWindow.Overlay.Visibility = Visibility.Visible;
            //deleteVehicleWindow.Show();
        }

        private void AddWarehouse_Click(object sender, RoutedEventArgs e)
        {
            //createVehicleWindow.VehicleCreated += CreateVehicleWindow_VehicleCreated;
            //MainWindow.Overlay.Visibility = Visibility.Visible;
            //var createWarehouseWindow = new WarehouseCreateWindow();
            //createWarehouseWindow.warehouseService = warehouseService;
            //createWarehouseWindow.Show();
            //warehouseService.Create(new Warehouse());
            //UpdateGrid();

            var createWarehouseWindow = new WarehouseCreateWindow();
            createWarehouseWindow.WarehouseCreated += CreateWarehouseWindow_WarehouseCreated;
            MainWindow.Overlay.Visibility = Visibility.Visible;
            createWarehouseWindow.Show();
        }

    }
}
