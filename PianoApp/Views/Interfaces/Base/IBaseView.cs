using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace PianoApp.Views.Interfaces
{
    /// <summary>
    /// A base interface for all views
    /// </summary>
    public interface IBaseView
    {
        /// <summary>
        /// Gets Dispatcher of control
        /// </summary>
        /// <returns></returns>
        Dispatcher GetDispatcher();
    }
}
