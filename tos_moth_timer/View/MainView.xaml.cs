using Newtonsoft.Json;
using SpeechLib;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using tos_moth_timer.View;

namespace tos_moth_timer
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private int _time = 0;
        private readonly DispatcherTimer _timer;
        private readonly SpVoice speech = new SpVoice();
        private readonly string pitch = "<pitch absmiddle=\"0\"/>";

        public SettingInfo setting;
        public List<SettingInfo> settings;

        public MainWindow()
        {
            // load setting.json
            string json = File.ReadAllText("settings.json", Encoding.Default);
            settings = JsonConvert.DeserializeObject<List<SettingInfo>>(json);
            setting = settings[settings[0].ReadinessTime + 1];

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

            if (_time >= (setting.Interval - setting.ReadinessTime))
            {
                txt_timer.Foreground = Brushes.Red;
            }
            else
            {
                txt_timer.Foreground = Brushes.Green;
            }

            if (_time == (setting.Interval - setting.ReadinessTime))
            {
                // 语音提示装备
                // sound1.Play();
                SpeechText(setting.Sound1);
            }
            else if (_time == setting.Interval)
            {
                // 语音提示开始
                // sound2.Play();
                SpeechText(setting.Sound2);
                // 重置
                _time = 0;
            }
        }

        /// <summary>
        /// Speech something?
        /// </summary>
        /// <param name="s"></param>
        private void SpeechText(string s)
        {
            speech.Speak(string.Empty, SpeechVoiceSpeakFlags.SVSFPurgeBeforeSpeak);
            speech.Speak(pitch + s, SpeechVoiceSpeakFlags.SVSFlagsAsync);
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
            System.Diagnostics.Process.Start("https://github.com/EjiHuang/Tos-moth-timer");
        }

        private void MenuItem_Setting_Click(object sender, RoutedEventArgs e)
        {
            new SettingView(this).ShowDialog();
        }
    }

    public class SettingInfo
    {
        public string Title { get; set; }
        public string Sound1 { get; set; }
        public string Sound2 { get; set; }
        public int Interval { get; set; }
        public int ReadinessTime { get; set; }
    }
}
