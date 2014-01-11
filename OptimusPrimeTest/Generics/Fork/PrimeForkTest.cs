﻿using OptimusPrime.Factory;

namespace OptimusPrimeTests.Generics.Fork
{
    public class PrimeForkTest : ForkTestBase
    {
        protected override IFactory CreateFactory()
        {
            return new OptimusPrimeFactory();
        }
    }
}