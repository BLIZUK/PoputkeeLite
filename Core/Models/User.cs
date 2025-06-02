using System.ComponentModel;

namespace PoputkeeLite.Core.Models
{
    /// <summary>
    /// Модель данных, представляющая пользователя приложения
    /// </summary>
    public class User : INotifyPropertyChanged
    {
        #region Приватные поля
        private string _name;
        private string _email;
        private string _phone;
        #endregion

        #region Свойства с уведомлениями об изменениях
        /// <summary>
        /// Уникальный логин пользователя
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Отображаемое имя пользователя
        /// </summary>
        public string Name
        {
            get => _name;
            set
            {
                if (_name != value)
                {
                    _name = value;
                    OnPropertyChanged(nameof(Name));
                }
            }
        }

        /// <summary>
        /// Адрес электронной почты пользователя
        /// </summary>
        public string Email
        {
            get => _email;
            set
            {
                if (_email != value)
                {
                    _email = value;
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        /// <summary>
        /// Контактный телефон пользователя
        /// </summary>
        public string Phone
        {
            get => _phone;
            set
            {
                if (_phone != value)
                {
                    _phone = value;
                    OnPropertyChanged(nameof(Phone));
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