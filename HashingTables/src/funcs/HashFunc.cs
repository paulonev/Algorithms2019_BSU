using System;
namespace src.funcs
{
    public abstract class HashFunc
    {
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