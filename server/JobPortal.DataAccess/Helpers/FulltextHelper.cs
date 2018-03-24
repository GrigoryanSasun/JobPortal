using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JobPortal.DataAccess.Helpers
{
    static class FulltextHelper
    {
        public static string PreprocessForFulltextSearch(string keyword)
        {
            var words = keyword.Split(null).Select((word) => "\"" + word + "*\"");
            return string.Join(" AND ", words);
        }
    }
}
