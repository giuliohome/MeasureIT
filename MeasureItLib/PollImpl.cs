using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureItLib
{
    public class PollImpl : IPoll<int, string>
    {
        async Task<string> IPoll<int, string>.poll(int milliseconds)
        {
            var timer = new Stopwatch();
            timer.Start();
            await Task.Delay(milliseconds);
            timer.Stop();
            return "awaited " + milliseconds + " ms in " 
                + timer.ElapsedMilliseconds + " ms";
        }
    }
}
