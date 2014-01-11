﻿using OptimusPrime.Factory;

namespace OptimusPrimeTest.Logger
{
    public class OptimusPrimeLoggerTest : LoggerTestBase
    {
        protected override IFactory CreateFactory()
        {
            return new OptimusPrimeFactory();
        }
    }
}