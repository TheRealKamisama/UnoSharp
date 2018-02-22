using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace UnoSharp
{
    public class EmbedResourceReader
    {
        public static string Read(string name)
        {
            var stream = GetStream(name);
            return stream == null ? null : new StreamReader(stream).ReadToEnd();
        }

        public static Stream GetStream(string name)
        {
            var currentAssembly = Assembly.GetExecutingAssembly();
            return currentAssembly.GetManifestResourceStream(name);
        }
    }
}
