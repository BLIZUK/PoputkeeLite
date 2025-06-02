using System.ComponentModel;

namespace PoputkeeLite.Core.Models
{
    /// <summary>
    /// Модель данных, представляющая бронирование поездки пассажиром
    /// </summary>
    public class Booking : INotifyPropertyChanged
    {
        #region Свойства с уведомлениями об изменениях
        private string _status;
        private Trip _tripDetails;

        /// <summary>
        /// Идентификатор связанной поездки
        /// </summary>
        public int TripId { get; set; }

        /// <summary>
        /// Логин пассажира, осуществившего бронирование
        /// </summary>
        public string PassengerLogin { get; set; }

        /// <summary>
        /// Статус бронирования (Active, Completed, Canceled и т.д.)
        /// </summary>
        public string Status
        {
            get => _status;
            set
            {
                if (_status != value)
                {
                    _status = value;
                    OnPropertyChanged(nameof(Status));
                }
            }
        }

        /// <summary>
        /// Детали связанной поездки
        /// </summary>
        public Trip TripDetails
        {
            get => _tripDetails;
            set
            {
                if (_tripDetails != value)
                {
                    _tripDetails = value;
                    OnPropertyChanged(nameof(TripDetails));
                }
            }
        }
        #endregion

        #region Реализация INotifyPropertyChanged
        /// <summary>
        /// Событие, возникающее при изменении значения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged для указанного свойства
        /// </summary>
        /// <param name="propertyName">Имя изменившегося свойства</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
    }
}