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
    /// Interaction logic for CustomInput.xaml
    /// </summary>
    public partial class CustomTextBlock : UserControl
    {

        public string TextBlockValue
        {
            get { return (string)GetValue(TextBlockValueProperty); }
            set { SetValue(TextBlockValueProperty, value); }
        }

        public static readonly DependencyProperty TextBlockValueProperty =
            DependencyProperty.Register("TextBlockValue", typeof(string), typeof(CustomTextBlock), new PropertyMetadata(string.Empty));





        public string TextBlockTitle
        {
            get { return (string)GetValue(TextBlockTitleProperty); }
            set { SetValue(TextBlockTitleProperty, value); }
        }

        public static readonly DependencyProperty TextBlockTitleProperty =
            DependencyProperty.Register("TextBlockTitle", typeof(string), typeof(CustomTextBlock), new PropertyMetadata(string.Empty));


        public CustomTextBlock()
        {
            InitializeComponent();
            
        }

    }
}
