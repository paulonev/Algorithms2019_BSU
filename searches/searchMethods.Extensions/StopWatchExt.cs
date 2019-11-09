using System;
using System.Diagnostics;

namespace searchMethods.Extensions
{
    public static class StopwatchExt
    {
        public static string GetTimeString(this Stopwatch stopwatch, int numberofDigits = 1)
        {
            double time = stopwatch.ElapsedTicks / (double)Stopwatch.Frequency;
            if (time > 1)
                return Math.Round(time, numberofDigits) + " sec";
            if (time > 1e-3)
                return Math.Round(1e3 * time, numberofDigits) + " millisec";
            if (time > 1e-6)
                return Math.Round(1e6 * time, numberofDigits) + " microsec";
            if (time > 1e-9)
                return Math.Round(1e9 * time, numberofDigits) + " nanosec";
            return stopwatch.ElapsedTicks + " ticks";
        }
    }
}
