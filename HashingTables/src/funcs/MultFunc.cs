using System;
namespace src.funcs
{
    public class MultFunc : HashFunc
    {
        //single precision value with .
        private Double _constant;
        public MultFunc(double c) : base()
        {
            _constant = c;
        }

        public override uint GetHash(Object key, int size)
        {
            return (uint)(size * frac_part(key.GetHashCode() * _constant));
        }

        private double frac_part(double v)
        {
            return v - Math.Floor(v);
        }

    }
}