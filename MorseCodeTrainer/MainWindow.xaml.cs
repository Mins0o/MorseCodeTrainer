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

            int speed = (Int32)speed_scroll.Value;
            Morse m = new Morse(user_input.Text);
            BeepGenerator bg = new BeepGenerator(speed);
            var theCode = bg.stringInput(m.translateToMorse());
            var og = new OutputGenerator(theCode);
            og.play(play_button);
        }

        private void updateSpeedIndi(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            int speed = (Int32)speed_scroll.Value;
            speed_indicator.Text = ""+speed;
        }
    }

}
