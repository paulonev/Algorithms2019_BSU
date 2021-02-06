using System.ComponentModel.DataAnnotations;
using System;

namespace src.funcs
{
    public class ModFunc : HashFunc
    {
        public ModFunc() : base()
        {}
        
        public override uint GetHash(Object key, int size)
        {
            var keyCode = key.GetHashCode();
            keyCode = keyCode > 0 ? keyCode : 0-keyCode;
            return (uint)(keyCode % size);
        }
    }
}