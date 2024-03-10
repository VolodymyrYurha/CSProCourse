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

namespace Logistic.GUI.MVVM.Windows.CutomMessageWindow
{
    /// <summary>
    /// Interaction logic for ErrorWindow.xaml
    /// </summary>
    public partial class ErrorWindow : Window
    {
        public event EventHandler onWindowClosed;

        public ErrorWindow()
        {
            InitializeComponent();
            //MainWindow.Overlay.Visibility = Visibility.Visible;
        }

        public void ShowFullErrorWindow(string title, string message)
        {
            errorTitle.Text = title;
            errorText.Text = message;
            this.Show();
        }
        public void ShowErrorMessageWindow(string message)
        {
            errorText.Text = message;
            onWindowClosed?.Invoke(this, EventArgs.Empty);
            this.Show();
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            //MainWindow.Overlay.Visibility = Visibility.Collapsed;
            onWindowClosed?.Invoke(this, EventArgs.Empty);
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
