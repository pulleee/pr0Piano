using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Media;
using System.Windows.Threading;
using MahApps.Metro.Controls;
using PianoApp.ViewModels;
using PianoApp.Views.Interfaces;

namespace PianoApp.Views
{
    /// <summary>
    /// Interaktionslogik für MonitorControl.xaml
    /// </summary>
    public partial class MonitorControl : MetroContentControl, IMonitorView
    {
        public MonitorControl()
        {
            InitializeComponent();
            DataContext = new MonitorViewModel(this);
        }

        public KeyValuePair<string, SolidColorBrush>[] InitializeColorBrushes()
        {
            KeyValuePair <string, SolidColorBrush>[] styles =
            {
                new KeyValuePair<string, SolidColorBrush>("steel", (SolidColorBrush) Resources["GrauColor"]),
                new KeyValuePair<string, SolidColorBrush>("cobalt", (SolidColorBrush) Resources["EpicBlueColor"]),
                new KeyValuePair<string, SolidColorBrush>("olive", (SolidColorBrush)Resources["PeacfulGreenColor"]),
                new KeyValuePair<string, SolidColorBrush>("green", (SolidColorBrush)Resources["ComfortableGreenColor"]),
                new KeyValuePair<string, SolidColorBrush>("pink", (SolidColorBrush)Resources["OldPinkColor"]),
                new KeyValuePair<string, SolidColorBrush>("orange", (SolidColorBrush)Resources["ProvenOrangeColor"]),
            };
            return styles;
        }

        public Dispatcher GetDispatcher()
        {
            return this.Dispatcher;
        }
    }
}
