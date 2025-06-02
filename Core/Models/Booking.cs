// Models/Booking.cs (обновление)
using PoputkeeLite.Core.Models;
using System.ComponentModel;

namespace PoputkeeLite.Core.Models
{
    public class Booking : INotifyPropertyChanged
    {
        public int TripId { get; set; }
        public string PassengerLogin { get; set; }

        private string _status;
        public string Status
        {
            get => _status;
            set
            {
                _status = value;
                OnPropertyChanged(nameof(Status));
            }
        }

        private Trip _tripDetails;
        public Trip TripDetails
        {
            get => _tripDetails;
            set
            {
                _tripDetails = value;
                OnPropertyChanged(nameof(TripDetails));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}