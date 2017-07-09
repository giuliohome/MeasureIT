using System;
using MeasureItLib;
using System.Threading.Tasks;

namespace MeasureIt
{
    class Program
    {
       
        static void Main(string[] args)
        {
            Random rnd = new Random(DateTime.Now.Millisecond);
            IPoll<int,string> polling = new PollImpl();
            IMeasure<int, string, Exception> measuring = new MeasureImpl();

            var stats = App.TestMeasure(polling, measuring, rnd);
            stats.Wait();
            Console.WriteLine("mean: " + stats.Result.average());
            Console.WriteLine("dev. std: " + stats.Result.stdDev());
            Console.ReadLine();
        }
    }
}
