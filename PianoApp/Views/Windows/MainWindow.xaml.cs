﻿using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using MahApps.Metro;
using PianoApp.ViewModels;
using PianoApp.Views.Interfaces;

namespace PianoApp.Views
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : IMainView
    {
        private readonly MainWindowViewModel _viewmodel;
        private readonly KeyViewModel _keyViewModel;
        private readonly MonitorViewModel _monitorViewModel;

        public MainWindow()
        {
            InitializeComponent();
            monitorControl.Focus();
            _viewmodel = new MainWindowViewModel(this, keyControl, monitorControl);
            _keyViewModel = (KeyViewModel) keyControl.DataContext;
            _monitorViewModel = (MonitorViewModel) monitorControl.DataContext;

            _monitorViewModel.OnStyleChanged += _monitorViewModel_OnStyleChanged;
        }

        private void SetupThemeManager()
        {
            ThemeManager.AddAccent("Right Gray",
                new Uri("pack://application:,,,/MahApps.Metro;component/Styles/Accents/basedark.xaml"));
        }

        /// <summary>
        /// KeyDown event to fire KeyPressedCommand in ViewModel
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MetroWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.LeftShift)
                return;
            _viewmodel.KeyPressedCommand.Execute(e);
        }

        public void ShowAnimationFadeOut(Button button)
        {
            this.Dispatcher.Invoke(() =>
            {
                button.Visibility = Visibility.Visible;
                var a = new DoubleAnimation
                {
                    From = 1.0,
                    To = 0.4,
                    FillBehavior = FillBehavior.Stop,
                    BeginTime = TimeSpan.FromSeconds(0),
                    Duration = new Duration(TimeSpan.FromSeconds(0.3))
                };
                var storyboard = new Storyboard();

                storyboard.Children.Add(a);
                Storyboard.SetTarget(a, button);
                Storyboard.SetTargetProperty(a, new PropertyPath(OpacityProperty));
                storyboard.Completed += delegate { button.Visibility = Visibility.Visible; };
                storyboard.Begin();
            });
        }

        public void ButtonClickAnimation(int index)
        {
            Task.Factory.StartNew(() => _keyViewModel.KeyPressedCommand.Execute((object)index));
            ShowAnimationFadeOut(_viewmodel.Keys[index]);
        }

        public Dispatcher GetDispatcher()
        {
            return this.Dispatcher;
        }

        private void _monitorViewModel_OnStyleChanged(string e)
        {
            // set the Red accent and dark theme only to the current window
            var accent = ThemeManager.GetAccent(e);
            var theme = ThemeManager.GetAppTheme("BaseDark");


            ThemeManager.ChangeAppStyle(this, accent, theme);
        }
    }
}
