using System;
using System.IO;
using System.Media;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using PianoApp.Resources;
using PianoApp.ViewModels;
using PianoApp.Views.Interfaces;

namespace PianoApp.Views
{
    /// <summary>
    /// Interaktionslogik für KeyControl.xaml
    /// </summary>
    public partial class KeyControl : MetroContentControl, IKeyView
    {
        public KeyControl()
        {
            InitializeComponent();
            DataContext = new KeyViewModel(this);
        }

        public Dispatcher GetDispatcher()
        {
            return this.Dispatcher;
        }

        private void MetroContentControl_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            SoundBar.LoadSoundPaths();
        }
    }
}
