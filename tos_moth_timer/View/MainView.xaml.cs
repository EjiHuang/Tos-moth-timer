using System;
using System.Media;
using System.Windows;
using System.Windows.Media;
using System.Windows.Threading;

namespace tos_moth_timer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public int _time = 0;
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

            // init hook
            // _hook = new KeyboardHook();
            // _hook.KeyUp += _hook_KeyUp;
        }

        /// <summary>
        /// 全局键盘hook
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _hook_KeyUp(object sender, HookEventArgs e)
        {
            if (e.Alt == true)
            {
                if (e.Key == System.Windows.Forms.Keys.F11)
                {
                    txt_timer.Text = (++_time).ToString();
                    _timer.Start();
                }
                if (e.Key == System.Windows.Forms.Keys.F12)
                {
                    _time = 0;
                    txt_timer.Text = _time.ToString();
                    _timer.Stop();
                }
            }
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

            if (_time >= 60)
            {
                txt_timer.Foreground = Brushes.Red;
            }
            else
            {
                txt_timer.Foreground = Brushes.Green;
            }

            if (_time == 60)
            {
                // 语音提示15秒后注视
                sound1.Play();
            }
            else if (_time == 75)
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
            _timer.Stop();
        }

        /// <summary>
        /// 帮助
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Help_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(
                "快捷键：" + Environment.NewLine +
                "  启动(Alt+F11)、停止(Alt+F12)" + Environment.NewLine +
                "作者主页：" + Environment.NewLine +
                "  https://github.com/JerryAJ", "Help", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
