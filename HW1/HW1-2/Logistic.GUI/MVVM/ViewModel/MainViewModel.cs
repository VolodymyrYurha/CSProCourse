using Logistic.Core;
using Logistic.DAL;
using Logistic.GUI.Core;
using Logistic.GUI.MVVM.View;
using Logistic.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.GUI.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public JsonRepository<Vehicle> jsonVehicleRepository;
        public XmlRepository<Vehicle> xmlVehicleRepository;
        public InMemoryRepository<Vehicle> inMemoryVehicleRepository;

        public VehicleService vehicleService;
        public ReportService<Vehicle> reportVehicleService;

        public RelayCommand VehicleViewCommand { get; set; }
        public RelayCommand WarehouseViewCommand { get; set; }

        public VehicleViewModel VehicleVM { get; set; }
        public WarehouseViewModel WarehouseVM { get; set; }

        private object _currentView;

        private VehicleView _vehicleView;
        private WarehouseView _warehouseView;

        public object CurrentView
        {
            get { return _currentView; }
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public MainViewModel()
        {
            jsonVehicleRepository = new JsonRepository<Vehicle>();
            xmlVehicleRepository = new XmlRepository<Vehicle>();
            inMemoryVehicleRepository = new InMemoryRepository<Vehicle>();

            vehicleService = new VehicleService(inMemoryVehicleRepository);
            reportVehicleService = new ReportService<Vehicle>(jsonVehicleRepository, xmlVehicleRepository);

            //VehicleVM = new VehicleViewModel();
            //WarehouseVM = new WarehouseViewModel();
            //CurrentView = VehicleVM;

            //VehicleViewCommand = new RelayCommand(o =>
            //{
            //    CurrentView = VehicleVM;
            //});

            //WarehouseViewCommand = new RelayCommand(o =>
            //{
            //    CurrentView = WarehouseVM;
            //});
            // Initialize your view models
            VehicleVM = new VehicleViewModel();
            WarehouseVM = new WarehouseViewModel();
            CurrentView = VehicleVM;

            // Initialize your views
            _vehicleView = new VehicleView();
            _vehicleView.InitServices(vehicleService);
            _warehouseView = new WarehouseView();
            _warehouseView.InitServices(vehicleService);

            VehicleViewCommand = new RelayCommand(o =>
            {
                CurrentView = _vehicleView;
            });

            WarehouseViewCommand = new RelayCommand(o =>
            {
                CurrentView = _warehouseView;
            });
        }
    }
}
