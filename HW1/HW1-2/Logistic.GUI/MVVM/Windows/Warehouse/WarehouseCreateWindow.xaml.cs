using Logistic.GUI.MVVM.Validations;
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
using Logistic.Core;
using Logistic.Models;

namespace Logistic.GUI.MVVM.Windows.Warehouse
{
    /// <summary>
    /// Interaction logic for WarehouseCreateWindow.xaml
    /// </summary>
    public partial class WarehouseCreateWindow : Window
    {
        public WarehouseService warehouseService;
        public event EventHandler<Models.Warehouse> WarehouseCreated;

        public WarehouseCreateWindow()
        {
            InitializeComponent();
            MainWindow.Overlay.Visibility = Visibility.Visible;
        }

        protected virtual void OnWarehouseCreated(Models.Warehouse e)
        {
            WarehouseCreated?.Invoke(this, e);
        }

        private void SaveCreateWarehouse_Click(object sender, RoutedEventArgs e)
        {
            var warehouse = new Models.Warehouse();
            OnWarehouseCreated(warehouse);
            MainWindow.Overlay.Visibility = Visibility.Collapsed;
            
            this.Close();

        }

        private void CancelCreateWarehouse_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Overlay.Visibility = Visibility.Collapsed;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SaveCreateWarehouse_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                CancelCreateWarehouse_Click(null, null);
            }
        }

    }
}
