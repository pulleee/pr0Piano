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

        public Dispatcher GetDispatcher()
        {
            return this.Dispatcher;
        }
    }
}
