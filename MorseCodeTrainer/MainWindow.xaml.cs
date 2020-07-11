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
using System.Diagnostics;

namespace MorseCodeTrainer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private OutputGenerator og;
        private bool inputModeBool = false;
        private Stopwatch timeChecker = new Stopwatch();
        private UpDownToMorse toMorse;
        private Boolean aPressed = false;
        private Boolean kPressed = false;
        public MainWindow()
        {
            System.Diagnostics.Debug.Print(((int)'ㅢ').ToString());
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
            CloseInputMode();

            int deviceNumber = device_list.SelectedIndex;
            System.Diagnostics.Debug.Print(deviceNumber.ToString());
            int speed = (Int32)speed_scroll.Value;
            MorseToSound m = new MorseToSound(user_input.Text);
            BeepGenerator bg = new BeepGenerator(speed);
            var theCode = bg.stringInput(m.translateToMorse());
            this.og = new OutputGenerator(theCode,deviceNumber);
            og.play(play_button);
        }
        private void stopPlaying(object sender, RoutedEventArgs e)
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
            CloseInputMode();
            string title = user_input.Text;
            int speed = (Int32)speed_scroll.Value;
            MorseToSound m = new MorseToSound(user_input.Text);
            BeepGenerator bg = new BeepGenerator(speed);
            var theCode = bg.stringInput(m.translateToMorse());
            var og = new OutputGenerator(theCode);
            og.save(title);
        }

        private void InputMode(object sender, RoutedEventArgs e)
        {
            if (inputModeBool)
            {
                CloseInputMode();                
            }
            else
            {
                OpenInputMode();
            }
            this.inputModeBool = !inputModeBool;
        }
        private void OpenInputMode()
        {
            alpha_button.Visibility=Visibility.Visible;
            kor_button.Visibility = Visibility.Visible;
            input_mode_toggle.Content = "Input Mode : ON ";
            toMorse = new UpDownToMorse();
            timeChecker.Stop();
            timeChecker.Reset();
        }
        private void CloseInputMode()
        {
            alpha_button.Visibility = Visibility.Hidden;
            kor_button.Visibility = Visibility.Hidden;
            input_mode_toggle.Content = "Input Mode : OFF";
            try
            {
                user_input.Text = toMorse.TranslatedText();
            }
            catch
            {
                System.Windows.MessageBox.Show("Cannot convert this beep sequence to Morse code");
            }
        }
        private void TimingStarts(char lang)
        {
            timeChecker.Stop();
            long offTime = timeChecker.ElapsedMilliseconds;
            toMorse.AddToBuffer(offTime, 'n',lang);
            timeChecker.Reset();
            timeChecker.Start();
        }
        private void TimingEnds(char lang)
        {
            timeChecker.Stop();
            long holdTime = timeChecker.ElapsedMilliseconds;
            toMorse.AddToBuffer(holdTime,'p',lang);
            timeChecker.Reset();
            timeChecker.Start();
        }
        private void AlphaUp(object sender, MouseButtonEventArgs e)
        {
            if (aPressed)
            {
                TimingEnds('a');
                aPressed = !aPressed;
            }   
        }

        private void AlphaDown(object sender, MouseButtonEventArgs e)
        {
            if (!aPressed)
            {
                TimingStarts('a');
                aPressed = !aPressed;
            }
        }

        private void korDown(object sender, MouseButtonEventArgs e)
        {
            if (!kPressed)
            {
                TimingStarts('k');
                kPressed = !kPressed;
            }
        }

        private void korUp(object sender, MouseButtonEventArgs e)
        {
            if(kPressed)
            {
                TimingEnds('k');
                kPressed = !kPressed;
            }
        }

        private void KeyboardDown(object sender, KeyEventArgs e)
        {
            if (inputModeBool)
            {
                if (e.Key == Key.A)
                {
                    if (!aPressed)
                    {
                        TimingStarts('a');
                        aPressed = !aPressed;
                    }
                }
                if (e.Key == Key.K)
                {
                    if (!kPressed)
                    {
                        TimingStarts('k');
                        kPressed = !kPressed;
                    }
                }
                if (e.Key == Key.Return)
                {
                    CloseInputMode();
                }
            }
        }

        private void KeyboardUp(object sender, KeyEventArgs e)
        {
            if (inputModeBool)
            {
                if (e.Key == Key.A)
                {
                    if (aPressed)
                    {
                        TimingEnds('a');
                        aPressed = !aPressed;
                    }
                }
                if (e.Key == Key.K)
                {
                    if (kPressed)
                    {
                        TimingEnds('k');
                        kPressed = !kPressed;
                    }
                }
            }
        }
    }

}
