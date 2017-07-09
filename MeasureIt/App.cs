using System;
using MeasureItLib;
using System.Threading.Tasks;

namespace MeasureIt
{
    public class App
    {
        public static async Task<Stats> TestMeasure(IPoll<int, string> polling,
            IMeasure<int, string, Exception> measuring, Random rnd)
        {
            var stats = new Stats();
            for (int i = 0; true; i++)
            {
                if (stats.StopAfter(3000, 250))
                {
                    Console.WriteLine("Stopped condition fired at iteration " + i);
                    break;
                }
                var input = rnd.Next(500);
                var measure = await measuring.measure(polling, input);
                Console.WriteLine("Input " + input +
                    " Done after " + measure.elapsed.Milliseconds);
                Console.WriteLine(measure.success ? "success" : "failure");
                Console.WriteLine(measure.success ? measure.output : measure.error.Message);
                stats.addEvent(measure.elapsed.Milliseconds);
            }
            return stats;
        }
    }
}
