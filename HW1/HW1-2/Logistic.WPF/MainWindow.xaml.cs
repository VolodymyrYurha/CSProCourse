using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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

namespace WpfApp1
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
			//Creaste VehicleServrvice(InMEmoryReposity)
		}

		private void LoadCargoButton_Click(object sender, RoutedEventArgs e)
		{
			CargoWindow cargoWindow = new CargoWindow();
			cargoWindow.ShowDialog();

			if (cargoWindow.IsDataSet)
			{
				MessageBox.Show(cargoWindow.SomeData);
			}
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			OpenFileDialog openFileDialog = new OpenFileDialog();
			if (openFileDialog.ShowDialog() == true) 
			{
				ImportTextBox.Text = openFileDialog.FileName;
			}
		}

		private void Button_Click_1(object sender, RoutedEventArgs e)
		{

		}
	}
}
