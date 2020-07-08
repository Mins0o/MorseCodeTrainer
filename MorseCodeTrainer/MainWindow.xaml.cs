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
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MorseCodeTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        private void play(object sender, RoutedEventArgs e)
        {
            play_button.IsEnabled = false;
            Morse m = new Morse(user_input.Text);
            int speed = (Int32)speed_scroll.Value;
            BeepGenerator bg = new BeepGenerator(speed);
            var theCode = bg.stringInput(m.translateToMorse());
            Thread t = new Thread(() => playUnderNewThread(1, theCode));
            t.Start();
        }

        private void updateSpeedIndi(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int speed = (Int32)speed_scroll.Value;
            speed_indicator.Text = ""+speed;
        }

        private void playUnderNewThread(int deviceNum,ISampleProvider theCode)
        {
            using (var wo = new WaveOutEvent() { DeviceNumber = deviceNum })
            {
                wo.Init(theCode);
                wo.Play();
                while (wo.PlaybackState == PlaybackState.Playing)
                {
                    Thread.Sleep(100);
                }
            }

        }
    }

}
