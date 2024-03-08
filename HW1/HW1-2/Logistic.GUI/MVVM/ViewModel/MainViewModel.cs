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
        public ReportService<Vehicle> reportVehicleService;

        public InMemoryRepository<Vehicle> inMemoryVehicleRepository;
        public InMemoryRepository<Warehouse> inMemoryWarehouseRepository;

        public VehicleService vehicleService;
        public WarehouseService warehouseService;


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
            reportVehicleService = new ReportService<Vehicle>(jsonVehicleRepository, xmlVehicleRepository);
            
            inMemoryVehicleRepository = new InMemoryRepository<Vehicle>();
            inMemoryWarehouseRepository = new InMemoryRepository<Warehouse>();

            vehicleService = new VehicleService(inMemoryVehicleRepository);
            warehouseService = new WarehouseService(inMemoryWarehouseRepository);

            VehicleVM = new VehicleViewModel();
            _vehicleView = new VehicleView(vehicleService);
            
            _warehouseView = new WarehouseView(warehouseService);
            WarehouseVM = new WarehouseViewModel();
            CurrentView = _vehicleView;

            //_vehicleView.InitServices(vehicleService);


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
