using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StreamCommit
{
    public class Util
    {
        public static string GetTopDirectory(string filePath)
        {
            if (filePath.EndsWith(@"\"))
                filePath = filePath.Substring(0, filePath.Length - 1);

            int lastBackslashIndex = filePath.LastIndexOf('\\');
            return lastBackslashIndex == -1 ? filePath : filePath.Substring(lastBackslashIndex + 1);
        }
    }
}
