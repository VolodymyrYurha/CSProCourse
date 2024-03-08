using Logistic.Core;
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

namespace Logistic.GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for WarehouseView.xaml
    /// </summary>
    public partial class WarehouseView : UserControl
    {
        public VehicleService vehicleService;
        public WarehouseView()
        {
            InitializeComponent();
        }

        public void InitServices(VehicleService vehicleService)
        {
            this.vehicleService = vehicleService;
            //UpdateGrid();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var vehicles = vehicleService.GetAll();
        }
    }
}
