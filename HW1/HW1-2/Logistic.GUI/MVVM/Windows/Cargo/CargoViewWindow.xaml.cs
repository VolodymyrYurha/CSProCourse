using Logistic.Core;
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
using System.Windows.Shapes;
using WpfGUI;

namespace Logistic.GUI.MVVM.Windows.Cargo
{
    /// <summary>
    /// Interaction logic for CargoViewWindow.xaml
    /// </summary>
    public partial class CargoViewWindow : Window
    {
        private Vehicle _vehicle;
        private VehicleService _vehicleService;
        private Type _typeToManage;
        public CargoViewWindow()
        {
            InitializeComponent();
            MainWindow.Overlay.Visibility= Visibility.Visible;
        }

        public void InitVehicle(Vehicle vehicle, VehicleService vehicleService)
        {
            _vehicle = vehicle;
            _vehicleService = vehicleService;
            _typeToManage = typeof(Vehicle);
            warehouseDataGrid.ItemsSource = _vehicle.Cargoes;
        }

        private void AddCargo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void EditCargo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeleteCargo_Click(object sender, RoutedEventArgs e)
        {

        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Overlay.Visibility= Visibility.Collapsed;
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
