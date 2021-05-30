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
                new AnalyticEvents(0, "start"),
                new AnalyticEvents(0, "action"),
                new AnalyticEvents(1, "start"),
                new AnalyticEvents(2, "start"),
                new AnalyticEvents(0, "action"),
                new AnalyticEvents(1, "action"),
                new AnalyticEvents(3, "start"),
                new AnalyticEvents(3, "error"),
                new AnalyticEvents(1, "end"),
                new AnalyticEvents(2, "end")
                };
            }
        }
    }
}
