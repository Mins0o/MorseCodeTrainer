using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using NAudio;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System.Windows.Controls;
using System.ComponentModel;

namespace MorseCodeTrainer
{
    class OutputGenerator
    {
        BackgroundWorker worker;
        private ISampleProvider thingToOutPut;
        public OutputGenerator(ISampleProvider theCode)
        {
            thingToOutPut = theCode;
        }
        public delegate void PlayEndedEventHandler(object source, EventArgs args);
        public event PlayEndedEventHandler PlayEnded;
        protected virtual void OnPlayEnded()
        {
            PlayEnded?.Invoke(this, EventArgs.Empty);
        }
        public void play(object play_button)
        {
            worker = new BackgroundWorker();
            worker.DoWork += (s, ea) =>
            {
                using (var wo = new WaveOutEvent() { DeviceNumber = 1 })
                {
                    wo.Init(thingToOutPut);
                    wo.Play();
                    while (wo.PlaybackState == PlaybackState.Playing)
                    {
                        Thread.Sleep(100);
                    }
                }
            };
            worker.RunWorkerCompleted += (s, ea) => {((Button)play_button).IsEnabled = true; };
            worker.RunWorkerAsync();
        }
    }
}
