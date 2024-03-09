using Logistic.Core;
using Logistic.Core.Interfaces;
using Logistic.DAL;
using Logistic.GUI.MVVM.Windows.CutomMessageWindow;
using Logistic.Models;
using Logistic.Models.Interfaces;
using NSubstitute;
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
using System.Windows.Shapes;
using WpfGUI;

namespace Logistic.GUI.MVVM.Windows.Cargo
{
    /// <summary>
    /// Interaction logic for CargoViewWindow.xaml
    /// </summary>
    public partial class CargoViewWindow : Window
    {
        public static Rectangle Overlay { get; private set; }

        private Type _typeToManage;
        public event EventHandler cargoesManaged;
        //public event EventHandler<InMemoryRepository<Vehicle>> vehicleRepositoryUpdated;
        //public event EventHandler<InMemoryRepository<Models.Warehouse>> warehouseRepositoryUpdated;

        //private Vehicle _vehicle;
        private VehicleService _vehicleService;

        //private Models.Warehouse _warehouse;
        private WarehouseService _warehouseService;

        private IEntity entity;
        private IEntityLoadingService<IEntity> entityService;

        public CargoViewWindow()
        {
            InitializeComponent();
            Overlay = overlay;
            MainWindow.Overlay.Visibility = Visibility.Visible;
        }

        public void InitializeEntities(IEntity entity, VehicleService vehicleService)
        {
            //var vehicleService = new VehicleService(new InMemoryRepository<Vehicle>());

            this.entity = entity;
            _vehicleService = vehicleService;
            cargoesDataGrid.ItemsSource = entity.Cargoes;
        }

        public void InitVehicle(Vehicle vehicle, VehicleService vehicleService)
        {
            this.entity = vehicle;
            _typeToManage = typeof(Vehicle);
            _vehicleService = vehicleService;
            cargoesDataGrid.ItemsSource = entity.Cargoes;


        }

        public void InitWarehouse(Models.Warehouse warehouse, WarehouseService warehouseService)
        {
            this.entity = warehouse;
            _typeToManage = typeof(Models.Warehouse);
            _warehouseService = warehouseService;
            cargoesDataGrid.ItemsSource = entity.Cargoes;
        }

        private void UpdateGrid()
        {
            if (_typeToManage == typeof(Vehicle))
            {
                var vehicle = _vehicleService.GetById(entity.Id);
                cargoesDataGrid.ItemsSource = vehicle.Cargoes;
                
            }
            else
            {
                var warehouse = _warehouseService.GetById(entity.Id);
                cargoesDataGrid.ItemsSource = warehouse.Cargoes;
                
            }
        }

        private void AddCargo_Click(object sender, RoutedEventArgs e)
        {
            var createCargoWindow = new CargoCreateWindow();
            //createCargoWindow.cargoCreated = cargo;

            createCargoWindow.Show();
            createCargoWindow.cargoCreated += CreateCargoWindow_cargoCreated;
        }

        private void CreateCargoWindow_cargoCreated(object sender, Models.Cargo cargo)
        {
            try
            {
                if (_typeToManage == typeof(Vehicle))
                {
                    _vehicleService.LoadCargo(cargo, entity.Id);
                    UpdateGrid();
                }
                else
                {
                    _warehouseService.LoadCargo(cargo, entity.Id);
                    UpdateGrid();
                }
                overlay.Visibility = Visibility.Collapsed;
            }
            catch (Exception e)
            {
                var errorWindow = new ErrorWindow();
                errorWindow.onWindowClosed += ErrorWindow_onWindowClosed;
                errorWindow.ShowErrorMessageWindow(e.Message);
                overlay.Visibility = Visibility.Visible;
            }
            
        }

        private void ErrorWindow_onWindowClosed(object sender, EventArgs e)
        {
            overlay.Visibility=Visibility.Collapsed;
        }

        private void EditCargo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCargo_Click(object sender, RoutedEventArgs e)
        {
            var selectedItem = ((FrameworkElement)sender).DataContext;
            var cargo = (Models.Cargo)selectedItem;
            
            var deleteVehicleWindow = new DeleteCargoWindow();
            deleteVehicleWindow.DeletedCargo = cargo;

            deleteVehicleWindow.CargoDeleted += CargoDeleteWindow_CargoDeleted;
            deleteVehicleWindow.Show();
        }

        private void CargoDeleteWindow_CargoDeleted(object sender, Guid deletedCargoGuid)
        {
            if (_typeToManage == typeof(Vehicle))
            {
                _vehicleService.UnloadCargo(deletedCargoGuid, entity.Id);
                UpdateGrid();
            }
            else
            {
                _warehouseService.UnloadCargo(deletedCargoGuid, entity.Id);
                UpdateGrid();
            }
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {

            var repository = _vehicleService.repository;
            MainWindow.Overlay.Visibility = Visibility.Collapsed;
            cargoesManaged?.Invoke(this, null);
            this.Close();

        }
        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                OkButton_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                OkButton_Click(null, null);
            }
        }
    }
}
