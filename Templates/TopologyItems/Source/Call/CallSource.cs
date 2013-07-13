﻿using System.Collections.Generic;
using System.Threading;

namespace OptimusPrime.Templates
{
    public class CallSource<T> : ICallSource<T>
    {
        public IList<T> Collection { get; private set; }
        public AutoResetEvent AutoResetEvent { get; private set; }

        public CallSource()
        {
            Collection = new List<T>();
            AutoResetEvent = new AutoResetEvent(false);
        }

        public CallSource(IList<T> collection)
        {
            Collection = collection;
            AutoResetEvent = new AutoResetEvent(false);
        }

        public ISourceReader<T> CreateReader()
        {
            return new SourceReader<T>(this);
        }
    }
}