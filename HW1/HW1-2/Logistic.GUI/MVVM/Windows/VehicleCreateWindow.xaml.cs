using Logistic.GUI.MVVM.Validations;
using Logistic.Models;
using Logistic.Models.Enums;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Printing;
using System.Text;
using System.Text.RegularExpressions;
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
        private FormValidator _formValidator;

        public VehicleCreateWindow()
        {
            InitializeComponent();
            _formValidator = new FormValidator();
        }

        protected virtual void OnVehicleCreated(Vehicle e)
        {
            VehicleCreated?.Invoke(this, e);
        }

        private void SaveCreateVehicle_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                var newVehicle = new Vehicle(
                    (VehicleType)Enum.Parse(typeof(VehicleType), inputType.SelectedItem.ToString()),
                    int.Parse(inputWeight.inputValue.Text),
                    float.Parse(inputVolume.inputValue.Text, NumberStyles.Float, CultureInfo.InvariantCulture),
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
        }

        private bool ValidateForm()
        {
            bool isValidFrom = true;

            var defaultBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A8A8A"));

            var numberStr = inputNumber.inputValue.Text;
            var result = _formValidator.ValidateVehicleNumber(numberStr);
            if (result.IsValid)
            {
                inputNumber.BorderBrush = defaultBorderBrush;
                inputNumber.ErrorMessage = "";         
            }
            else
            {
                inputNumber.ErrorMessage = result.ErrorContent.ToString();
                inputNumber.BorderBrush = Brushes.Red;
                isValidFrom = false;
            }

            var volumeStr = inputVolume.inputValue.Text;
            result = _formValidator.ValidateVolumeFloat(volumeStr);
            if (result.IsValid)
            {
                inputVolume.BorderBrush = defaultBorderBrush;
                inputVolume.ErrorMessage = "";
            }
            else
            {
                inputVolume.ErrorMessage = result.ErrorContent.ToString();
                inputVolume.BorderBrush = Brushes.Red;
                isValidFrom = false;
            }

            var weightStr = inputWeight.inputValue.Text;
            result = _formValidator.ValidateWeightInt(weightStr);
            if (result.IsValid)
            {
                inputWeight.BorderBrush = defaultBorderBrush;
                inputWeight.ErrorMessage = "";
            }
            else
            {
                inputWeight.ErrorMessage = result.ErrorContent.ToString();
                inputWeight.BorderBrush = Brushes.Red;
                isValidFrom = false;
            }

            var type = inputType.SelectedItem?.ToString();
            if (string.IsNullOrEmpty(type))
            {
                inputTypeErrorMessage.Text = "Chose the vehicle type";
                inputType.BorderBrush = Brushes.Red;
                isValidFrom = false;
            }
            else
            {
                inputType.BorderBrush = defaultBorderBrush;
                inputTypeErrorMessage.Text = "";
            }

            return isValidFrom;
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
