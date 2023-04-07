using CB.Application.Utilities.Defaults;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CB.Application.Utilities.Helpers
{
    public static class CommonHelper
    {
        public static string CharacterRegularity(string text)
        {
            text = text.Replace(" ", "-");
            Regex.Replace(text, RegexDefaults.CharacterRegularity, "", RegexOptions.Compiled);
            return text;
        }
    }
}
