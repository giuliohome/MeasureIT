using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MeasureItLib;
using System.Threading.Tasks;

namespace MeasureIt
{
    class Program
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
        static void Main(string[] args)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            IPoll<int,string> polling = new PollImpl();
            IMeasure<int, string, Exception> measuring = new MeasureImpl();

            var stats = TestMeasure(polling, measuring, rnd);
            stats.Wait();
            Console.WriteLine("mean: " + stats.Result.average());
            Console.WriteLine("dev. std: " + stats.Result.stdDev());
            Console.ReadLine();
        }


        static async Task<Stats> TestMeasure(IPoll<int,string> polling, 
            IMeasure<int, string, Exception> measuring, Random rnd)
        {
            var stats = new Stats();
            for (int i = 0; i < 20; i++)
            {
                var input = rnd.Next(500);
                var measure = await measuring.measure(polling, input);
                Console.WriteLine("Input " + input + 
                    " Done after " + measure.elapsed.Milliseconds);
                Console.WriteLine(measure.success ? "success" : "failure");
                Console.WriteLine(measure.success ? measure.output : measure.error.Message);
                stats.addEvent(measure.elapsed.Milliseconds);
                if (stats.StopAfter(2000,250))
                {
                    break;
                }
            }
            return stats;
        }
    }
}
