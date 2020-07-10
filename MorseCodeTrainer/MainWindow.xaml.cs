using System;
using System.Collections.Generic;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.ComponentModel;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using Microsoft.Win32;

namespace MorseCodeTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OutputGenerator og;
        public MainWindow()
        {
            InitializeComponent();
            for (int n = 0; n < WaveOut.DeviceCount; n++)
            {
                var caps = WaveOut.GetCapabilities(n);
                TextBlock deviceItem = new TextBlock()
                {
                    Text = caps.ProductName
                };
                device_list.Items.Add(deviceItem);
                Console.WriteLine("{0}: {1}", n, caps.ProductName);
            }
            device_list.SelectedIndex = 0;
        }

        private void play(object sender, RoutedEventArgs e)
        {
            play_button.IsEnabled = false;

            int deviceNumber = device_list.SelectedIndex;
            System.Diagnostics.Debug.Print(deviceNumber.ToString());
            int speed = (Int32)speed_scroll.Value;
            Morse m = new Morse(user_input.Text);
            BeepGenerator bg = new BeepGenerator(speed);
            var theCode = bg.stringInput(m.translateToMorse());
            this.og = new OutputGenerator(theCode,deviceNumber);
            og.play(play_button);
        }private void stopPlaying(object sender, RoutedEventArgs e)
        {
            og.stop();
        }

        private void updateSpeedIndi(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int speed = (Int32)speed_scroll.Value;
            speed_indicator.Text = "Playback speed : "+speed;
        }

        private void Save(object sender, RoutedEventArgs e)
        {
            string title = user_input.Text;
            int speed = (Int32)speed_scroll.Value;
            Morse m = new Morse(user_input.Text);
            BeepGenerator bg = new BeepGenerator(speed);
            var theCode = bg.stringInput(m.translateToMorse());
            var og = new OutputGenerator(theCode);
            og.save(title);
        }
    }

}
