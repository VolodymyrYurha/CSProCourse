using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for Cargo.xaml
	/// </summary>
	public partial class CargoWindow : Window
	{
		public string SomeData = "Constant data";
		public bool IsDataSet = false;

		public CargoWindow()
		{
			InitializeComponent();
			var vehicles = new List<string>();
		}

		private void SaveDataButton_Click(object sender, RoutedEventArgs e)
		{
			IsDataSet = true;
			Close();
		}
	}
}
