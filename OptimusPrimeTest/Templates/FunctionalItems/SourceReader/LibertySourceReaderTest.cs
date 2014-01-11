﻿using OptimusPrime.Factory;

namespace OptimusPrimeTests.Templates.SourceReader
{
    public class LibertySourceReaderTest : SourceReaderTestBase
    {
        protected override IFactory CreateFactory()
        {
            return new CallFactory();
        }
    }
}