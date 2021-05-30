using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp2
{
    static public class Analytics
    {
        //task method
        //List<int> ids, 
        static public AnalyticEvents[] CalculateProgress(AnalyticEvents[] events)
        {
            //get latest state of id probs a better way
            var test = new AnalyticEvents[] {
                events.Last(x => x.Id == 0),
                events.Last(x => x.Id == 1),
                events.Last(x => x.Id == 2),
                events.Last(x => x.Id == 3),
            };
            return test;
        }
    }

    public class AnalyticEvents
    {
        public int Id { get; set; }
        public string Event { get; set; }
        
        public AnalyticEvents(int id, string ev)
        {
            Id = id;
            Event = ev;
        }
        public AnalyticEvents(){}
    }
}
