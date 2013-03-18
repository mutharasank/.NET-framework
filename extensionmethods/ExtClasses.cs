using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ext
{
    public static class StrExt
    {
        public static int WordCount(this string str)
        {
            string[] words = str.Split();
            return words.Length;
        }

        public static string Initials(this string str)
        {
            string[] words = str.Split();
            string res = "";
            foreach (string s in words)
            {
                res += s[0];
            }
            return res;
        }

        public static string ExtractWord(this string str, int n)
        {
            string[] words = str.Split();
            if (n < 0 || n >= words.Length)
                throw new ArgumentException("Word nr. " + n + " does not exist");
            else
                return words[n];
        }
    }

}

