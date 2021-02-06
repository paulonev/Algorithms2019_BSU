
using System;
using System.Collections;

namespace src.instances
{
    /// <summary>
    /// Class which encapsulates basic unit of data 
    /// </summary>
    public class DataBlockNode
    {
        public DataBlockNode    _next;
        public object           _key;
        public object           _value;
        public uint             _hcd; // hash code produced by my_hash_library

        public DataBlockNode()
        {
            _key = null;
            _value = null;
            _hcd = 0;
            _next = null;
        }

        public DataBlockNode(DataBlockNode item)
        {
            this._key = item;
        }

        public DataBlockNode(object key, object value)
        {
            _key = key;
            _value = value;
            _hcd = 0;
            _next = null;
        }

        public DataBlockNode(object key, object value, uint hashCode)
        {
            _key = key;
            _value = value;
            _hcd = hashCode;
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
            _hcd = hashCode;
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
    /// Collision resolution
    /// </summary>
    public class DataBlock : IEnumerable
    {
        DataBlockNode head;

        public int Count { get; set; }
        public DataBlockNode Head { get => head; }
        
        public DataBlock()
        {
            head = null;
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

            if(head._key == node._key) // if searched item is head
            {
                head = head._next;
                return 1;
            }

            DataBlockNode t = this.head;
            while(t._next != null)
            {
                if (t._next._key == node._key && t._next._hcd == node._hcd) 
                    break;

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
}