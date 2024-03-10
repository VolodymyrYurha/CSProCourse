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

namespace Logistic.GUI.MVVM.Windows
{
    /// <summary>
    /// Interaction logic for VehicleDeleteWindow.xaml
    /// </summary>
    public partial class VehicleDeleteWindow : Window
    {
        public Vehicle DeletedVehicle { get; set; }
        public event EventHandler<int> VehicleDeleted;


        public VehicleDeleteWindow()
        {
            InitializeComponent();
        }

        protected virtual void OnVehicleDeleted(int id)
        {
            VehicleDeleted?.Invoke(this, id);
        }

        private void ConfirmDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            OnVehicleDeleted(DeletedVehicle.Id);
            MainWindow.Overlay.Visibility = Visibility.Collapsed;

            this.Close();
        }
        private void CancelDeleteVehicle_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Overlay.Visibility = Visibility.Collapsed;

            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ConfirmDeleteVehicle_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                CancelDeleteVehicle_Click(null, null);
            }
        }
    }
}
