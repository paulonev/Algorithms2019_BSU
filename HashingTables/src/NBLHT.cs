﻿using System.Security.AccessControl;
using System.Reflection.Emit;
using System;
using System.Collections;
using System.Collections.Generic;
using src.funcs;

namespace NLBHashtable
{

/// <summary>
/// Class which encapsulates basic unit of data 
/// </summary>
    public class DataBlockNode
    {
        public DataBlockNode _next;
        public object  _key;
        public object  _value;
        public uint    _libHashCode; // hash code produced by my_hash_library
    
        // public object Key { get => _key;}
        // public object Value { get => _value; }
        // public uint HashCode { get => _libHashCode; }

        public DataBlockNode()
        {
            _key = null;
            _value = null;
            _libHashCode = 0;
            _next = null;
        }

        public DataBlockNode(object key, object value)
        {
            _key = key;
            _value = value;
            _libHashCode = 0;
            _next = null;
        }

        public DataBlockNode(object key, object value, uint hashCode)
        {
            _key = key;
            _value = value;
            _libHashCode = hashCode;
            _next = null;
        }

        public DataBlockNode(object key, object value, DataBlockNode child)
        {
            _key = key;
            _value = value;
            _next = child;
        }

        public DataBlockNode(object key, object value, uint hashCode, DataBlockNode child)
        {
            _key = key;
            _value = value;
            _next = child;
            _libHashCode = hashCode;
        }

        public bool KeyEquals(object key)
        {
             if (this._key.Equals(key))
                return true;
            else 
                return false;
        }
    }

/// <summary>
/// 
/// </summary>
    public class DataBlock : IEnumerable
    {
        DataBlockNode head;

        public int Count { get; set; }
        public DataBlockNode Head { get => head; }
        
        public DataBlock()
        {
            head = new DataBlockNode();
        }
        public DataBlock(DataBlockNode head)
        {
            this.head = head;
        }
        public void AddLast(DataBlockNode node)
        {
            if (node == null)
                throw new ArgumentException("[EXC03--AddLast] Failed to add null reference");
            if (head == null)
            {
                head = node;
            }
            else
            {
                DataBlockNode t = head;
                while(t._next != null)
                {
                    t = t._next;
                }
                t._next = node;
            }
            Count++;
        }

        public void AddFirst(DataBlockNode node)
        {
            if (node == null)
                throw new ArgumentException("[EXC03--AddFirst] Failed to add null reference");
            if (head == null)
            {
                head = node;
            }
            else
            {
                node._next = head;
                head = node;
            }
            Count++;
        }
    
        /// <summary>
        /// Method that is called from NLBHT.Remove to take an element out of chaining hashtable
        /// </summary>
        /// <param name="block"> </param>
        /// <param name="node"> </param>
        /// <returns>
        /// 1 - if item was found and successfully removed
        /// 0 - if item wasn't found
        ///-1 - if some exception happened
        /// </returns>
        public int Remove(DataBlockNode node)
        {
            if (node == null)
                return -1;

            DataBlockNode t = this.head;
            if(t._key == node._key) // if searched item is head
            {
                head = t._next;
                return 1;
            }
            while(t._next != null)
            {
                if (t._next._key == node._key) break;
                t = t._next;
                if(t == null)
                    return 0;
            }
            t._next = t._next._next;
            return 1;
        }

        public IEnumerator GetEnumerator()
        {
            return new DataBlockEnumerator(this);
        }
    }
    public class DataBlockEnumerator : IEnumerator
    {
        DataBlock _block;
        DataBlockNode _current;

        public object Current
        {
            get => _current;
        }

        /// <summary></summary>
        /// <returns>
        /// false - if reach end of collection
        /// true - otherwise
        /// </returns>
        public bool MoveNext()
        {
            _current = _current._next;
            return (_current != null);
        }

        public void Reset()
        {
            throw new NotImplementedException();
        }

        public DataBlockEnumerator(DataBlock block)
        {
            _block = block;
            _current = block.Head;
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
        const int _DEFAULT_CAPACITY = 7;

        // =====PRIV========
        DataBlock[] _blocks;
        HashFunc hashFunction;
        int _count;
        
        // =====PROPS========
        public float LoadFactor { 
            get => _count / _blocks.Length;
        }

        // Table Capacity
        public int TabSize { 
            get => _blocks.Length; 
        }

        public int Count { 
            get => _count;
            set => _count = value; 
        }

        // ======CTOR========
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
        /// Collision resolution will be CHAINING
        /// </summary>
        /// <param name="key">unique elem in pair</param>
        /// <param name="value">data of any type</param>
        public void Put(object key, object value)
        {
            // null checking
            if (key == null)
                throw new ArgumentException("[EXC04] Unable to put null reference as a key to the table");
            // LoadFactor is an average amount of items per DataBlock(bucket)
            if (LoadFactor > 0.75f) resizeBlocks();
            
            uint hashIdx = hashFunction.GetHash(key, TabSize);
            // collision resolution
            _blocks[hashIdx].AddFirst(new DataBlockNode(key, value, hashIdx));
            Count++;
            //GetHash(key) & 0x7FFFFFFF
        }

        private void resizeBlocks()
        {
            // 1) allocate space for new array[2*N]
            //FIX: newSize is ought to be nearest prime to 2*Size
            int newSize = 2*TabSize;
            DataBlock[] newBlocks = new DataBlock[newSize];
            // 2) rehash all values in smaller array
            // 3) add instances to bigger array
            for (int i = 0; i < TabSize; i++)
            {
                DataBlock block = _blocks[i];
                foreach (DataBlockNode item in block)
                {
                    uint rehashIdx = rehash(item, newSize);
                    
                    if (rehashIdx != item._libHashCode)
                    {
                        newBlocks[rehashIdx].AddFirst(item); 
                    }
                    else throw new Exception($"[EXC02] Rehashing produced the same hashcode on {item._key} item key");
                }
            }
            _blocks = newBlocks;
        }
        private uint rehash(DataBlockNode block, int newSize)
        {        
            // maybe add some new rehashing
            return hashFunction.GetHash(block._key, newSize);
        }

        // implement 
        // - Put (adds one key-value pair) +
        // - Put (adds ICollection collection)
        // - Remove(object key) +
        // - UpdateValue(object key, object new_value)
        // - Contains(object key) +
        // - Clear ()

        /// <summary>
        /// Tells if there is any object with that key in a table
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool Contains(object key)
        {
            if (key == null)
                throw new ArgumentException("[EXC04] Unable to search null reference key in the table");
            uint hashIdx = hashFunction.GetHash(key, TabSize);
            if (hashIdx > TabSize)
                return false;
            else
            {
                foreach(DataBlockNode item in _blocks[hashIdx])
                {
                    if(item.KeyEquals(key) && hashIdx == item._libHashCode) 
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Imple of removing item of table (from particular chaining list)
        /// </summary>
        /// <param name="key"></param>
        /// <returns> 
        /// <see cref="DataBlock.Remove(DataBlockNode)"/> for return values
        /// </returns>
        public int Remove(object key)
        {
            if (key == null)
                // return -1;
                throw new ArgumentException("[EXC04] Unable to search null reference key in the table");
            uint hashIdx = hashFunction.GetHash(key, TabSize);
            if (hashIdx > TabSize)
                return 0;
            else
            {
                foreach(DataBlockNode item in _blocks[hashIdx])
                {
                    if(item.KeyEquals(key) && hashIdx == item._libHashCode) 
                    {
                        Count--;
                        return _blocks[hashIdx].Remove(item);
                    }
                }
                return 0;
            }
        }

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