using System;
using System.Globalization;
using System.Windows.Data;

namespace PoputkeeLite.Core.Common
{
    /// <summary>
    /// Конвертер, преобразующий целое число в булево значение:
    /// true - если число больше нуля, false - в противном случае
    /// </summary>
    public class GreaterThanZeroConverter : IValueConverter
    {
        #region Реализация IValueConverter

        /// <summary>
        /// Преобразует целое число в булево значение
        /// </summary>
        /// <param name="value">Входное значение (ожидается int)</param>
        /// <param name="targetType">Тип цели преобразования (игнорируется)</param>
        /// <param name="parameter">Дополнительный параметр (не используется)</param>
        /// <param name="culture">Информация о культуре (игнорируется)</param>
        /// <returns>
        /// true - если значение является int и больше 0, 
        /// false - во всех остальных случаях
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is int intValue)
            {
                return intValue > 0;
            }
            return false;
        }

        /// <summary>
        /// Обратное преобразование не поддерживается
        /// </summary>
        /// <exception cref="NotImplementedException">Всегда выбрасывает исключение</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Обратное преобразование не поддерживается");
        }

        #endregion
    }
}