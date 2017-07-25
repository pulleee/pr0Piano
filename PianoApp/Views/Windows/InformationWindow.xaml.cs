using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using MahApps.Metro.Controls;
using Path = System.IO.Path;

namespace PianoApp.Views.Windows
{
    /// <summary>
    /// Interaction logic for InformationWindow.xaml
    /// </summary>
    public partial class InformationWindow
    {
        public InformationWindow()
        {
            InitializeComponent();
        }

        public void NavigateWebControlToInfoPage()
        {
            webBrowser.Navigate(@"D:\Users\Daniel\SVN\PianoAppGit\pr0Piano.git\trunk\PianoApp\Views\Windows\Credits.html");
        }
    }
}
