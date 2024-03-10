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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Logistic.GUI.MVVM.Components
{
    /// <summary>
    /// Interaction logic for CustomHollowButton.xaml
    /// </summary>
    public partial class CustomHollowButton : UserControl
    {
        public event RoutedEventHandler Click;

        public static readonly DependencyProperty ButtonColorProperty =
            DependencyProperty.Register("ButtonColor", typeof(string), typeof(CustomHollowButton), new PropertyMetadata("#6741D9"));

        public string ButtonColor
        {
            get { return (string)GetValue(ButtonColorProperty); }
            set { SetValue(ButtonColorProperty, value); }
        }

        public string CustomHollowButtonText
        {
            get { return (string)GetValue(CustomHollowButtonTextProperty); }
            set { SetValue(CustomHollowButtonTextProperty, value); }
        }

        public static readonly DependencyProperty CustomHollowButtonTextProperty =
            DependencyProperty.Register("CustomHollowButtonText", typeof(string), typeof(CustomHollowButton), new PropertyMetadata("button"));


        public CustomHollowButton()
        {
            InitializeComponent();
            customHollowButton.Click += Button_Click;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Click?.Invoke(this, e);
        }
    }

    public class StringToBrushConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string colorString)
            {
                try
                {
                    return new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorString));
                }
                catch (FormatException)
                {
                    return DependencyProperty.UnsetValue;
                }
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }

}
