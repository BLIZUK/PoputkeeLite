using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PoputkeeLite.Core.Common
{
    /// <summary>
    /// Поведение для добавления watermark-подсказки в текстовые поля
    /// </summary>
    public static class WatermarkBehavior
    {
        #region Attached Property
        /// <summary>
        /// Регистрируемое attached свойство для watermark-текста
        /// </summary>
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.RegisterAttached(
                "Watermark",
                typeof(string),
                typeof(WatermarkBehavior),
                new PropertyMetadata(string.Empty, OnWatermarkChanged));

        /// <summary>
        /// Геттер для attached свойства Watermark
        /// </summary>
        public static string GetWatermark(Control control)
        {
            return (string)control.GetValue(WatermarkProperty);
        }

        /// <summary>
        /// Сеттер для attached свойства Watermark
        /// </summary>
        public static void SetWatermark(Control control, string value)
        {
            control.SetValue(WatermarkProperty, value);
        }
        #endregion

        #region Обработчики событий
        /// <summary>
        /// Обработчик изменения значения Watermark свойства
        /// </summary>
        private static void OnWatermarkChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is TextBox textBox)
            {
                // Подписка на события фокуса
                textBox.GotFocus += RemoveWatermark;
                textBox.LostFocus += ShowWatermark;

                // Инициализация watermark
                ShowWatermark(textBox, null);
            }
        }

        /// <summary>
        /// Удаляет watermark при получении фокуса
        /// </summary>
        private static void RemoveWatermark(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && GetWatermark(textBox) == textBox.Text)
            {
                textBox.Text = string.Empty;
                textBox.Foreground = Brushes.Black;
            }
        }

        /// <summary>
        /// Показывает watermark при потере фокуса и пустом поле
        /// </summary>
        private static void ShowWatermark(object sender, RoutedEventArgs e)
        {
            if (sender is TextBox textBox && string.IsNullOrEmpty(textBox.Text))
            {
                textBox.Text = GetWatermark(textBox);
                textBox.Foreground = Brushes.Gray;
            }
        }
        #endregion
    }
}