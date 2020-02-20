using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using NAudio.Wave;

namespace WPFMusicplayer
{
    public class MusicPlayer
    {

        public static MusicPlayer _instance;
        //Source and sync = where the data is going to 
        private WaveFileReader waveStream;

        private DirectSoundOut directSoundOut;

        private BlockAlignReductionStream mp3Stream;

        public bool IsPlaying
        {
            get;set;
        }


        private MusicPlayer() 
        {
            IsPlaying = false;


        }
        public static MusicPlayer Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MusicPlayer();
                }
                return _instance;
            }
        }

        private void InitWAV(string filePath)
        {
            //source
            waveStream = new WaveFileReader(filePath);

            directSoundOut = new DirectSoundOut();

            directSoundOut.Init(new WaveChannel32(waveStream));

        }

        private void InitMP3(string filePath)
        {
            WaveStream pcm = WaveFormatConversionStream.CreatePcmStream(new Mp3FileReader(filePath));

            mp3Stream = new BlockAlignReductionStream(pcm);

            directSoundOut = new DirectSoundOut();

            directSoundOut.Init(mp3Stream);

            // Change the button to pause shape

        }

        public void IntiFile(MusicElement file)
        {
            //stop a song currently playing
            Stop();


            //and close the stream of current song

            if (!System.IO.File.Exists(file.Path))//if file not exsist than return
                return;
            try
            {
                MessageBox.Show("" + (System.IO.Path.GetExtension((TagLib.File.Create(file.Path)).Name)));
                if (file.Type == MusicElement.Extension.MP3)
                    InitMP3(file.Path);
                else
                    InitWAV(file.Path);
            }
            catch
            {
                MessageBox.Show("Exception occurs");
                mp3Stream = null;
                waveStream = null;

            }

            this.Play();

        }
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

        private void StopAndCloseStream()
        {
            IsPlaying = false;
            if (directSoundOut != null)
            {
                directSoundOut.Stop();
            }
            if(waveStream != null)
            {
                waveStream.Close(); 
                waveStream = null;
            }
            if(mp3Stream != null)
            {
                mp3Stream.Close();//Check object hoiding resources(pcm or MP3Reader)
                mp3Stream = null;
            }
            if (directSoundOut != null)
            {
                directSoundOut.Dispose();
                directSoundOut = null;
            }

        }

    }
}
