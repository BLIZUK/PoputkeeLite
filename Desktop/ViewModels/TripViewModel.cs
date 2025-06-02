// ViewModels/TripViewModel.cs
using PoputkeeLite.Core.Common;
using PoputkeeLite.Core.Models;
using PoputkeeLite.Core.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PoputkeeLite.ViewModels
{
    public class TripViewModel : BaseViewModel
    {
        public bool HasErrors =>
        !ValidateRequired(From) ||
        !ValidateRequired(To) ||
        !ValidatePositiveNumber(SeatsAvailable);

        private string _from;
        private string _to;
        private DateTime _date = DateTime.Today;
        private TimeSpan _time = DateTime.Now.TimeOfDay;
        private int _seatsAvailable = 1;
        private Trip _selectedTrip;

        //NEW
        private string _filterFrom;
        private string _filterTo;
        private DateTime? _filterDate;

        public string From
        {
            get => _from;
            set { _from = value; OnPropertyChanged(); }
        }

        public string To
        {
            get => _to;
            set { _to = value; OnPropertyChanged(); }
        }

        public DateTime Date
        {
            get => _date;
            set { _date = value; OnPropertyChanged(); }
        }

        public TimeSpan Time
        {
            get => _time;
            set { _time = value; OnPropertyChanged(); }
        }

        public int SeatsAvailable
        {
            get => _seatsAvailable;
            set { _seatsAvailable = value; OnPropertyChanged(); }
        }

        public Trip SelectedTrip
        {
            get => _selectedTrip;
            set { _selectedTrip = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Trip> AvailableTrips { get; } = new ObservableCollection<Trip>();

        public ICommand CreateTripCommand { get; }
        public ICommand BookTripCommand { get; }
        public ICommand RefreshTripsCommand { get; }

        public TripViewModel()
        {
            CreateTripCommand = new RelayCommand(_ => CreateTrip());
            BookTripCommand = new RelayCommand(_ => BookSelectedTrip());
            RefreshTripsCommand = new RelayCommand(_ => RefreshTrips());

            RefreshTrips(); // Загружаем поездки при инициализации
        }

        public string ErrorMessage
        {
            get
            {
                if (!ValidateRequired(From)) return "Укажите пункт отправления";
                if (!ValidateRequired(To)) return "Укажите пункт назначения";
                if (!ValidatePositiveNumber(SeatsAvailable)) return "Количество мест должно быть положительным числом";
                return null;
            }
        }
        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка валидации", MessageBoxButton.OK, MessageBoxImage.Warning);
        }

        private void CreateTrip()
        {
            if (HasErrors)
            {
                ShowError(ErrorMessage);
                return;
            }
            if (string.IsNullOrWhiteSpace(From)) return;
            if (string.IsNullOrWhiteSpace(To)) return;
            if (SeatsAvailable <= 0) return;

            var newTrip = new Trip
            {
                DriverLogin = App.CurrentUser.Login,
                From = From,
                To = To,
                Date = Date,
                Time = Time,
                SeatsAvailable = SeatsAvailable
            };

            var tripService = new TripService();
            tripService.AddTrip(newTrip);

            // Очистка формы
            From = string.Empty;
            To = string.Empty;
            SeatsAvailable = 1;

            // Обновляем список
            RefreshTrips();
        }

        private void BookSelectedTrip()
        {
            if (SelectedTrip == null || SelectedTrip.SeatsAvailable <= 0) return;

            var tripService = new TripService();
            tripService.BookTrip(SelectedTrip.Id, App.CurrentUser.Login);

            // Обновляем список
            RefreshTrips();
        }

        private void RefreshTrips()
        {
            var tripService = new TripService();
            var trips = tripService.GetAllTrips()
                .Where(t => t.Date >= DateTime.Today) // только будущие поездки
                .ToList();

            _allTrips = tripService.GetAllTrips().Where(t => t.Date >= DateTime.Today).ToList();

            FilterTrips();

            AvailableTrips.Clear();
            foreach (var trip in trips)
            {
                AvailableTrips.Add(trip);
            }
        }

        //NEW
        public string FilterFrom
        {
            get => _filterFrom;
            set { _filterFrom = value; OnPropertyChanged(); FilterTrips(); }
        }

        public string FilterTo
        {
            get => _filterTo;
            set { _filterTo = value; OnPropertyChanged(); FilterTrips(); }
        }

        public DateTime? FilterDate
        {
            get => _filterDate;
            set { _filterDate = value; OnPropertyChanged(); FilterTrips(); }
        }

        private void FilterTrips()
        {
            var trips = _allTrips.Where(t =>
                (string.IsNullOrEmpty(FilterFrom) || t.From.Contains(FilterFrom, StringComparison.OrdinalIgnoreCase)) &&
                (string.IsNullOrEmpty(FilterTo) || t.To.Contains(FilterTo, StringComparison.OrdinalIgnoreCase)) &&
                (!FilterDate.HasValue || t.Date.Date == FilterDate.Value.Date)
            ).ToList();

            AvailableTrips.Clear();
            foreach (var trip in trips)
            {
                AvailableTrips.Add(trip);
            }
        }

        private List<Trip> _allTrips = new List<Trip>();

    }
}