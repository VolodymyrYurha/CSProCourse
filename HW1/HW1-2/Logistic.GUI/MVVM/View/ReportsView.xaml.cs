﻿using Logistic.Core;
using Logistic.DAL;
using Logistic.GUI.MVVM.Windows.CutomMessageWindow;
using Logistic.Models;
using Logistic.Models.Enums;
using Logistic.Models.Models.Exceptions;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
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
using Xunit.Sdk;

namespace Logistic.GUI.MVVM.View
{
    /// <summary>
    /// Interaction logic for ReportsView.xaml
    /// </summary>
    public partial class ReportsView : UserControl
    {
        public ReportService<Vehicle> reportVehicleService;

        public ReportService<Warehouse> reportWarehouseService;

        public VehicleService vehicleService;
        public WarehouseService warehouseService;

        public InMemoryRepository<Vehicle> inMemoryVehicleRepository;
        public InMemoryRepository<Warehouse> inMemoryWarehouseRepository;


        public ReportsView()
        {
            InitializeComponent();
        }

        public void InitServicesRepositories(ReportService<Vehicle> reportVehicleService,
                                     ReportService<Warehouse> reportWarehouseService,
                                     VehicleService vehicleService,
                                     WarehouseService warehouseService,
                                     InMemoryRepository<Vehicle> inMemoryVehicleRepository,
                                     InMemoryRepository<Warehouse> inMemoryWarehouseRepository)
        {
            this.reportVehicleService = reportVehicleService;
            this.reportWarehouseService = reportWarehouseService;
            this.vehicleService = vehicleService;
            this.warehouseService = warehouseService;
            this.inMemoryVehicleRepository = inMemoryVehicleRepository;
            this.inMemoryWarehouseRepository = inMemoryWarehouseRepository;
        }


        private void VehicleCopyContent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(vehicleTextBox.Text))
            {
                Clipboard.SetText(vehicleTextBox.Text);
                //MessageBox.Show("Text copied to clipboard.", "Copy Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void WarehouseCopyContent_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(vehicleTextBox.Text))
            {
                Clipboard.SetText(vehicleTextBox.Text);
                //MessageBox.Show("Text copied to clipboard.", "Copy Successful", MessageBoxButton.OK, MessageBoxImage.Information);
            }
        }

        private void ImportVehicle_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    var path = openFileDialog.FileName;
                    var vehicles = reportVehicleService.LoadReport(path);
                    if (path.EndsWith(".json"))
                    {
                        vehicleJsonRadioButton.IsChecked = true;
                    }
                    else
                    {
                        vehicleXmlRadioButton.IsChecked = true;
                    }

                    using (StreamReader sr = new StreamReader(path))
                    {
                        string json = sr.ReadToEnd();
                        vehicleTextBox.Text = json;
                    }

                    vehicleService.repository = new InMemoryRepository<Vehicle>(vehicles);
                }
            }
            catch (CustomException c)
            {
                MessageBox.Show(c.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportVehicle_Click(object sender, RoutedEventArgs e)
        {
            var vehicles = vehicleService.GetAll();
            string filename;
            if(vehicleJsonRadioButton.IsChecked == true)
            {
                filename = reportVehicleService.CreateReport(ReportType.Json, vehicles);
            }
            else
            {
                filename = reportVehicleService.CreateReport(ReportType.Xml, vehicles);
            }

            var messageWindow = new MessageWindow();
            messageWindow.ShowMessageWindow("Vehicle exported", $"File name:\n{filename}");
        }


        private void vehicleJsonRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var vehicles = vehicleService.GetAll();
            vehicleTextBox.Text = reportVehicleService.Serialize(vehicles, ReportType.Json);
        }

        private void vehicleXmlRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var vehicles = vehicleService.GetAll();
            vehicleTextBox.Text = reportVehicleService.Serialize(vehicles, ReportType.Xml);
        }

        private void ImportWarehouse_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                OpenFileDialog openFileDialog = new OpenFileDialog();
                if (openFileDialog.ShowDialog() == true)
                {
                    var path = openFileDialog.FileName;
                    var warehouses = reportWarehouseService.LoadReport(path);
                    if (path.EndsWith(".json"))
                    {
                        warehouseJsonRadioButton.IsChecked = true;
                    }
                    else
                    {
                        warehouseXmlRadioButton.IsChecked = true;
                    }

                    using (StreamReader sr = new StreamReader(path))
                    {
                        string json = sr.ReadToEnd();
                        warehouseTextBox.Text = json;
                    }

                    warehouseService.repository = new InMemoryRepository<Warehouse>(warehouses);
                }
            }
            catch (CustomException c)
            {
                MessageBox.Show(c.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void ExportWarehouse_Click(object sender, RoutedEventArgs e)
        {
            var warehouses = warehouseService.GetAll();
            string filename;
            if (warehouseJsonRadioButton.IsChecked == true)
            {
                filename = reportWarehouseService.CreateReport(ReportType.Json, warehouses);
            }
            else
            {
                filename = reportWarehouseService.CreateReport(ReportType.Xml, warehouses);
            }
            var messageWindow = new MessageWindow();
            messageWindow.ShowMessageWindow("Warehouse exported", $"File name:\n{filename}");
        }

        private void warehouseJsonRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var warehouses = warehouseService.GetAll();
            warehouseTextBox.Text = reportWarehouseService.Serialize(warehouses, ReportType.Json);
        }

        private void warehouseXmlRadioButton_Click(object sender, RoutedEventArgs e)
        {
            var warehouses = warehouseService.GetAll();
            warehouseTextBox.Text = reportWarehouseService.Serialize(warehouses, ReportType.Xml);
        }


        public void UpdateTextBoxes()
        {
            if(vehicleJsonRadioButton.IsChecked == true)
            {
                vehicleJsonRadioButton_Click(null, null);
            }
            else
            {
                vehicleXmlRadioButton_Click(null, null);
            }

            if (warehouseJsonRadioButton.IsChecked == true)
            {
                warehouseJsonRadioButton_Click(null, null);
            }
            else
            {
                warehouseXmlRadioButton_Click(null, null);
            }
        }
    }
}
