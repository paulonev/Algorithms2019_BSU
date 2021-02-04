using System;
namespace src.funcs
{
    public abstract class HashFunc
    {
        // public Object Key { get; set; } 
        // public int TabSize { get; set; }
        
        // public HashFunc(Object key, int size)
        // {
        //     Key = key;
        //     TabSize = size;
        // }
        public abstract uint GetHash(Object key, int size);

        public override string ToString()
        {
            return base.ToString();
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}