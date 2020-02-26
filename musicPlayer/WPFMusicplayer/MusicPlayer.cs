using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using NAudio.Wave;

namespace WPFMusicplayer
{
    public class MusicPlayer : INotifyPropertyChanged
    {
        #region Field
        
        private DirectSoundOut directSoundOut;

        private bool IsPositionTicking;

        public event PropertyChangedEventHandler PropertyChanged;

        private readonly DispatcherTimer positionTimer = new DispatcherTimer();

        #endregion


        private MusicPlayer()
        {
            isPlaying = false;
            positionTimer.Interval = TimeSpan.FromMilliseconds(50);// every 50 ms fire PositionTick event
            positionTimer.Tick += PositionTick;

        }


        public WaveStream ActiveStream
        { get; set; }

        private bool isPlaying;
        public bool IsPlaying
        {
            get { return isPlaying; }
            set
            {
                isPlaying = value;
                OnPropertyChanged("IsPlaying");
            }
        }

        #region SingleTon
        public static MusicPlayer instance;

        public static MusicPlayer Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new MusicPlayer();
                }
                return instance;
            }
        }
        #endregion


        #region Properties
        private double musicPosition;

        public double MusicPosition
        {
            get { return musicPosition; }
            set
            {

            }
        }

        #endregion

        #region FileRegardMethod
        private void InitWAV(string filePath)
        {
            //source
            ActiveStream = new WaveFileReader(filePath);

            directSoundOut = new DirectSoundOut();

            directSoundOut.Init(new WaveChannel32(ActiveStream));

        }

        private void InitMP3(string filePath)
        {
            WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(filePath));

            ActiveStream = new BlockAlignReductionStream(pcm);

            directSoundOut = new DirectSoundOut();

            directSoundOut.Init(ActiveStream);

            // Change the button to pause shape

        }


        public void IntiFile(MusicElement file)
        {
            //stop a song currently playing
            this.StopAndCloseStream();


            //and close the stream of current song

            if (!System.IO.File.Exists(file.Path))//if file not exsist than return
                return;
            try
            {
                if (file.Type == MusicElement.Extension.MP3)
                    InitMP3(file.Path);
                else
                    InitWAV(file.Path);
            }
            catch
            {
                MessageBox.Show("Exception occurs");
                ActiveStream = null;

            }

            this.Play();

        }

        private void StopAndCloseStream()
        {
            IsPlaying = false;
            if (directSoundOut != null)
            {
                directSoundOut.Stop();
            }
            if (ActiveStream != null)//Check object hoiding resources(pcm or MP3Reader)
            {
                ActiveStream.Close();
                ActiveStream = null;
            }
            if (directSoundOut != null)
            {
                directSoundOut.Dispose();
                directSoundOut = null;
            }

        }
        #endregion

        #region ControlRegardMethod
        public void Play()
        {
            IsPlaying = true;
            directSoundOut.Play();

        }
        public void Stop()
        {
            IsPlaying = false;
            directSoundOut?.Stop();
        }
         
        public void Pause()
        {
            IsPlaying = false;
            directSoundOut?.Pause();
        }
        #endregion

        #region EventHandler

        private void PositionTick(object sender, EventArgs e)
        {
            IsPositionTicking = true;

            if(ActiveStream != null)
                MusicPosition = ((double)ActiveStream.Position / (double)ActiveStream.Length) * ActiveStream.TotalTime.TotalSeconds;

            IsPositionTicking = false;
        }

        public WaveStream GetCurrentWaveStream()
        {
            return ActiveStream;
        }

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion 
    }
}
