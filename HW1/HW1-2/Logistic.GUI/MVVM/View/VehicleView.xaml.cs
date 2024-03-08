using WpfGUI;
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

namespace Logistic.GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for VehicleView.xaml
    /// </summary>
    public partial class VehicleView : UserControl
    {
        //public JsonRepository<Vehicle> jsonVehicleRepository;
        //public XmlRepository<Vehicle> xmlVehicleRepository;
        //public InMemoryRepository<Vehicle> inMemoryVehicleRepository;

        public VehicleService vehicleService;
        public ReportService<Vehicle> reportVehicleService;

        public Vehicle lastSelectedVehicle;

        public VehicleView()
        {
            InitializeComponent();
        }


        //    //jsonVehicleRepository = new JsonRepository<Vehicle>();
        //    //xmlVehicleRepository = new XmlRepository<Vehicle>();
        //    var inMemoryVehicleRepository = new InMemoryRepository<Vehicle>();

        //    vehicleService = new VehicleService(inMemoryVehicleRepository);
        //    //reportVehicleService = new ReportService<Vehicle>(jsonVehicleRepository, xmlVehicleRepository);

        //    vehicleService.Create(new Vehicle(VehicleType.Car, 1000, 1000, "BC 1111 AK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 1500, 1200, "BC 2222 BK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 800, 800, "BC 3333 CK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 1200, 1000, "BC 4444 DK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 2000, 1500, "BC 5555 EK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 2000, 1500, "BC 6666 FK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 2000, 1500, "BC 7777 GK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 2000, 1500, "BC 8888 HK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 2000, 1500, "BC 9999 IK"));
        //    //vehicleService.Create(new Vehicle(VehicleType.Car, 2000, 1500, "BC 1010 JK"));

        //    UpdateGrid();
        //}

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
            //deleteVehicleWindow.UpdateInputs();

            deleteVehicleWindow.VehicleDeleted += DeleteVehicleWindow_VehicleDeleted;
            MainWindow.Overlay.Visibility = Visibility.Visible;
            deleteVehicleWindow.Show();
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

        private void UpdateGrid()
        {
            var vehicles = vehicleService.GetAll();
            vehiclesDataGrid.ItemsSource = vehicles;
            VehiclesNumberTextBlock.Text = vehicles.Count.ToString();
        }


        private void RemoveButton_Click(object sender, RoutedEventArgs e)
        {
            // Get the selected item from the WpfGUI
            var selectedItem = ((FrameworkElement)sender).DataContext;
            // Perform remove operation with the selected item
        }

    }
}
