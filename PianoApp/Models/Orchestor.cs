using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
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

        private readonly IKeyView _view;
        public Orchestor(IKeyView view)
        {
            _view = view;
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
        /// Sets Volume property of all MediaPlayers in Wrappers to given volume
        /// </summary>
        /// <param name="volume"></param>
        public void SetSoundVolume(double volume)
        {
            List<MediaPlayerWrapper> allMedia = new List<MediaPlayerWrapper>();
            allMedia.AddRange(PlayingMedia);
            allMedia.AddRange(StoppedMedia);

            foreach (MediaPlayerWrapper item in allMedia)
            {
                _view.GetDispatcher().Invoke(() =>
                {
                    item.MediaPlayer.Volume = volume / 100;
                });
            }
        }
    }
}
