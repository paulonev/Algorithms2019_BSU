using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using PackingHeuristics;

namespace PackingHeuristics.Test
{
    public class Tests
    {
        readonly Random random = new Random();

        [SetUp]
        public void Setup()
        {
        }
        
        [TestCase(arg: 1000)]
        [TestCase(arg: 2000)]
        [TestCase(arg: 3000)]
        [TestCase(arg: 4000)]
        [TestCase(arg: 5000)]
        public void K_Thousand_Packs_Test(int N)
        {
            float[] weights = new float[N];
            for (int i = 0; i < N; i++)
            {
                weights[i] = random.Next(0, 1000);
            }
            // weights {w1, w2, w3, ... , wn}
            // Reweight for each wi: wi / max(weights)
            Packing.ReWeight(ref weights);

            List<float> bfContainers = new List<float>();
            int bfCount = Packing.BestFit(weights, ref bfContainers);
            
            List<float> ffContainers = new List<float>();
            int ffCount = Packing.FirstFit(weights, ref ffContainers);
            
            List<float> nfContainers = new List<float>();
            int nfCount = Packing.NextFit(weights, ref nfContainers);

            List<float> ffoContainers = new List<float>();
            Array.Sort(weights, new ReverseComparer());
            int ffoCount = Packing.FirstFit(weights, ref ffoContainers);

            Console.WriteLine($"-- {N} items bin packing --");
            Console.WriteLine($"Next Fit = {nfCount}");
            Console.WriteLine($"First Fit = {ffCount}");
            Console.WriteLine($"Best Fit = {bfCount}");
            Console.WriteLine($"Order First Fit = {ffoCount}");
        }

        [TestCase(arg: 1000)]
        public void NextFit_Tests(int N)
        {
            float[] weights = new float[N];
            for (int i = 0; i < N; i++)
            {
                weights[i] = random.Next(0, 1000);
            }
            Packing.ReWeight(ref weights);
            
            List<float> containers = new List<float>();
            DateTime start = DateTime.Now;
            int count = Packing.NextFit(weights, ref containers);
            int time = (DateTime.Now - start).Milliseconds;

            Console.WriteLine($"-- {N} items next-fit packing --");
            Console.WriteLine($"Count: {count}, Time {time}");
            int j = 0;
            foreach (var item in containers)
            {
                Console.WriteLine($"Container {++j}:  {item}");
            }
        }
        
        [TestCase(arg: 1000)]
        public void FirstFit_Tests(int N)
        {
            float[] weights = new float[N];
            for (int i = 0; i < N; i++)
            {
                weights[i] = random.Next(0, 1000);
            }
            Packing.ReWeight(ref weights);
            
            List<float> containers = new List<float>();
            DateTime start = DateTime.Now;
            int count = Packing.FirstFit(weights, ref containers);
            int time = (DateTime.Now - start).Milliseconds;

            Console.WriteLine($"-- {N} items first-fit packing --");
            Console.WriteLine($"Count: {count}, Time: {time}");
            int j = 0;
            foreach (var item in containers)
            {
                Console.WriteLine($"Container {++j}:  {item}");
            }
        }
        
        [TestCase(arg: 1000)]
        public void BestFit_Tests(int N)
        {
            float[] weights = new float[N];
            for (int i = 0; i < N; i++)
            {
                weights[i] = random.Next(0, 1000);
            }
            Packing.ReWeight(ref weights);
            
            List<float> containers = new List<float>();
            DateTime start = DateTime.Now;
            int count = Packing.BestFit(weights, ref containers);
            int time = (DateTime.Now - start).Milliseconds;
            
            Console.WriteLine($"-- {N} items best-fit packing --");
            Console.WriteLine($"Count: {count}, Time {time}");
            int j = 0;
            foreach (var item in containers)
            {
                Console.WriteLine($"Container {++j}:  {item}");
            }
        }
    }
}