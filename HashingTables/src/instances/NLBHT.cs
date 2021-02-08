using System;
using System.Collections;
using System.Text;
using src.funcs;

namespace src.instances
{
    /*
        * Store (key,value) pairs associatively in array
        ** for each (key,value) pair to be added produce
        *** a hashIndex that will indicate elem's array pos.
        *** key - a string converted value
        *** value - data of any type: reference or value-type
        ** ICollection, IEnumerable, ICloneable
        * having a hidden implementation of CRUD operations
    */
    
    // implement 
    // - Put (adds one key-value pair) +
    // - Put (adds ICollection collection) +
    // - TryPut (adds if key != null or duplicate) +
    // - Remove(object key) +
    // - bool Contains(object key) +
    // - void Clear () +
    // - UpdateValue(object key, object new_value) - indexer

    public class NLBHT : ICollection
    {
        const int _DEFAULT_CAPACITY = 3;

        // =====PRIV========
        DataBlock[] _blocks;
        HashFunc hashFunction;
        int _count;
        
        // =====PROPS========
        public float LoadFactor { 
            get => (float)(_count / (_blocks.Length*1.0));
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
            _blocks.FillBlocks(_blocks.Length);
            hashFunction = null;
        }
        public NLBHT(int size)
        {
            if (size < 0) throw new ArgumentException("[EXC01] Please, check size parameter in initialization\n");

            _blocks = new DataBlock[size];
            _blocks.FillBlocks(_blocks.Length);
            hashFunction = null;
        }
        public NLBHT(HashFunc hF) : this()
        {
            this.hashFunction = hF;
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
        /// Put a collection as keys to the table
        /// </summary>
        /// <param name="coll"></param>
        public void Put(ICollection coll)
        {
            if (coll == null) 
                throw new ArgumentException("[EXC06] Unable to put null reference collection to the table");
            if (coll.Count == 0)
                return;
            
            foreach (var item in coll)
            {
                this.Put(item, null /*or item.GetHashCode()*/);
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
        /// <exception cref="System.ArgumentException">Thrown when attempt to add duplicated key</exception>
        public void Put(object key, object value)
        {
            // null checking
            if (key == null)
                throw new ArgumentException("[EXC04] Unable to put null reference as a key to the table");
            
            // LoadFactor is an average amount of items per DataBlock(bucket)
            if (LoadFactor > 0.75f) resizeBlocks();
            
            uint newHCD = hashFunction.GetHash(key, TabSize);
            Console.WriteLine("hashfunc produced {0} hashcode", newHCD);
            foreach (DataBlockNode item in _blocks[newHCD])
            {
                if (item._hcd == newHCD && item.KeyEquals(key))
                {
                    throw new ArgumentException(String.Format("[EXC07] An item with key `{0}` has already been added", key));
                }
            }
            _blocks[newHCD].AddFirst(new DataBlockNode(key, value, newHCD));
            Count++;
            //GetHash(key) & 0x7FFFFFFF = negative number -> 0
        }

        /// <summary>
        /// Method can be used to let the programmer know if it's possible to
        /// add a new item to the table without throwing any Exception
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <returns>
        /// true - if item was successfully added to the table
        /// false - if key is null or duplicate
        /// </returns>
        public bool TryPut(object key, object value)
        {
            // null checking
            if (key == null)
                return false;

            // LoadFactor is an average amount of items per DataBlock(bucket)
            if (LoadFactor > 0.75f) resizeBlocks();

            uint newHCD = hashFunction.GetHash(key, TabSize);
            Console.WriteLine("hashfunc produced {0} hashcode", newHCD);
            foreach (DataBlockNode item in _blocks[newHCD])
            {
                if (item._hcd == newHCD && item.KeyEquals(key))
                {
                    Console.WriteLine("Attempting to add duplicate key `{0}` to the table", key);
                    return false;
                }
            }
            _blocks[newHCD].AddFirst(new DataBlockNode(key, value, newHCD));
            Count++;
            return true;
        }


        private void resizeBlocks()
        {
            // 1) allocate space for new array[2*N]
            //FIX: newSize is ought to be nearest prime to 2*Size
            int newSize = 2*TabSize;
            DataBlock[] newBlocks = new DataBlock[newSize];
            newBlocks.FillBlocks(newBlocks.Length);
            // 2) rehash all values in smaller array
            // 3) add instances to bigger array
            for (int i = 0; i < TabSize; i++)
            {
                DataBlock block = _blocks[i];
                foreach (DataBlockNode item in block) //[BUG] forever loop
                {
                    uint rehashIdx = rehash(item, newSize);
                    if (rehashIdx != item._hcd)
                    {
                        newBlocks[rehashIdx].AddFirst(new DataBlockNode(item._key, item._value, rehashIdx)); 
                    }
                    // else throw new Exception($"[EXC02] Rehashing produced the same hashcode on {item._key} item key");
                    else
                    {
                        Console.WriteLine($"Rehashing produced the same hashcode on '{item._key}'");
                        newBlocks[rehashIdx].AddFirst(new DataBlockNode(item._key, item._value, rehashIdx));
                    }
                }
            }
            _blocks = newBlocks;
        }
        private uint rehash(DataBlockNode block, int newSize)
        {        
            // maybe add some new rehashing
            return hashFunction.GetHash(block._key, newSize);
        }

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
                    if(item.KeyEquals(key) && hashIdx == item._hcd) 
                        return true;
                }
                return false;
            }
        }

        /// <summary>
        /// Imple of removing item from table (from particular chaining list)
        /// </summary>
        /// <param name="key"></param>
        /// <returns> 
        /// <see cref="DataBlock.Remove(DataBlockNode)"> for return values </see>
        /// <exception cref="System.ArgumentException">Thrown when search key is null</exception>
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
                    if(item.KeyEquals(key) && hashIdx == item._hcd) 
                    {
                        Count--;
                        return _blocks[hashIdx].Remove(item);
                    }
                }
                return 0;
            }
        }

        /// <summary>
        /// Method that removes all instances stored in the table
        /// by clearing each DataBlock
        /// </summary>
        /// <see cref="src.instances.DataBlock.Clear()"/>
        /// <returns></returns>
        public void Clear()
        {
            if (Count == 0)
                return;
            for (int i = 0; i < _blocks.Length; i++)
            {
                DataBlock block = _blocks[i];
                block.Clear();
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

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("[");
            for (int i = 0; i < TabSize; i++)
            {
                sb.Append($"\n\t{i}: [");
                foreach(DataBlockNode item in _blocks[i])
                {   
                    if(item._key != null)
                        sb.Append("\n{").Append($" hash={item._hcd}, key={item._key.ToString()}, value={item._value.ToString()}").Append(" }");
                }
                sb.Append("\n ]");
            }
            sb.Append("\n]");
            return sb.ToString();
        }
    }
}
