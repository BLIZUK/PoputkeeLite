using System;
using System.Globalization;
using System.Windows.Data;

namespace PoputkeeLite.Core.Common
{
    /// <summary>
    /// Конвертер, преобразующий любой объект в булево значение:
    /// true - если объект не null, false - если объект null
    /// </summary>
    public class NotNullConverter : IValueConverter
    {
        #region Реализация IValueConverter

        /// <summary>
        /// Преобразует объект в булево значение, указывающее на его существование
        /// </summary>
        /// <param name="value">Проверяемый объект</param>
        /// <param name="targetType">Тип цели преобразования (игнорируется)</param>
        /// <param name="parameter">Дополнительный параметр (не используется)</param>
        /// <param name="culture">Информация о культуре (игнорируется)</param>
        /// <returns>
        /// true - если значение не равно null,
        /// false - если значение равно null
        /// </returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value != null;
        }

        /// <summary>
        /// Обратное преобразование не поддерживается
        /// </summary>
        /// <exception cref="NotImplementedException">Всегда выбрасывает исключение</exception>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException("Обратное преобразование для NotNullConverter не поддерживается");
        }

        #endregion
    }
}