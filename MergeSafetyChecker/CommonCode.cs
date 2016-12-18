using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MergeSafetyChecker
{
    public static class CommonCode
    {
        public static object GetRegexMatchedObject(this string MatchedString, Dictionary<object, string[]> d2dMatchables)
        {
            foreach (KeyValuePair<object, string[]> vPair in d2dMatchables)
            {
                foreach (string sString in vPair.Value)
                {
                    if (sString == MatchedString)
                    {
                        return vPair.Key;
                    }
                }
            }
            return null;
        }
        public static string GetRegexPatternFromMatchables(this Dictionary<object, string[]> d2dMatchables)
        {
            bool bDoOnce = false;
            string sPattern = "";
            foreach (KeyValuePair<object, string[]> vPair in d2dMatchables)
            {
                foreach (string sString in vPair.Value)
                {
                    if (!bDoOnce)
                    {
                        bDoOnce = true;
                        sPattern += "|";
                    }
                    sPattern += sString;
                }
            }
            return "\b(" + sPattern + ")\b";
        }
    }
}
