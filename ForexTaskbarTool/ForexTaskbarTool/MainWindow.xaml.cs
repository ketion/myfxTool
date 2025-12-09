using System;
using System.Windows;
using System.Windows.Threading;

namespace ForexTaskbarTool
{
    public partial class MainWindow : Window
    {
        private ForexService _forexService;
        private DispatcherTimer _timer;
        private string _currentSymbol = "XAUUSD";

        public MainWindow()
        {
            InitializeComponent();
            _forexService = new ForexService();
            
            // 设置定时器，每30秒更新一次
            _timer = new DispatcherTimer();
            _timer.Interval = TimeSpan.FromSeconds(30);
            _timer.Tick += Timer_Tick;
            _timer.Start();

            // 初始加载数据
            UpdateForexData();
        }

        private async void Timer_Tick(object sender, EventArgs e)
        {
            await UpdateForexData();
        }

        private async System.Threading.Tasks.Task UpdateForexData()
        {
            try
            {
                var data = await _forexService.GetForexDataAsync(_currentSymbol);
                if (data != null)
                {
                    string priceChange = data.Change >= 0 ? $"+{data.Change:F2}" : $"{data.Change:F2}";
                    string arrow = data.Change >= 0 ? "↑" : "↓";
                    TaskbarIcon.ToolTipText = $"{_currentSymbol}\n{data.Price:F2} {arrow}\n{priceChange}";
                }
            }
            catch (Exception ex)
            {
                TaskbarIcon.ToolTipText = $"{_currentSymbol}\n获取数据失败";
            }
        }

        private void SwitchToXAUUSD(object sender, RoutedEventArgs e)
        {
            _currentSymbol = "XAUUSD";
            UpdateForexData();
        }

        private void SwitchToEURUSD(object sender, RoutedEventArgs e)
        {
            _currentSymbol = "EURUSD";
            UpdateForexData();
        }

        private void SwitchToGBPUSD(object sender, RoutedEventArgs e)
        {
            _currentSymbol = "GBPUSD";
            UpdateForexData();
        }

        private void RefreshData(object sender, RoutedEventArgs e)
        {
            UpdateForexData();
        }

        private void ExitApplication(object sender, RoutedEventArgs e)
        {
            _timer.Stop();
            TaskbarIcon.Dispose();
            Application.Current.Shutdown();
        }

        private void TaskbarIcon_TrayMouseDoubleClick(object sender, RoutedEventArgs e)
        {
            UpdateForexData();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);
            TaskbarIcon.Dispose();
        }
    }
}
