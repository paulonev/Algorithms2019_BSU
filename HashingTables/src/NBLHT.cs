using System;
using System.Collections;
using System.Collections.Generic;
using src.funcs;

namespace NLBHashtable
{
    struct DataBlock
    {
        string  _key;
        object  _value;
        uint    _libHashCode; // hash code produced by my_hash_library
        
        public string Key { get => _key;}
        public object Value { get => _value; }
        public uint CurHashCode { get => _libHashCode; }
        public DataBlock(object key, object value)
        {
            _key = key.ToString();
            _value = value;
            _libHashCode = 0;
        }

        public DataBlock(object key, object value, uint hashCode)
        {
            _key = key.ToString();
            _value = value;
            _libHashCode = hashCode;
        }
    }
    /*
        * Store (key,value) pairs associatively in array
        ** for each (key,value) pair to be added produce
        *** a hashIndex that will indicate elem's array pos.
        *** key - a string converted value
        *** value - data of any type: reference or value-type
        ** ICollection, IEnumerable, ICloneable
        * having a hidden implementation of CRUD operations
    */
    
    public class NLBHT : ICollection
    {
        private const int _DEFAULT_CAPACITY = 7;

        // =====PRIV========
        private DataBlock[] _blocks;
        private HashFunc hashFunction;
        private int _count;
        // =====PRIV========
        
        // =====PROPS========
        public float LoadFactor { 
            get => _count / _blocks.Length;
        }

        // tab capacity
        public int Size { 
            get => _blocks.Length; 
        }

        // current amount in tab
        public int Count { 
            get => _count;
            set => _count = value; 
        }
        // =====PROPS========

        public NLBHT()
        {
            _blocks = new DataBlock[_DEFAULT_CAPACITY];
            hashFunction = null;
        }
        public NLBHT(int size)
        {
            if (size < 0) throw new ArgumentException("[EXC01] Please, check size parameter in initialization\n");

            _blocks = new DataBlock[size];
            hashFunction = null;
        }
        // public NLBHT(int size, HashFunc fun)
        // {
        //     if (size < 0) throw new ArgumentException("[EXC01] Please, check size parameter in initialization\n");

        //     _blocks = new DataBlock[size];
        //     this.hashFunction = fun;
        // }
        
        public void SetHashFunction(HashFunc fun, bool makeDefault=false)
        {
            if (this.hashFunction != null)
                Console.WriteLine("[INF01] There is already a hash function for this hash table. We can't perform different hashing for the same hash table");
            else
            {
                if(makeDefault)
                    hashFunction = new ModFunc();
                else
                {
                    hashFunction = fun;
                }
            }
        }

        /// <summary>
        /// Put key-value pair in table
        /// Uses build-in or user-defined hash function
        /// that spread pairs across the whole array
        /// Can innerly resize table(if loadFactor > 0.75)
        /// </summary>
        /// <param name="key">unique elem in pair</param>
        /// <param name="value">data of any type</param>
        public void Put(object key, object value)
        {
            // check for presence of that key

            // LoadFactor is an average amount of items per DataBlock(bucket)
            if (LoadFactor > 0.75f)
            {
                _blocks = resizeBlocks(2*Size);
            }
                       
            uint hashIdx = hashFunction.GetHash(key, Size);
            DataBlock newElem = new DataBlock(key, value, hashIdx);
            //put datablock into array
            _blocks[hashIdx] = newElem;
            _count++;
            //GetHash(key) & 0x7FFFFFFF
        }

        private DataBlock[] resizeBlocks(int newSize)
        {
            // 1) allocate space for new array[2*N]
            int N = _blocks.Length;
            DataBlock[] newBlocks = new DataBlock[newSize];
            // 2) rehash all values in smaller array
            // 3) add instances to bigger array
            for (int i = 0; i < N; i++)
            {
                DataBlock block = _blocks[i];
                uint rehashIdx = rehash(block, newSize);
                if (rehashIdx != block.CurHashCode)
                {
                    newBlocks[rehashIdx] = block; 
                }
                else throw new Exception("[EXC02] Rehashing produced the same hashcode");
                
            }
            return newBlocks;
        }

        /// <summary>
        /// Imple of 
        /// </summary>
        /// <param name="newSize"></param>
        private uint rehash(DataBlock block, int newSize)
        {        
            // maybe add some new rehashing
            return hashFunction.GetHash(block.Key, newSize);
        }

        // implement 
        // - Put (adds one key-value pair)
        // - Put (adds ICollection collection)
        // - Remove(object key)
        // - UpdateValue(object key, object new_value)
        // - something more
        public void CopyTo(Array array, int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerator GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public bool IsSynchronized => throw new NotImplementedException();
        public object SyncRoot => throw new NotImplementedException();
    }
}
