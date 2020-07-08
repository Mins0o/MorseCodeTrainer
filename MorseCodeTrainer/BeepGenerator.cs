using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;

namespace MorseCodeTrainer
{
    class BeepGenerator
    {
        private int speed;
        private readonly double shortDuration;
        private readonly double longDuration;
        private readonly double pauseInterval;
        private readonly double baseBeat = 0.03;
        public BeepGenerator(int speed)
        {
            if (speed > 10 || speed < 1)
            {
                System.Windows.MessageBox.Show("The speed should be within 1 to 10");
                speed = 5;
            }
            this.speed = speed;
            this.shortDuration = baseBeat * (11 - speed);
            this.longDuration = baseBeat * (11 - speed*0.9) * 2;
            this.pauseInterval = baseBeat * (11 - speed * 0.9);
        }
        //default constructor - speed is 5
        public BeepGenerator():this(5)
        {
        }

        private ISampleProvider beep(double duration)
        {
            ISampleProvider beepSound = new SignalGenerator()
            {
                Gain = 0.5,
                Frequency = 500,
                Type = SignalGeneratorType.Sin
            }.Take(TimeSpan.FromSeconds(duration));
            return beepSound;
        }
        public ISampleProvider shortBeep(){
            return this.beep(this.shortDuration);
        }
        public ISampleProvider longBeep()
        {
            return this.beep(this.longDuration);
        }
        private ISampleProvider longPause() { 
            return new SignalGenerator()
            {
                Gain = 0
            }.Take(TimeSpan.FromSeconds(longDuration));
        }
        public ISampleProvider stringInput(string userInput, int speed = 5)
        {
            ISampleProvider addToThis = new SignalGenerator()
            {
                Gain = 0
            }.Take(TimeSpan.FromSeconds(0.5));
            if (speed > 10 || speed < 1)
            {
                System.Windows.MessageBox.Show("The speed should be within 1 to 10");
                return addToThis;
            }
            for (int i = 0; i < userInput.Length; i++)
            {
                if (userInput[i] == '1')
                {
                    addToThis = addToThis.FollowedBy(TimeSpan.FromSeconds(baseBeat*(4.3-speed/3)), beep(shortDuration));
                }
                else if (userInput[i] == '2')
                {
                    addToThis = addToThis.FollowedBy(TimeSpan.FromSeconds(baseBeat * (4.3-speed / 3)), beep(longDuration));
                }
                else
                {
                    addToThis = addToThis.FollowedBy(longPause());
                }
            }
            return addToThis;
        }
    }
}
