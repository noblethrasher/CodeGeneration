using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace CodeGeneration
{
    static class StringUtils
    {
        public static void AppendLine(this StringBuilder sb, string str, int n)
        {
            var s = "\t".Repeat (n) + str;

            sb.AppendLine (s);
        }

        public static string Repeat(this string s, int n)
        {
            var sb = new StringBuilder ();

            for (var i = 0; i < n; i++)
                sb.Append (s);
            
            return sb.ToString ();
        }

        public static string Repeat(this char c, int n)
        {
            var array = new char[n];
            
            for (var i = 0; i < n; i++)
                array[i] = c;
            
            return new String (array);
        }

        public static string Join(this IEnumerable<string> xs, string sep)
        {
            return string.Join (sep, xs);
        }
    }
}
