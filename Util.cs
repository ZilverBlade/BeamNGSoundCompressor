using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeamNGSoundCompressor
{
    public static class Util
    {
        public static string ReplaceFileExtention(string file, string newExtension)
        {
            return file.Substring(0, file.LastIndexOf('.') + 1) + newExtension;
        }
    }
}
