﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrimeProfiler.Runner
{
    class Program
    {
        static void Run(int length, int widht, int count, int systemType, int dataType, int compType)
        {
            var process = new Process();
            process.StartInfo.FileName = "PrimeProfiler";
            process.StartInfo.Arguments = string.Format("{0} {1} {2} {3} {4} {5}", length, widht, count, systemType, dataType, compType);
            process.StartInfo.UseShellExecute = false;
            process.Start();
            process.WaitForExit();
        }

        static void MakeComp(int count, int width)
        {
            for (int stype = 0; stype < 6; stype++)
                for (int dtype = 0; dtype < 2; dtype++)
                {
                    var cnt = count;
                    if (stype < 2) cnt *= 10;
                    Run(100, width, cnt, stype, dtype, 0);
                }
        }

        static void Main(string[] args)
        {
            File.Delete("..\\..\\..\\result.txt");
            for (int i = 0; i < 10; i++)
            {
                MakeComp(100000, 1);
                MakeComp(2000, 8);
            }
        }
    }
}