using System;
using System.Collections.ObjectModel;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Threading;
using System.Threading.Tasks;

namespace PingGUI
{
    /// <summary>
    /// Логика взаимодействия для PingWindow.xaml
    /// </summary>
    public partial class PingWindow : Window
    {
        public ObservableCollection<PingerResults> results = null;
        private int Interval { get; set; }
        private DispatcherTimer tm;
        private string Host { get; set; }
        public PingWindow(string _host, int _interval)
        {
            InitializeComponent();
            this.Host = _host;
            this.Interval = _interval;
 if (results == null) results = new ObservableCollection<PingerResults>();
            resultsGrid.ItemsSource = results;
            UpdateValuesAsync();
            if (this.Interval > 0)
            {
                tm = new DispatcherTimer();
                tm.Interval = new TimeSpan(0, 0, Interval);
                tm.Tick += UpdateValuesAsync;
                tm.Start();
            }



        }
        private async void UpdateValuesAsync(object sender = null, EventArgs e = null)
        {
            try
            {
                using (Ping ping = new Ping())
                {
                    PingReply reply = await ping.SendPingAsync(this.Host);
                    results.Add(new PingerResults { DT = DateTime.Now.ToString(), RTT = reply.RoundtripTime.ToString(), TTL = reply.Options.Ttl.ToString() });
                }
            }
            catch (Exception ex)
            {
                if (tm != null) tm.Stop();
                MessageBox.Show("Произошла ошибка при пинговании хоста. Проверьте правильность IP адреса или имени ресурса и повторите попытку.");
                this.Close();
            }
        }
        private void PingButton(object sender, RoutedEventArgs e)        {
            UpdateValuesAsync();
        }
        private void ExportToCSVButton(object sender, RoutedEventArgs e)
        {
            if (results != null)
            {
                if (ExportToCSV.Export(results, "report.csv")) MessageBox.Show("Данные были успешно экспортированы");
                else MessageBox.Show("Не удалось экспортировать данные");
            }
        }
        private void ExitButton(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }
    }
}
