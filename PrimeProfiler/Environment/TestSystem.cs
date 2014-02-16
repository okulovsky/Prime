﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PrimeProfiler
{
    abstract class TestSystem
    {
        public Computations Computations { get; set; }
        public int Length { get; set; }
        public int Count { get; set; }
        public int WaveCount { get; set; }

        protected abstract void Initialize();
        protected abstract void RunWaves();
        protected abstract void Finish();

        public double ElapsedMS;


        public void Run(int length, int widht, int waves, Computations comp)
        {
            Length = length;
            Count = widht;
            WaveCount = waves;
            Computations = comp;
            Run();
        }

        public void Run()
        {
            Console.Write("Init ");
            Initialize();
            var watch = new Stopwatch();
            Console.Write("Start "); 
            watch.Start();
            RunWaves();
            watch.Stop();
            Console.Write("Stop ");
            Thread.Sleep(100);
            try
            {
                Finish();
            }
            catch { }
            Console.Write("OK ");
            ElapsedMS= (double)watch.ElapsedMilliseconds;
            GC.Collect();
            GC.Collect();
            Console.WriteLine(GC.GetTotalMemory(true));

        }

        public string GetName()
        {
            return this.GetType().GetGenericTypeDefinition().Name;
        }

        public abstract bool IsSync { get; }

        public abstract Type GetDataType { get; }
    }
}