using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace PoputkeeLite.Desktop.Behaviors
{
    public class TextBoxWatermarkBehavior : Behavior<TextBox>
    {
        public static readonly DependencyProperty WatermarkProperty =
            DependencyProperty.Register("Watermark", typeof(string), typeof(TextBoxWatermarkBehavior));

        public string Watermark
        {
            get => (string)GetValue(WatermarkProperty);
            set => SetValue(WatermarkProperty, value);
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            AssociatedObject.GotFocus += RemoveWatermark;
            AssociatedObject.LostFocus += ShowWatermark;
            ShowWatermark(null, null);
        }

        protected override void OnDetaching()
        {
            base.OnDetaching();
            AssociatedObject.GotFocus -= RemoveWatermark;
            AssociatedObject.LostFocus -= ShowWatermark;
        }

        private void RemoveWatermark(object sender, RoutedEventArgs e)
        {
            if (AssociatedObject.Text == Watermark)
            {
                AssociatedObject.Text = string.Empty;
                AssociatedObject.Foreground = Brushes.Black;
            }
        }

        private void ShowWatermark(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrEmpty(AssociatedObject.Text))
            {
                AssociatedObject.Text = Watermark;
                AssociatedObject.Foreground = Brushes.Gray;
            }
        }
    }
}