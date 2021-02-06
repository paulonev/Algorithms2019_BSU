using System.Collections;
using NUnit.Framework;
using src.instances;
using src.funcs;
using src.parser;
using System;

namespace test
{           
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateHashtable()
        {
            NLBHT knuthTable = new NLBHT();
            knuthTable.SetHashFunction(new MultFunc((Math.Sqrt(5)-1)/2));
            knuthTable.Put("Paul", new MultFunc(1/3));
            knuthTable.Put("Jacob", 15);
            knuthTable.Put("Dog", "Candy");

            // Assert.AreEqual(knuthTable.Count, 3);
            Assert.AreEqual(expected: 7, actual: knuthTable.TabSize);
        }

        [Test, MaxTime(5000)]
        public void PrintHashTable()
        {
            NLBHT knuthTable = new NLBHT();
            knuthTable.SetHashFunction(new MultFunc((Math.Sqrt(5)-1)/2));
            knuthTable.Put("Paul", new MultFunc(1/3));
            knuthTable.Put("Jacob", 15);
            knuthTable.Put("Dog", "Candy");

            Console.Write(knuthTable.ToString());
        }

        [Test, MaxTime(5000)]
        public void CheckResizeTable()
        {
            NLBHT knuthTable = new NLBHT();
            knuthTable.SetHashFunction(new MultFunc((Math.Sqrt(5)-1)/2));
            knuthTable.Put("Paul", new MultFunc(1/3));
            knuthTable.Put("Jacob", 15);
            knuthTable.Put("Dog", "Candy");
            knuthTable.Put("Gregory", 10);

            Assert.AreEqual(knuthTable.TabSize, 6);
            Console.WriteLine(knuthTable.ToString());
        }

        [Test, MaxTime(2000)]
        public void TableContainsItem()
        {
            NLBHT knuthTable = new NLBHT();
            knuthTable.SetHashFunction(new MultFunc((Math.Sqrt(5)-1)/2));
            knuthTable.Put("Jacob", 15);
            knuthTable.Put("Dog", "Candy");

            Assert.AreEqual(knuthTable.Contains("Jacob"), true);
            // Assert.AreEqual(knuthTable.Contains("jacobi"), false);
        }

        [Test, MaxTime(5000)]
        public void CanRemoveItem()
        {
            NLBHT knuthTable = new NLBHT();
            knuthTable.SetHashFunction(new ModFunc());
            knuthTable.Put("Paul", new MultFunc(1/3));
            knuthTable.Put("Jacob", 15);
            knuthTable.Put("Dog", "Candy");
            knuthTable.Put("Gregory", 10);
            Console.WriteLine(knuthTable.ToString());
            
            knuthTable.Remove("Dog");
            Console.WriteLine("After removal");
            Console.WriteLine(knuthTable.ToString());
            // Assert.AreEqual(knuthTable.Count, 3);
        }

        [TestCase(100), MaxTime(5000)]
        public void AddItems(int size)
        {
            NLBHT table = new NLBHT(new MultFunc((Math.Sqrt(5)-1)/2));
            string fileLocation = "../../../wikiwords.txt";
            var tuples = WordParser.ParseFromFile(fileLocation, numLines: size);
            foreach (var tuple in tuples)
            {
                table.Put(tuple.Item1, tuple.Item2);
            }
            // Assert.AreEqual(table.Count, 100);
            Console.WriteLine(table.TabSize);
            Console.WriteLine("Table load factor = {0}", table.LoadFactor);
        }

        [Test]
        public void HashComputation()
        {
            HashFunc f1 = new MultFunc((Math.Sqrt(5)-1)/2);
            int[] ar = {1,2,3,4};
            int[] ar1 = {1,2,3,4};
            
            int hashCode = (int)f1.GetHash(ar, 7); 
            int hashCode1 = (int)f1.GetHash(ar1, 7);

            // Assert.AreEqual(hashCode, hashCode1);  
            Console.WriteLine(Array.Equals(ar,ar));
            // Console.Write(hashCode);
        }
        
        [TearDown]
        public void Teardown()
        {

        }
    }
}