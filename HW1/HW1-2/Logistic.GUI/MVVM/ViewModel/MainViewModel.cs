﻿using Logistic.Core;
using Logistic.DAL;
using Logistic.GUI.Core;
using Logistic.GUI.MVVM.View;
using Logistic.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.GUI.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public JsonRepository<Vehicle> jsonVehicleRepository;
        public XmlRepository<Vehicle> xmlVehicleRepository;
        public ReportService<Vehicle> reportVehicleService;

        public JsonRepository<Warehouse> jsonWarehouseRepository;
        public XmlRepository<Warehouse> xmlWarehouseRepository;
        public ReportService<Warehouse> reportWarehouseService;
        
        public InMemoryRepository<Vehicle> inMemoryVehicleRepository;
        public InMemoryRepository<Warehouse> inMemoryWarehouseRepository;

        public VehicleService vehicleService;
        public WarehouseService warehouseService;


        public RelayCommand VehicleViewCommand { get; set; }
        public RelayCommand WarehouseViewCommand { get; set; }
        public RelayCommand ReportViewCommand { get; set; }

        public VehicleViewModel VehicleVM { get; set; }
        public WarehouseViewModel WarehouseVM { get; set; }

        private object _currentView;

        private VehicleView _vehicleView;
        private WarehouseView _warehouseView;
        private ReportsView _reportView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        private void InitServicesViews()
        {
            jsonVehicleRepository = new JsonRepository<Vehicle>();
            xmlVehicleRepository = new XmlRepository<Vehicle>();
            reportVehicleService = new ReportService<Vehicle>(jsonVehicleRepository, xmlVehicleRepository);

            jsonWarehouseRepository = new JsonRepository<Warehouse>();
            xmlWarehouseRepository = new XmlRepository<Warehouse>();
            reportWarehouseService = new ReportService<Warehouse>(jsonWarehouseRepository, xmlWarehouseRepository);

            inMemoryVehicleRepository = new InMemoryRepository<Vehicle>();
            inMemoryWarehouseRepository = new InMemoryRepository<Warehouse>();

            
            //VehicleVM = new VehicleViewModel();
            //WarehouseVM = new WarehouseViewModel();
            
        }

        private void LoadDefaultRepositories()
        {
            var vehicles = reportVehicleService.LoadReport("Vehicle_default.json");
            inMemoryVehicleRepository = new InMemoryRepository<Vehicle> (vehicles);

            var warehouses = reportWarehouseService.LoadReport("Warehouse_default.json");
            inMemoryWarehouseRepository = new InMemoryRepository<Warehouse> (warehouses);
        }

        public MainViewModel()
        {
            InitServicesViews();
            LoadDefaultRepositories();

            vehicleService = new VehicleService(inMemoryVehicleRepository);
            warehouseService = new WarehouseService(inMemoryWarehouseRepository);

            _vehicleView = new VehicleView(vehicleService);

            _warehouseView = new WarehouseView(warehouseService);

            _reportView = new ReportsView();
            _reportView.InitServicesRepositories(reportVehicleService, reportWarehouseService, vehicleService, warehouseService, inMemoryVehicleRepository, inMemoryWarehouseRepository);
            CurrentView = _vehicleView;

            VehicleViewCommand = new RelayCommand(o =>
            {
                CurrentView = _vehicleView;
                _vehicleView.UpdateGrid();
            });

            WarehouseViewCommand = new RelayCommand(o =>
            {
                CurrentView = _warehouseView;
                _warehouseView.UpdateGrid();
            });

            ReportViewCommand = new RelayCommand(o =>
            {
                CurrentView = _reportView;
                _reportView.UpdateTextBoxes();
            });
        }
    }
}
