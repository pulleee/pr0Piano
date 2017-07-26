using System.Collections.Generic;
using System.IO;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;

namespace PianoApp.Util
{
    public static class NodeSheetImporter
    {
        public static List<string> ImportNodeSheet()
        {
            List<string> inputfields = new List<string>();
            var filechooser = new OpenFileDialog();
            filechooser.Filter = "Text|*.txt|All|*.*";
            filechooser.ShowDialog();

            // Check if the file exists before trying to open it 
            if (File.Exists(filechooser.FileName))
            {
                using (StreamReader fileReader = new StreamReader(filechooser.FileName))
                {
                    while (fileReader.ReadLine() != null)
                    {
                        var readLine = fileReader.ReadLine();
                        if (readLine != null)
                        {
                            inputfields.Add(readLine.Replace("\t", ""));
                            inputfields.Add("\n");
                        }
                    }
                }
                
            }
            return inputfields;
        }
    }
}
