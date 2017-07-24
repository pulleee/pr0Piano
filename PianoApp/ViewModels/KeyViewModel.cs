using System.Windows.Input;
using PianoApp.Models;
using PianoApp.Views.Interfaces;

namespace PianoApp.ViewModels
{
    //FMI: https://stackoverflow.com/questions/45110356/c-sharp-wpf-invoking-windows-media-player/45110480?noredirect=1#comment77210712_45110480
    public class KeyViewModel
    {
        public IKeyView View { get; set; }
        public Orchestor Orchestor { get; set; }

        private ICommand _keyPressedCommand;
        public ICommand KeyPressedCommand
        {
            get
            {
                return _keyPressedCommand ?? (_keyPressedCommand = new RelayCommand(
                           p => true,
                           TriggerSound));
            }
        }

        public KeyViewModel(IKeyView view)
        {
            View = view;
            Orchestor = new Orchestor(view);
        }

        /// <summary>
        /// Triggers sound while saying a available MediaPlayer in the Orchestor to start playing
        /// </summary>
        /// <param name="parameter"></param>
        private void TriggerSound(object parameter)
        {
            var player = Orchestor.GetAvailableMediaPlayer();
            View.GetDispatcher().Invoke(() =>
            {
                player.PlaySound(parameter);
            });
        }
    }
}
