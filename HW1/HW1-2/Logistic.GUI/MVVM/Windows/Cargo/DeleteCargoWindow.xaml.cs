using Logistic.Models.Models;
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

namespace Logistic.GUI.MVVM.Windows.Cargo
{
    /// <summary>
    /// Interaction logic for DeleteCargoWindow.xaml
    /// </summary>
    public partial class DeleteCargoWindow : Window
    {
        public Models.Cargo DeletedCargo { get; set; }
        public event EventHandler<Guid> CargoDeleted;


        public DeleteCargoWindow()
        {
            InitializeComponent();
            CargoViewWindow.Overlay.Visibility = Visibility.Visible;
        }

        protected virtual void OnCargoDeleted(Guid id)
        {
            CargoDeleted?.Invoke(this, id);
        }

        private void ConfirmDeleteCargo_Click(object sender, RoutedEventArgs e)
        {
            OnCargoDeleted(DeletedCargo.Id);
            CargoViewWindow.Overlay.Visibility = Visibility.Collapsed;
            this.Close();
        }
        private void CancelDeleteCargo_Click(object sender, RoutedEventArgs e)
        {
            CargoViewWindow.Overlay.Visibility = Visibility.Collapsed;
            this.Close();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                ConfirmDeleteCargo_Click(null, null);
            }
            else if (e.Key == Key.Escape)
            {
                CancelDeleteCargo_Click(null, null);
            }
        }
    }
}
