using System;
namespace src.funcs
{
    public class MultFunc : HashFunc
    {
        //double precision value with .
        private Double _constant;
        public MultFunc(double c) : base()
        {
            _constant = c;
        }

        public override uint GetHash(Object key, int size)
        {
            // why GetHashCode() returns different results
            // var keyHashCode = key.GetHashCode();
            // Console.WriteLine("{0} hash code is {1}", key.GetType(), keyHashCode);
            // var resDouble = size * frac_part(key.GetHashCode() * _constant);
            // Console.WriteLine("double index is {0}", resDouble);
            // var res = (uint)resDouble;
            return (uint)(size * frac_part(key.GetHashCode() * _constant));
        }

        private double frac_part(double v)
        {
            double fraction = v - ((int)v);
            return fraction < 0 ? (0-fraction) : fraction;
        }

    }
}