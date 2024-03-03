using Logistic.GUI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Logistic.GUI.MVVM.ViewModel
{
    class MainViewModel : ObservableObject
    {
        public RelayCommand VehicleViewCommand { get; set; }
        public RelayCommand WarehouseViewCommand { get; set; }

        public VehicleViewModel VehicleVM { get; set; }
        public WarehouseViewModel WarehouseVM { get; set; }

        private object _currentView;

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
            VehicleVM = new VehicleViewModel();
            WarehouseVM = new WarehouseViewModel();
            CurrentView = VehicleVM;

            VehicleViewCommand = new RelayCommand(o =>
            {
                CurrentView = VehicleVM;
            });
            
            WarehouseViewCommand = new RelayCommand(o =>
            {
                CurrentView = WarehouseVM;
            });
        }
    }
}
