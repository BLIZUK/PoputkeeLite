// ViewModels/BookingViewModel.cs
using PoputkeeLite.Core.Common;
using PoputkeeLite.Core.Models;
using PoputkeeLite.Core.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;

namespace PoputkeeLite.Desktop.ViewModels
{
    public class BookingViewModel : BaseViewModel
    {
        private Booking _selectedActiveBooking;
        private Booking _selectedArchivedBooking;

        public Booking SelectedActiveBooking
        {
            get => _selectedActiveBooking;
            set { _selectedActiveBooking = value; OnPropertyChanged(); }
        }

        public Booking SelectedArchivedBooking
        {
            get => _selectedArchivedBooking;
            set { _selectedArchivedBooking = value; OnPropertyChanged(); }
        }

        public ObservableCollection<Booking> ActiveBookings { get; } = new ObservableCollection<Booking>();
        public ObservableCollection<Booking> ArchivedBookings { get; } = new ObservableCollection<Booking>();

        public ICommand RefreshBookingsCommand { get; }
        public ICommand CancelBookingCommand { get; }

        public BookingViewModel()
        {
            RefreshBookingsCommand = new RelayCommand(_ => RefreshBookings());
            CancelBookingCommand = new RelayCommand(_ => CancelSelectedBooking());

            RefreshBookings();
        }

        private void RefreshBookings()
        {
            // Архивируем завершенные поездки
            new BookingService().ArchiveCompletedTrips();

            // Загружаем бронирования пользователя
            var bookingService = new BookingService();
            var tripService = new TripService();
            var userBookings = bookingService.GetUserBookings(App.CurrentUser.Login);

            ActiveBookings.Clear();
            ArchivedBookings.Clear();

            foreach (var booking in userBookings)
            {
                var trip = tripService.GetAllTrips().FirstOrDefault(t => t.Id == booking.TripId);
                if (trip != null)
                {
                    booking.TripDetails = trip; // Добавляем детали поездки

                    if (booking.Status == "Active")
                        ActiveBookings.Add(booking);
                    else
                        ArchivedBookings.Add(booking);
                }
            }
        }

        private void CancelSelectedBooking()
        {
            if (NotificationService.ShowConfirmation("Вы уверены, что хотите отменить бронирование?"))
            {
                if (SelectedActiveBooking == null) return;

                var bookingService = new BookingService();
                bookingService.CancelBooking(
                    SelectedActiveBooking.TripId,
                    App.CurrentUser.Login);

                RefreshBookings();
                NotificationService.ShowInfo("Бронирование отменено");
            }

        }
    }
}