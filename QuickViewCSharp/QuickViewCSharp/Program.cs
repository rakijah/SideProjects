using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace QuickViewCSharp
{
    static class Program
    {
        [STAThread]
        static int Main(string[] args)
        {
            if (args.Length < 1)
                return 1;

            if (!File.Exists(args[0]) || !isImage(args[0]))
                return 1;
            
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new ImageWindow(args));
            return 0;
        }

        static Boolean isImage(string input)
        {
            string[] types = { ".jpg", ".jped", ".gif", ".png", ".bmp", ".tif" };

            foreach (string i in types)
                if (input.ToLower().EndsWith(i, true, null))
                    return true;

            return false;
        }
    }
}
