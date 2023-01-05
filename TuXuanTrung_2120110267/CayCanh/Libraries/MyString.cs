using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace CayCanh.Libraries
{
    public static class MyString
    {
        public static string str_slug(String s)
        {
            String[][] symbols ={
                new String[] {"[àáảãạăắằẵẳặâầấẫẩậ]", "a"},
                new String[] {"[đ]", "d"},
                new String[] {"[êéèẽẻẹếềểễệ]", "e"},
                new String[] {"[ìíĩỉị]", "i"},
                new String[] {"[óòõỏọôồốỗổộơờớỡởợ]", "o"},
                new String[] {"[úùũụủưừứữửự]", "u"},
                new String[] {"[ỳýỹỷỵ]", "y"},
                new String[] {"[\\s'\";,]", "-"}
            };
            s = s.ToLower();
            foreach (var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
        public static string str_limit(this string str, int? length)
        {
            int lengt = (length ?? 20);
            if (str.Length > lengt)
            {
                str = str.Substring(0, lengt) + "...";
            }
            return str;
        }
    }
}