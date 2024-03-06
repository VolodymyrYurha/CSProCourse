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

namespace Logistic.GUI.MVVM.Components
{
    /// <summary>
    /// Interaction logic for CustomButton.xaml
    /// </summary>
    public partial class CustomButton : UserControl
    {
        public event RoutedEventHandler Click;


        public string CustomButtonText
        {
            get { return (string)GetValue(CustomButtonTextProperty); }
            set { SetValue(CustomButtonTextProperty, value); }
        }

        public static readonly DependencyProperty CustomButtonTextProperty =
            DependencyProperty.Register("CustomButtonText", typeof(string), typeof(CustomButton), new PropertyMetadata(string.Empty));


        public CustomButton()
        {
            InitializeComponent();
            customButton.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }
}
