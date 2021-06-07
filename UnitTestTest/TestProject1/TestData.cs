using ConsoleApp2;
using System;
using System.Collections.Generic;
using System.Text;

namespace TestProject1
{
    internal static class TestData
    {
        internal static AnalyticEvents[] Basic
        {
            get
            {
                return new[] {
                new AnalyticEvents(0, IDEvent.Create),
                new AnalyticEvents(0, IDEvent.Action),
                new AnalyticEvents(1, IDEvent.Create),
                new AnalyticEvents(2, IDEvent.Create),
                new AnalyticEvents(0, IDEvent.Action),
                new AnalyticEvents(1, IDEvent.Action),
                new AnalyticEvents(3, IDEvent.Create),
                new AnalyticEvents(3, IDEvent.Error),
                new AnalyticEvents(1, IDEvent.End),
                new AnalyticEvents(2, IDEvent.End)
                };
            }
        }
    }
}
