using System.Collections.Generic;
using System.Windows.Media;

namespace PianoApp.Views.Interfaces
{
    public interface IMonitorView : IBaseView
    {
        KeyValuePair<string, SolidColorBrush>[] InitializeColorBrushes();
    }
}
