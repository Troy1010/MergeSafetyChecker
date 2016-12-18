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
}
