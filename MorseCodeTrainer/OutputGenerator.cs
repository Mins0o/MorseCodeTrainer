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
using Microsoft.Win32;

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
                        if (worker.CancellationPending)
                        {
                            ea.Cancel = true;
                            break;
                        }
                    }
                }
            };
            worker.WorkerSupportsCancellation = true;
            worker.RunWorkerCompleted += (s, ea) => {((Button)play_button).IsEnabled = true; };
            worker.RunWorkerAsync();
        }
        public void stop()
        {
            worker.CancelAsync();
        }
        public void save(string title)
        {
            int cutOff = 8;
            if (title.Length < 8)
            {
                cutOff = title.Length;
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog()
            {
                FileName = "M-Code of \"" + title.Substring(0, cutOff) + "...\"",
                DefaultExt = ".wav",
                Filter = "Wav Audio(.wav)|*.wav"
            };
            Nullable<bool> result = saveFileDialog.ShowDialog();
            if (result == true)
            {
                WaveFileWriter.CreateWaveFile16(saveFileDialog.FileName, thingToOutPut);
            }
        }
    }
}
