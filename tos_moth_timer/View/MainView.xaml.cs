using System;
using System.Media;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;

namespace tos_moth_timer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _time = 0;
        private int _interval = 75;
        private readonly DispatcherTimer _timer;
        SoundPlayer sound1 = new SoundPlayer(Properties.Resources._60);
        SoundPlayer sound2 = new SoundPlayer(Properties.Resources._75);

        // keyboard hook
        // private KeyboardHook _hook;

        public MainWindow()
        {
            InitializeComponent();

            // init timer
            _timer = new DispatcherTimer(new TimeSpan(0, 0, 1), DispatcherPriority.Normal, Timer_Tick, Dispatcher);
            _timer.Stop();
        }

        /// <summary>
        /// 计时事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Timer_Tick(object sender, EventArgs e)
        {
            ++_time;
            txt_timer.Text = _time.ToString();

            if (_time >= (_interval - 15))
            {
                txt_timer.Foreground = Brushes.Red;
            }
            else
            {
                txt_timer.Foreground = Brushes.Green;
            }

            if (_time == (_interval - 15))
            {
                // 语音提示15秒后注视
                sound1.Play();
            }
            else if (_time == _interval)
            {
                // 语音提示出现注视
                sound2.Play();
                // 重置
                _time = 0;
            }
        }

        /// <summary>
        /// 启动按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_start_Click(object sender, RoutedEventArgs e)
        {
            txt_timer.Text = (++_time).ToString();
            _timer.Start();

            mi_start.IsChecked = true;
            mi_stop.IsChecked = false;
        }

        /// <summary>
        /// 停止按钮单击事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_stop_Click(object sender, RoutedEventArgs e)
        {
            _time = 0;
            txt_timer.Text = _time.ToString();
            txt_timer.Foreground = Brushes.Green;
            _timer.Stop();

            mi_start.IsChecked = false;
            mi_stop.IsChecked = true;
        }

        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "项目主页：" + Environment.NewLine +
                "  https://github.com/JerryAJ/Tos-moth-timer", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        /// <summary>
        /// 间隔改变
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tb_interval_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            _interval = int.Parse((sender as TextBox).Text);
        }
    }
}
