using Logistic.GUI.MVVM.Validations;
using Logistic.Models.Enums;
using Logistic.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
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
    /// Interaction logic for CargoCreateWindow.xaml
    /// </summary>
    public partial class CargoCreateWindow : Window
    {
        public static Rectangle Overlay { get; private set; }

        public event EventHandler<Models.Cargo> cargoCreated;
        private FormValidator _formValidator;

        public CargoCreateWindow()
        {
            InitializeComponent();
            Overlay = overlay;
            _formValidator = new FormValidator();
            CargoViewWindow.Overlay.Visibility = Visibility.Visible;
        }

        protected virtual void OnCargoCreated(Models.Cargo cargo)
        {
            cargoCreated?.Invoke(this, cargo);
        }

        private void SaveCreateCargo_Click(object sender, RoutedEventArgs e)
        {
            if (ValidateForm())
            {
                var newCargo = new Models.Cargo(
                    int.Parse(inputWeight.inputValue.Text),
                    float.Parse(inputVolume.inputValue.Text, NumberStyles.Float, CultureInfo.InvariantCulture),
                    inputCode.inputValue.Text);
                
                OnCargoCreated(newCargo);
                //CargoViewWindow.Overlay.Visibility = Visibility.Collapsed;

                this.Close();
            }
        }

        private bool ValidateForm()
        {
            bool isValidFrom = true;

            var defaultBorderBrush = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#8A8A8A"));

            var codeStr = inputCode.inputValue.Text;
            var result = _formValidator.ValidateCargoCode(codeStr);
            if (result.IsValid)
            {
                inputCode.BorderBrush = defaultBorderBrush;
                inputCode.ErrorMessage = "";
            }
            else
            {
                inputCode.ErrorMessage = result.ErrorContent.ToString();
                inputCode.BorderBrush = Brushes.Red;
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

            return isValidFrom;
        }

        private void CancelCreateCargo_Click(object sender, RoutedEventArgs e)
        {
            CargoViewWindow.Overlay.Visibility = Visibility.Collapsed;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveCreateCargo_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                CancelCreateCargo_Click(null, null);
            }
        }
    }
}
