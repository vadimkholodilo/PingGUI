using System;
using System.Media;
using System.Windows;
using System.Windows.Input;

namespace PingGUI
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        private void CheckSimbles(object sender, TextCompositionEventArgs e)
        {
            var c = Convert.ToChar(e.Text);
            if (!char.IsDigit(c))
            {
                SystemSounds.Beep.Play();
                e.Handled = true;
            }
        }
        private void StartButton(object sender, RoutedEventArgs e)
        {
            {
                int _interval = 0;
                if (string.IsNullOrEmpty(host.Text)) return;
                if (!string.IsNullOrEmpty(interval.Text)) _interval = Convert.ToInt32(interval.Text);
                PingWindow window = new PingWindow(host.Text, _interval);
                window.ShowDialog();
            }
        }
    }
}
