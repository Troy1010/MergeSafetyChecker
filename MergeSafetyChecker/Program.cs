using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MergeSafetyChecker
{
    class Program
    {
        static void Main(string[] args)
        {
            MergeSafetyCheckerProgram prog = new MergeSafetyCheckerProgram();
            prog.Run();
            System.Console.ReadLine();
        }
    }

    class MergeSafetyCheckerProgram
    {
        public void Run()
        {
            //  Establish sRegexPattern
            string sRegexPattern = "";
            bool bDoOnce = false;
            foreach (var sModName in File.ReadAllLines(Properties.Settings.Default.SourceFile).ToList())
            {
                if (!bDoOnce)
                {
                    bDoOnce = true;
                }
                else
                {
                    sRegexPattern += "|";
                }
                sRegexPattern += sModName;
            }
            sRegexPattern = "IsModLoaded \"" + sRegexPattern;
            //  Search
            int iTotalMatchCount = 0;
            foreach (var oMod in (new DirectoryInfo(Properties.Settings.Default.DataFolder)).EnumerateFiles())
            {
                // Filter
                if (oMod.Extension != ".esp" && oMod.Extension != ".esm") continue;
                //
                var FileContents = File.ReadAllText(oMod.FullName);
                var oMatch = Regex.Match(FileContents,sRegexPattern);
                if (oMatch.Success)
                {
                    iTotalMatchCount++;
                    System.Console.WriteLine($"WARNING: {oMod.Name} contained removed mod {oMatch.Value}");
                }
            }
            System.Console.WriteLine($"Detected {iTotalMatchCount} matches.");
        }
    }

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
