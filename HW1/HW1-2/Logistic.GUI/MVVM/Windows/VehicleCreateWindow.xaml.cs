using Logistic.Models;
using Logistic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Printing;
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

namespace Logistic.GUI.MVVM.Windows
{
    /// <summary>
    /// Interaction logic for VehicleCreateWindow.xaml
    /// </summary>
    public partial class VehicleCreateWindow : Window
    {
        public event EventHandler<Vehicle> VehicleCreated;

        public VehicleCreateWindow()
        {
            InitializeComponent();
        }

        protected virtual void OnVehicleCreated(Vehicle e)
        {
            VehicleCreated?.Invoke(this, e);
        }

        private void SaveCreateVehicle_Click(object sender, RoutedEventArgs e)
        {
            var newVehicle = new Vehicle(
                (VehicleType)Enum.Parse(typeof(VehicleType), inputType.SelectedItem.ToString()),
                int.Parse(inputVolume.inputValue.Text),
                int.Parse(inputWeight.inputValue.Text),
                inputNumber.inputValue.Text);
            //newVehicle.Number = inputNumber.inputValue.Text;
            //newVehicle.MaxCargoVolume = int.Parse(inputWeight.inputValue.Text);
            //newVehicle.MaxCargoWeightKg = int.Parse(inputVolume.inputValue.Text);
            ////newVehicle.Id = int.Parse(inputID.inputValue.Text);
            //newVehicle.Type = (VehicleType)Enum.Parse(typeof(VehicleType), inputType.SelectedItem.ToString());

            OnVehicleCreated(newVehicle);
            MainWindow.Overlay.Visibility = Visibility.Collapsed;

            this.Close();
        }

        private void CancelCreateVehicle_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Overlay.Visibility = Visibility.Collapsed;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveCreateVehicle_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                CancelCreateVehicle_Click(null, null);
            }
        }
    }
}
