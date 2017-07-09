using System;
using System.Collections.Generic;
using System.Linq;

namespace MeasureIt
{
    public class Stats
    {
        int totDuration = 0;
        int totEvents = 0;
        double mean = 0;
        double devStd = 0;

        List<int> results = new List<int>();

        public void addEvent(int milliseconds)
        {
            totEvents++;
            totDuration += milliseconds;
            results.Add(milliseconds);
            mean = results.Average();
            devStd = Math.Sqrt(
            results.Select(r => (r - mean) * (r - mean)).Sum()
            / totEvents
            );
        }

        public double average()
        {
            return mean;
        }

        public double stdDev()
        {
            return devStd;
        }

        public bool StopAfter(int maxMilliSecs, double defEstimate)
        {
            if (totEvents == 0)
            {
                return defEstimate >= maxMilliSecs;
            }
            return totDuration + mean + devStd > maxMilliSecs;
        }
    }
}
