using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MeasureItLib
{
    public class MeasureImpl : IMeasure<int, string, Exception>
    {
        async Task<PollingReturn<string, Exception>> IMeasure<int, string, Exception>.measure(IPoll<int, string> polling,
            int input)
        {
            var timer = new Stopwatch();
            timer.Start();
            var ret = new PollingReturn<string, Exception>();
            var pollingTask = polling.poll(input);
            try
            {
                ret.output = await pollingTask;
                ret.success = true;
            }
            catch (Exception exc)
            {
                ret.error = exc;
            }
            timer.Stop();
            ret.elapsed = timer.Elapsed;
            return ret;
        }
    }
}
