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
using WpfGUI;

namespace Logistic.GUI.MVVM.Windows
{
    /// <summary>
    /// Interaction logic for VehicleEditWindow.xaml
    /// </summary>
    public partial class VehicleEditWindow : Window
    {
        public Vehicle EditedVehicle { get; set; }


        public event EventHandler<Vehicle> VehicleCreated;

        public VehicleEditWindow()
        {
            InitializeComponent();
            
        }

        public void UpdateInputs()
        {
            inputNumber.inputValue.Text = EditedVehicle.Number;
            inputWeight.inputValue.Text = EditedVehicle.MaxCargoVolume.ToString();
            inputVolume.inputValue.Text = EditedVehicle.MaxCargoWeightKg.ToString();
            inputID.inputValue.Text = EditedVehicle.Id.ToString();
            inputType.SelectedItem = EditedVehicle.Type.ToString();
        }

        protected virtual void OnVehicleEdited(Vehicle e)
        {
            VehicleCreated?.Invoke(this, e);
        }

        private void SaveEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            var newVehicle = this.EditedVehicle; 
            newVehicle.Number = inputNumber.inputValue.Text;
            newVehicle.MaxCargoVolume = int.Parse(inputWeight.inputValue.Text); 
            newVehicle.MaxCargoWeightKg = int.Parse(inputVolume.inputValue.Text); 
            newVehicle.Id = int.Parse(inputID.inputValue.Text);
            newVehicle.Type = (VehicleType)Enum.Parse(typeof(VehicleType), inputType.SelectedItem.ToString());

            OnVehicleEdited(newVehicle);
            MainWindow.Overlay.Visibility = Visibility.Collapsed;

            this.Close();
        }

        private void CancelEditVehicle_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Overlay.Visibility = Visibility.Collapsed;
            this.Close();
        }

        //private void SaveEditVehicle_Click(object sender, RoutedEventArgs e)
        
    }
}
