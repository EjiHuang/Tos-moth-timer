using Newtonsoft.Json;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace tos_moth_timer.View
{
    /// <summary>
    /// SettingView.xaml 的交互逻辑
    /// </summary>
    public partial class SettingView : Window
    {
        public SettingView(MainWindow parent)
        {
            Owner = parent;
            InitializeComponent();

            // Show setting
            for (int i = 1; i < parent.settings.Count; i++)
            {
                var cbi = new ComboBoxItem { Content = parent.settings[i].Title };
                Cb_Settings.Items.Add(cbi);
            }
            Cb_Settings.SelectedIndex = parent.settings[0].ReadinessTime;

            TB_sound1.Text = parent.setting.Sound1;
            TB_sound2.Text = parent.setting.Sound2;
            TB_interval.Text = parent.setting.Interval.ToString();
            TB_readinessTime.Text = parent.setting.ReadinessTime.ToString();
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            MainWindow parent = Owner as MainWindow;
            // Save select index
            parent.settings[0].ReadinessTime = Cb_Settings.SelectedIndex;
            parent.settings[Cb_Settings.SelectedIndex + 1].Sound1 = TB_sound1.Text;
            parent.settings[Cb_Settings.SelectedIndex + 1].Sound2 = TB_sound2.Text;
            parent.settings[Cb_Settings.SelectedIndex + 1].Interval = int.Parse(TB_interval.Text);
            parent.settings[Cb_Settings.SelectedIndex + 1].ReadinessTime = int.Parse(TB_readinessTime.Text);
            // Update current setting
            parent.setting = parent.settings[Cb_Settings.SelectedIndex + 1];
            // Save setting.json
            var json = JsonConvert.SerializeObject(parent.settings, Formatting.Indented);
            File.WriteAllText("settings.json", json, Encoding.Default);

            Close();
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Cb_Settings_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Show updated setting
            MainWindow parent = Owner as MainWindow;
            TB_sound1.Text = parent.settings[Cb_Settings.SelectedIndex + 1].Sound1;
            TB_sound2.Text = parent.settings[Cb_Settings.SelectedIndex + 1].Sound2;
            TB_interval.Text = parent.settings[Cb_Settings.SelectedIndex + 1].Interval.ToString();
            TB_readinessTime.Text = parent.settings[Cb_Settings.SelectedIndex + 1].ReadinessTime.ToString();
        }
    }
}
