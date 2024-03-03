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
        public VehicleViewModel VehicleVM { get; set; }
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
            CurrentView = VehicleVM;
        }
    }
}
