using System;
using System.Collections.Generic;
using System.Text;

namespace ChatConverter.Utilities
{
    public static class Utilities
    {
        public static int GetNthIndexFromEnd(this string s, char t, int n)
        {
            int count = 0;
            for (int i = s.Length-1; i > 0; i--)
            {
                if (s[i] == t)
                {
                    count++;
                    if (count == n)
                    {
                        return i;
                    }
                }
            }
            return -1;
        }
    }
}
