using System;
using System.Threading.Tasks;

namespace MeasureItLib
{
    public interface IPoll<TIn, TOut>
    {
        Task<TOut> poll(TIn input);
    }

    public class PollingReturn<TOut, TError>
    {
        public TimeSpan elapsed;
        public bool success;
        public TOut output; 
        public TError error;
    }
    
    public interface IMeasure<TIn, TOut, TError>
    {
        Task<PollingReturn<TOut, TError>> measure(IPoll<TIn, TOut> poll,
            TIn input);
    }
}
