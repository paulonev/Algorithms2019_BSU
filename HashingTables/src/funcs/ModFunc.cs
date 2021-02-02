using System;
namespace src.funcs
{
    public class ModFunc : HashFunc
    {
        public ModFunc() : base()
        {}
        // public ModFunc(Object key, int size) : base(key, size)
        // {}

        public override uint GetHash(Object key, int size)
        {
            return (uint)(key.GetHashCode() % size);
        }
    }
}