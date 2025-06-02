using System.Text;
using System.Windows;
using PoputkeeLite.Desktop.ViewModels;

namespace PoputkeeLite.Desktop.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        DataContext = new MainViewModel();
    }
    protected override void OnClosed(EventArgs e)
    {
        base.OnClosed(e);

        // Явно освобождаем ресурсы
        if (DataContext is IDisposable disposable)
        {
            disposable.Dispose();
        }

        // Закрываем все дочерние окна
        foreach (Window window in Application.Current.Windows)
        {
            if (window != this)
                window.Close();
        }
    }
}