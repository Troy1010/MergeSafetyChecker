using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MergeSafetyChecker
{
    public class MergeSafetyCheckerProgram
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
                var oMatch = Regex.Match(FileContents, sRegexPattern);
                if (oMatch.Success)
                {
                    iTotalMatchCount++;
                    System.Console.WriteLine($"WARNING: {oMod.Name} contained removed mod {oMatch.Value}");
                }
            }
            System.Console.WriteLine($"Detected {iTotalMatchCount} matches.");
        }
    }
}
