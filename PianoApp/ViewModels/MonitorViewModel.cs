using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using PianoApp.Properties;
using PianoApp.Views.Interfaces;
using PianoApp.Views.Windows;

namespace PianoApp.ViewModels
{
    public class MonitorViewModel :  INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        //todo: implements menu button commands

        private ICommand _buttonConnectionPressed;
        public ICommand ButtonConnectionPressed
        {
            get
            {
                return _buttonConnectionPressed ?? (_buttonConnectionPressed = new RelayCommand(
                           p => true,
                           ShowInformationWindow));
            }
        }

        //todo: implement
        private Color _color;
        /// <summary>
        /// Get/Set ColorStyle of the Control
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                OnPropertyChanged();
            }
        }

        private string _labelContent;
        /// <summary>
        /// Get/Set Content of the Middle Screen
        /// </summary>
        public string LabelContent
        {
            get { return _labelContent; }
            set
            {
                //_labelContent = CutPath(value);
                _labelContent = value;
                OnPropertyChanged();
            }
        }

        // default volume = 50
        private double _soundVolume = 50;
        /// <summary>
        /// Get/Set SoundVolume for a Value from 0 to 100
        /// </summary>
        public double SoundVolume
        {
            get { return _soundVolume; }
            set
            {
                _soundVolume = value;
                OnPropertyChanged();
                Task.Factory.StartNew(() => _keyViewModel.Orchestor.SetSoundVolume(value));
            }
        }

        private InformationWindow _infoWindow;
        private KeyViewModel _keyViewModel;

        public MonitorViewModel()
        {
            LabelContent = ">_";
        }

        private void ShowInformationWindow(object obj)
        {
            if(_infoWindow == null)
                _infoWindow = new InformationWindow();

            _infoWindow.NavigateWebControlToInfoPage();
            _infoWindow.Show();
        }

        /// <summary>
        /// Cuts Path in the most ineffective way... KOTZ
        /// todo: program dynamically
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string CutPath(string path)
        {
            var description = path.Substring(path.Length - 7, 3);
            var node = "";
            var number = "";
            if (description.Contains("_"))
            {
                node = description.Substring(1, 1);
                number = description.Substring(2, 1);
            }
            else
            {
                node = description.Substring(0, 1);
                number = description.Substring(1, 2);
            }
            return node + " - " + number;
        }


        /// <summary>
        /// Sets _keyViewModel variable
        /// </summary>
        /// <param name="keyViewModel"></param>
        public void SetKeyViewModel(KeyViewModel keyViewModel)
        {
            _keyViewModel = keyViewModel;
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
