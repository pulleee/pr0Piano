using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PianoApp.Resources
{
    public static class SoundBar
    {
        private static string[] _soundPaths = new string[61];
        public static string GetSoundPathByIdent(int index)
        {
            return _soundPaths[index];
        }

        /// <summary>
        /// Loads all Sound paths in resources location
        /// </summary>
        public static void LoadSoundPaths()
        {
            var path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..\\..\\Resources\\Sounds"));
            _soundPaths = Directory.GetFiles(path);
        }
    }
}
