using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using PianoApp.Resources;
using PianoApp.Views;
using PianoApp.Views.Interfaces;

namespace PianoApp.ViewModels
{
    public class MainWindowViewModel
    {
        public IMainView View { get; set; }
        public KeyControl KeyControl { get; set; }
        public MonitorControl MonitorControl { get; set; }

        public readonly Button[] Keys = new Button[61];

        private ICommand _keyPressedCommand;
        public ICommand KeyPressedCommand
        {
            get
            {
                return _keyPressedCommand ?? (_keyPressedCommand = new RelayCommand(
                           p => true,
                           PressedKeyHandler));
            }
        }

        public MainWindowViewModel(IMainView view, KeyControl keyControl, MonitorControl monitorControl)
        {
            View = view;
            KeyControl = keyControl;
            MonitorControl = monitorControl;

            // modify..
            var monitorViewModel = (MonitorViewModel)MonitorControl.DataContext;
            monitorViewModel.SetKeyViewModel((KeyViewModel)KeyControl.DataContext);
            SetupKeys();
        }

        private void SetupKeys()
        {
            int count = 0;
            foreach (var button in FindVisualChildren<Button>(KeyControl.ControlContainer))
            {
                Keys[count++] = button;
            }
        }

        private IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);
                    if (child != null && child is T)
                    {
                        yield return (T)child;
                    }

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                    {
                        yield return childOfChild;
                    }
                }
            }
        }

        #region KeyHandler

        private void PressedKeyHandler(object parameter)
        {
            int index = -1;
            var keyArgs = (KeyEventArgs)parameter;

            #region Case Modified 
            if (keyArgs.Key == Key.D1 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 36;
            }
            else if (keyArgs.Key == Key.D2 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 37;
            }
            else if (keyArgs.Key == Key.D4 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 38;
            }
            else if (keyArgs.Key == Key.D5 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 39;
            }
            else if (keyArgs.Key == Key.D6 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 40;
            }
            else if (keyArgs.Key == Key.D8 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 41;
            }
            else if (keyArgs.Key == Key.D9 && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 42;
            }
            else if (keyArgs.Key == Key.Q && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 43;
            }
            else if (keyArgs.Key == Key.W && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 44;
            }
            else if (keyArgs.Key == Key.E && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 45;
            }
            else if (keyArgs.Key == Key.T && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 46;
            }
            else if (keyArgs.Key == Key.Z && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 47;
            }
            else if (keyArgs.Key == Key.I && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 48;
            }
            else if (keyArgs.Key == Key.O && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 49;
            }
            else if (keyArgs.Key == Key.P && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 50;
            }
            else if (keyArgs.Key == Key.S && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 51;
            }
            else if (keyArgs.Key == Key.D && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 52;
            }
            else if (keyArgs.Key == Key.G && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 53;
            }
            else if (keyArgs.Key == Key.H && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 54;
            }
            else if (keyArgs.Key == Key.J && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 55;
            }
            else if (keyArgs.Key == Key.L && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 56;
            }
            else if (keyArgs.Key == Key.Y && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 57;
            }
            else if (keyArgs.Key == Key.C && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 58;
            }
            else if (keyArgs.Key == Key.V && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 59;
            }
            else if (keyArgs.Key == Key.B && Keyboard.IsKeyDown(Key.LeftShift))
            {
                index = 60;
            }
            #endregion

            #region Case Normal
            if (!Keyboard.IsKeyDown(Key.LeftShift))
            {
                switch (keyArgs.Key)
                {
                    case Key.D1:
                        index = 0;
                        break;
                    case Key.D2:
                        index = 1;
                        break;
                    case Key.D3:
                        index = 2;
                        break;
                    case Key.D4:
                        index = 3;
                        break;
                    case Key.D5:
                        index = 4;
                        break;
                    case Key.D6:
                        index = 5;
                        break;
                    case Key.D7:
                        index = 6;
                        break;
                    case Key.D8:
                        index = 7;
                        break;
                    case Key.D9:
                        index = 8;
                        break;
                    case Key.D0:
                        index = 9;
                        break;
                    case Key.Q:
                        index = 10;
                        break;
                    case Key.W:
                        index = 11;
                        break;
                    case Key.E:
                        index = 12;
                        break;
                    case Key.R:
                        index = 13;
                        break;
                    case Key.T:
                        index = 14;
                        break;
                    case Key.Z:
                        index = 15;
                        break;
                    case Key.U:
                        index = 16;
                        break;
                    case Key.I:
                        index = 17;
                        break;
                    case Key.O:
                        index = 18;
                        break;
                    case Key.P:
                        index = 19;
                        break;
                    case Key.A:
                        index = 20;
                        break;
                    case Key.S:
                        index = 21;
                        break;
                    case Key.D:
                        index = 22;
                        break;
                    case Key.F:
                        index = 23;
                        break;
                    case Key.G:
                        index = 24;
                        break;
                    case Key.H:
                        index = 25;
                        break;
                    case Key.J:
                        index = 26;
                        break;
                    case Key.K:
                        index = 27;
                        break;
                    case Key.L:
                        index = 28;
                        break;
                    case Key.Y:
                        index = 29;
                        break;
                    case Key.X:
                        index = 30;
                        break;
                    case Key.C:
                        index = 31;
                        break;
                    case Key.V:
                        index = 32;
                        break;
                    case Key.B:
                        index = 33;
                        break;
                    case Key.N:
                        index = 34;
                        break;
                    case Key.M:
                        index = 35;
                        break;
                }
            }


            #endregion

            if(index < 0)
                return;

            View.ButtonClickAnimation(index);
            var path = SoundBar.GetSoundPathByIdent(index);

            var monitorViewModel = (MonitorViewModel) MonitorControl.DataContext;
            monitorViewModel.LabelContent = monitorViewModel.CutPath(path);
        }
        #endregion
    }
}
