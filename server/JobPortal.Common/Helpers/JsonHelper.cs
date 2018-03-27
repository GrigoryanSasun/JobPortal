using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.Common.Helpers
{
    public static class JsonHelper
    {
        public static string ReadJsonFile(string path)
        {
            using (var streamReader = new StreamReader(path))
            {
                string json = streamReader.ReadToEnd();
                return json;
            }
        }
    }
}
