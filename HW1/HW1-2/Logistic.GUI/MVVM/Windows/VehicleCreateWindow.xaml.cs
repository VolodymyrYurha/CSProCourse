using Logistic.Models;
using Logistic.Models.Enums;
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

namespace DataGrid
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

        private void AddVehicle_Click(object sender, RoutedEventArgs e)
        {
            Vehicle newVehicle = new Vehicle(
                (VehicleType)Enum.Parse(typeof(VehicleType), inputType.SelectedItem.ToString()),
                Convert.ToInt32(inputWeight.Text),
                Convert.ToInt32(inputVolume.Text),
                inputNumber.Text);

            OnVehicleCreated(newVehicle);

            this.Close();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                AddVehicle_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                Cancel_Click(null, null);
            }
        }

    }
    public class VehicleEventArgs : EventArgs
    {
        public Vehicle Vehicle { get; }

        public VehicleEventArgs(Vehicle vehicle)
        {
            Vehicle = vehicle;
        }
    }
}
