﻿using System.Collections.Generic;

namespace OptimusPrime.Templates
{
    public interface ISourceDataCollectionFactory<out TDataCollection, in T1>
        where TDataCollection : ISourceDataCollection
    {
        TDataCollection Create(IEnumerable<T1> argument1);
    }
}