using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CSharpBlog.Utilities
{
    public class PostText
    {
        public static string Trim(string text)
        {
            return text.Length > 200 ? text.Substring(0, 197) + "..." : text;
        }
    }
}