using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        List<string> ModsRemoved;
        public void Run()
        {
            int iMatchCount = 0;
            this.ModsRemoved = File.ReadAllLines(Properties.Settings.Default.SourceFile).ToList();
            DirectoryInfo DataFolder = new DirectoryInfo(Properties.Settings.Default.DataFolder);
            foreach (var Mod in DataFolder.EnumerateFiles())
            {
                if (Mod.Extension != ".esp" && Mod.Extension != ".esm") continue;
                var FileContents = File.ReadAllText(Mod.FullName);
                foreach (var ModRemoved in ModsRemoved)
                {
                    var MatchableString = "IsModLoaded "+ModRemoved;
                    if (FileContents.Contains(MatchableString))
                    {
                        iMatchCount++;
                        System.Console.WriteLine($"WARNING: {Mod.Name} contained removed mod {ModRemoved}");
                    }
                }
            }
            System.Console.WriteLine($"Detected {iMatchCount} errors.");
        }
    }
}
