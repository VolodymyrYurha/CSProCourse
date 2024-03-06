using System.Windows;
using System.Windows.Controls;

namespace WpfGUI.Components
{
    public partial class CustomTextInputControl : UserControl
    {
        public static readonly DependencyProperty LabelTextProperty =
            DependencyProperty.Register("LabelText", typeof(string), typeof(CustomTextInputControl), new PropertyMetadata("Label"));

        public string LabelText
        {
            get { return (string)GetValue(LabelTextProperty); }
            set { SetValue(LabelTextProperty, value); }
        }

        public static readonly DependencyProperty TextValueProperty =
            DependencyProperty.Register("TextValue", typeof(string), typeof(CustomTextInputControl), new PropertyMetadata(""));

        public string TextValue
        {
            get { return (string)GetValue(TextValueProperty); }
            set { SetValue(TextValueProperty, value); }
        }

        public CustomTextInputControl()
        {
            InitializeComponent();
            DataContext = this;
        }
    }
}
