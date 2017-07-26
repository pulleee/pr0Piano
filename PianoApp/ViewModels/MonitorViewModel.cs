using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using PianoApp.Properties;
using PianoApp.Util;
using PianoApp.Views.Interfaces;
using PianoApp.Views.Windows;
using Color = System.Drawing.Color;

namespace PianoApp.ViewModels
{
    public class MonitorViewModel : INotifyPropertyChanged
    {
        #region Events
        public event PropertyChangedEventHandler PropertyChanged;

        public delegate void StringEventArgs(string e);
        public event StringEventArgs OnStyleChanged;
        #endregion

        #region Commands
        private ICommand _selectionChangedCommand;
        public ICommand SelectionChangedCommand
        {
            get
            {
                return _selectionChangedCommand ?? (_selectionChangedCommand = new RelayCommand(
                           p => true,
                           OnStyleselectionChanged));
            }
        }

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

        private ICommand _buttonStopPressed;
        public ICommand ButtonStopPressed
        {
            get
            {
                return _buttonStopPressed ?? (_buttonStopPressed = new RelayCommand(
                           p => true,
                           StopStopwatch));
            }
        }

        private ICommand _buttonPausePressed;
        public ICommand ButtonPausePressed
        {
            get
            {
                return _buttonPausePressed ?? (_buttonPausePressed = new RelayCommand(
                           p => true,
                           PauseStopwatch));
            }
        }

        private ICommand _buttonPlayPressed;
        public ICommand ButtonPlayPressed
        {
            get
            {
                return _buttonPlayPressed ?? (_buttonPlayPressed = new RelayCommand(
                           p => true,
                           StartStopWatch));
            }
        }

        private ICommand _buttonNodeSheetPressed;
        public ICommand ButtonNodeSheetPressed
        {
            get
            {
                return _buttonNodeSheetPressed ?? (_buttonNodeSheetPressed = new RelayCommand(
                           p => true,
                           ImportNodeSheet));
            }
        }
        #endregion

        #region Properties
        public IMonitorView View { get; set; }
        public KeyValuePair<string, SolidColorBrush>[] Styles { get; set; }

        private SolidColorBrush _currentStyle;
        /// <summary>
        /// Get/Set Current Color Style of the applicaiton
        /// </summary>
        public SolidColorBrush CurrentStyle
        {
            get { return _currentStyle; }
            set { _currentStyle = value; OnPropertyChanged(); }
        }

        private Color _color;
        /// <summary>
        /// Get/Set ColorStyle of the Control
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; OnPropertyChanged(); }
        }

        private string _labelContent = ">_";
        /// <summary>
        /// Get/Set Content of the Middle Screen
        /// </summary>
        public string LabelContent
        {
            get { return _labelContent; }
            set { _labelContent = value; OnPropertyChanged(); }
        }

        private string _timerContent = "00:00:00";
        /// <summary>
        /// Get/Set Content of the Middle Timer
        /// </summary>
        public string TimerContent
        {
            get { return _timerContent; }
            set {_timerContent = value; OnPropertyChanged(); }
        }

        // default volume = 50
        private double _soundVolume = 35;
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
                _keyViewModel.Orchestor.SetSoundVolume(value);
            }
        }

        private string _nodeSheetValue;
        /// <summary>
        /// Get/Set ColorStyle of the Control
        /// </summary>
        public string NodeSheetValue
        {
            get { return _nodeSheetValue; }
            set { _nodeSheetValue = value; OnPropertyChanged(); }
        }

        #endregion

        #region Fields
        // FYI: https://stackoverflow.com/questions/6615913/run-event-handlers-in-another-thread-without-threads-blocking
        private DispatcherTimer _timer;
        private InformationWindow _infoWindow;
        private KeyViewModel _keyViewModel;

        private Thread _thread;

        private int _min = 0;
        private int _sec = 0;
        private int _ms = 0;
        #endregion

        #region Public Constructor
        public MonitorViewModel(IMonitorView view)
        {
            View = view;
            Init();
        }
        #endregion

        #region Private Methods
        private void Init()
        {
            Styles = View.InitializeColorBrushes();

            NodeSheetValue = "import node sheet";
            _timer = new DispatcherTimer();
            _timer.Tick += dispatcherTimer_Tick;
            _timer.Interval = new TimeSpan(0, 0, 0, 0, 1);
        }

        private void StartStopWatch(object obj)
        {
            if (!_timer.IsEnabled)
                Dispatcher.CurrentDispatcher.Invoke(() =>
            {
                _timer.Start();
            });
        }

        private void StopStopwatch(object obj)
        {
            if (_timer.IsEnabled)
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    _timer.Stop();
                    ResetTimerVariables();
                });
            else
                ResetTimerVariables();
        }

        private void PauseStopwatch(object obj)
        {
            if (_timer.IsEnabled)
                Dispatcher.CurrentDispatcher.Invoke(() =>
                {
                    _timer.Stop();
                });
        }

        private void OnStyleselectionChanged(object selectedColor)
        {
            var keyValuePair = (KeyValuePair<string, SolidColorBrush>)selectedColor;
            CurrentStyle = keyValuePair.Value;

            OnStyleChanged?.Invoke(keyValuePair.Key);
            _keyViewModel.View.ReFocus();
        }

        private void ShowInformationWindow(object obj)
        {
            // better solution?
            _infoWindow = new InformationWindow();
            _infoWindow.NavigateWebControlToInfoPage();
            _infoWindow.Show();
        }

        private void ImportNodeSheet(object obj)
        {
            var values = NodeSheetImporter.ImportNodeSheet();
            if (values.Count > 0)
                NodeSheetValue = string.Empty;
            values.ForEach(new Action<string>((str) =>
            {
                NodeSheetValue = NodeSheetValue + str;
            }));
        }

        private void ResetTimerVariables()
        {
            TimerContent = "00:00:00";
            _ms = 0;
            _sec = 0;
            _min = 0;
        }
        #endregion

        #region Public Methods
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
        #endregion

        #region EventHandler
        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            #region Timer Magic
            _ms++;
            if (_ms >= 100)
            {
                _ms = 0;
                _sec++;
            }
            if (_sec == 60)
            {
                _sec = 0;
                _min++;
            }

            StringBuilder timeStr = new StringBuilder();

            if (_min < 10)
            {
                timeStr.Append("0");
                timeStr.Append(_min);
            }
            else
            {
                timeStr.Append(_min);
            }
            timeStr.Append(":");

            if (_sec < 10)
            {
                timeStr.Append("0");
                timeStr.Append(_sec);
            }
            else
            {
                timeStr.Append(_sec);
            }
            timeStr.Append(":");
            if (_ms < 10)
            {
                timeStr.Append("0");
                timeStr.Append(_ms);
            }
            else
            {
                timeStr.Append(_ms);
            }
            #endregion

            TimerContent = timeStr.ToString();

            // Let timer only play for 60 minutes
            if (_min == 60)
                _timer.Stop();
        }
        #endregion
    }
}
