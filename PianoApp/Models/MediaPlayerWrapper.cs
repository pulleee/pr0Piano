using System;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Media;
using PianoApp.Properties;
using PianoApp.Resources;

namespace PianoApp.Models
{
    public class MediaPlayerWrapper : INotifyPropertyChanged, IDisposable
    {
        /// <summary>
        /// Fires when media has ended
        /// </summary>
        public event EventHandler MediaHasEnded;

        public event PropertyChangedEventHandler PropertyChanged;

        public MediaPlayer MediaPlayer { get; set; }

        private readonly Orchestor _orchestor;

        public MediaPlayerWrapper(Orchestor orchestor)
        {
            _orchestor = orchestor;
            MediaPlayer = new MediaPlayer();
            // register on MediaEndedEvent
            MediaPlayer.MediaEnded += MediaPlayerOnMediaEnded;
        }

        /// <summary>
        /// Commands MediaPlayer to play sound
        /// </summary>
        /// <param name="parameter"></param>
        public void PlaySound(object parameter)
        {
            // when all players are in play, stop the first one to finish
            if (_orchestor.StoppedMedia.Count <= 1)
            {
                var player =_orchestor.PlayingMedia.First();
                player.MediaPlayer.Stop();
                _orchestor.PlayingMedia.Remove(player);
                _orchestor.StoppedMedia.Add(player);
            }

            _orchestor.PlayingMedia.Add(this);
            _orchestor.StoppedMedia.Remove(this);

            MediaPlayer.Open(new System.Uri(@SoundBar.GetSoundPathByIdent(int.Parse(parameter.ToString()))));
            MediaPlayer.Play();
        }

        private void MediaPlayerOnMediaEnded(object sender, EventArgs eventArgs)
        {
            _orchestor.PlayingMedia.Remove(this);
            _orchestor.StoppedMedia.Add(this);

            MediaHasEnded?.Invoke(sender, eventArgs);

            MediaPlayer.Stop();
            MediaPlayer.Close();
        }

        /// <summary>
        /// Disposes instance of MediaPlayerWrapper
        /// needed?
        /// </summary>
        public void Dispose()
        {
            MediaPlayer.MediaEnded -= MediaPlayerOnMediaEnded;
            MediaPlayer.Close();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
