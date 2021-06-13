using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace ConsoleApp2
{
    public enum EState
    {
        NonExistent,
        Created,
        Finished
    }
    public enum IDEvent
    {
        Create,
        Action,
        Error,
        End
    }

    public class Analytics
    {
        Dictionary<IDEvent, EState> StateDict
        {
            get
            {
                Dictionary<IDEvent, EState> dict = new Dictionary<IDEvent, EState>();
                dict.Add(IDEvent.Create, EState.Created);
                dict.Add(IDEvent.Action, EState.Created);
                dict.Add(IDEvent.End, EState.Finished);
                dict.Add(IDEvent.Error, EState.Created);
                return dict;
            }
        }
        //task method
        public AnalyticEvents[] CalculateProgress(AnalyticEvents[] events)
        {
            var test = new AnalyticEvents[] {
                events.Last(x => x.Id == 0),
                events.Last(x => x.Id == 1),
                events.Last(x => x.Id == 2),
                events.Last(x => x.Id == 3),
            };
            return test;
        }

        // events have an id attached to them
        // all ordered properly
        // use IDs of event list to get current STATE of ID
        public AnalyticEvents[] GetLatestEvents(AnalyticEvents[] events)
        {
            //var test = new List<IDEvent>();
            var test = events.GroupBy(x => x.Id)
                .Select(g => g.Last())
                .ToArray();

            return test;
        }
        public EState GetState(AnalyticEvents[] events)
        {
            //get 1 id's state
            var test = events.Select(x => x.Event).Last();
            StateDict.TryGetValue(test, out EState state);
            return state;
        }
        public EState[] GetStates(AnalyticEvents[] events)
        {
            var eventArray = GetLastEvents(events).ToArray();
            var stateArray = new EState[eventArray.Count()];

            for (int x = 0; x < stateArray.Count(); x++)
            {
                StateDict.TryGetValue(eventArray[x], out EState state);
                stateArray[x] = state;
            }
            return stateArray;
        }
        public IDEvent[] GetLastEvents(AnalyticEvents[] events)
        {
            var filteredEvents = events.GroupBy(x => x.Id)
            .Select(g => g.Last())
            .OrderBy(x => x.Id)
            .Select(x => x.Event)
            .ToArray();
            return filteredEvents;
        }
    }

    public class AnalyticEvents
    {
        public int Id { get; }
        public IDEvent Event { get; }

        public AnalyticEvents(int id, IDEvent ev)
        {
            Id = id;
            Event = ev;
        }
    }
}
