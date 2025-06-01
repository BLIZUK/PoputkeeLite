// ViewModels/MainViewModel.cs
using PoputkeeLite.Desktop.Views;
using System.Windows.Controls;
using PoputkeeLite.Core.Common;

namespace PoputkeeLite.Desktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        private UserControl _currentView;
        public UserControl CurrentView
        {
            get => _currentView;
            set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand NavigateToTripsCommand { get; }
        public RelayCommand NavigateToBookingsCommand { get; }
        public RelayCommand NavigateToAccountCommand { get; }

        public MainViewModel()
        {
            // По умолчанию открываем раздел поездок
            CurrentView = new TripView();

            NavigateToTripsCommand = new RelayCommand(_ => CurrentView = new TripView());
            //NavigateToBookingsCommand = new RelayCommand(_ => CurrentView = new BookingView());
            //NavigateToAccountCommand = new RelayCommand(_ => CurrentView = new AccountView());
        }
    }
}