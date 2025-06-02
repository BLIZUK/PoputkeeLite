using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace PoputkeeLite.Core.Common
{
    /// <summary>
    /// Базовый класс для всех ViewModel, реализующий INotifyPropertyChanged
    /// и предоставляющий общие функции валидации
    /// </summary>
    public class BaseViewModel : INotifyPropertyChanged
    {
        #region Реализация INotifyPropertyChanged
        /// <summary>
        /// Событие, возникающее при изменении значения свойства
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Вызывает событие PropertyChanged для указанного свойства
        /// </summary>
        /// <param name="propertyName">Имя изменившегося свойства (автоподстановка через CallerMemberName)</param>
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        #region Методы валидации
        /// <summary>
        /// Проверяет валидность email-адреса
        /// </summary>
        /// <param name="email">Проверяемый email</param>
        /// <returns>True если email валиден, иначе False</returns>
        protected bool ValidateEmail(string email)
        {
            return !string.IsNullOrWhiteSpace(email) &&
                   Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }

        /// <summary>
        /// Проверяет валидность номера телефона
        /// </summary>
        /// <param name="phone">Проверяемый номер телефона</param>
        /// <returns>True если номер телефона валиден, иначе False</returns>
        protected bool ValidatePhone(string phone)
        {
            return !string.IsNullOrWhiteSpace(phone) &&
                   Regex.IsMatch(phone, @"^\+?[0-9\s\-\(\)]{7,20}$");
        }

        /// <summary>
        /// Проверяет, что строка не является пустой или состоящей из пробелов
        /// </summary>
        /// <param name="value">Проверяемое значение</param>
        /// <returns>True если значение не пустое, иначе False</returns>
        protected bool ValidateRequired(string value)
        {
            return !string.IsNullOrWhiteSpace(value);
        }

        /// <summary>
        /// Проверяет, что число является положительным
        /// </summary>
        /// <param name="value">Проверяемое число</param>
        /// <returns>True если число положительное, иначе False</returns>
        protected bool ValidatePositiveNumber(int value)
        {
            return value > 0;
        }
        #endregion
    }
}