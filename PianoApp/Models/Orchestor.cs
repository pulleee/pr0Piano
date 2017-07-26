using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using PianoApp.Views.Interfaces;

namespace PianoApp.Models
{
    public class Orchestor
    {
        /// <summary>
        /// Number of all MediaPlayerWrapper instaces in Orchestor class
        /// </summary>
        private const int PlayerInstances = 36;

        public IList<MediaPlayerWrapper> PlayingMedia;
        public IList<MediaPlayerWrapper> StoppedMedia;

        private double _soundVolume = 0.35;
        public Orchestor()
        {
            PlayingMedia = new List<MediaPlayerWrapper>();
            StoppedMedia = new List<MediaPlayerWrapper>();
            SetupMediaPlayer();
        }

        /// <summary>
        /// Sets up Orchestor with MediaPlayerWrapper instances
        /// </summary>
        private void SetupMediaPlayer()
        {
            for (int i = 0; i < PlayerInstances; i++)
            {
                var mediaPlayer = new MediaPlayerWrapper(this);
                StoppedMedia.Add(mediaPlayer);
            }
        }

        /// <summary>
        /// Gets the next available MediaPlayer
        /// </summary>
        /// <returns></returns>
        public MediaPlayerWrapper GetAvailableMediaPlayer()
        {
            return StoppedMedia.First();
        }

        /// <summary>
        /// Sets _volume property of all MediaPlayers in Wrappers to given volume
        /// </summary>
        /// <param name="volume"></param>
        public void SetSoundVolume(double volume)
        {
            _soundVolume = volume/100;
        }

        public double GetSoundVolume()
        {
            return _soundVolume;
        }
    }
}
