using System;
using System.ComponentModel;

namespace PoputkeeLite.Core.Models
{
    /// <summary>
    /// Модель данных, представляющая поездку
    /// </summary>
    public class Trip : INotifyPropertyChanged
    {
        #region Приватные поля
        private string _from;
        private string _to;
        private DateTime _date;
        private TimeSpan _time;
        private int _seatsAvailable;
        #endregion

        #region Свойства с уведомлениями об изменениях
        /// <summary>
        /// Уникальный идентификатор поездки
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин водителя, создавшего поездку
        /// </summary>
        public string DriverLogin { get; set; }

        /// <summary>
        /// Пункт отправления
        /// </summary>
        public string From
        {
            get => _from;
            set
            {
                if (_from != value)
                {
                    _from = value;
                    OnPropertyChanged(nameof(From));
                }
            }
        }

        /// <summary>
        /// Пункт назначения
        /// </summary>
        public string To
        {
            get => _to;
            set
            {
                if (_to != value)
                {
                    _to = value;
                    OnPropertyChanged(nameof(To));
                }
            }
        }

        /// <summary>
        /// Дата поездки
        /// </summary>
        public DateTime Date
        {
            get => _date;
            set
            {
                if (_date != value)
                {
                    _date = value;
                    OnPropertyChanged(nameof(Date));
                }
            }
        }

        /// <summary>
        /// Время отправления
        /// </summary>
        public TimeSpan Time
        {
            get => _time;
            set
            {
                if (_time != value)
                {
                    _time = value;
                    OnPropertyChanged(nameof(Time));
                }
            }
        }

        /// <summary>
        /// Количество доступных мест
        /// </summary>
        public int SeatsAvailable
        {
            get => _seatsAvailable;
            set
            {
                if (_seatsAvailable != value)
                {
                    _seatsAvailable = value;
                    OnPropertyChanged(nameof(SeatsAvailable));
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