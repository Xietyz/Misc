using System;
using System.Runtime.Serialization;

namespace GsaTest.Core.Model
{
    public class CumPnl
    {
        public CumPnl(string region, DateTime date, decimal cumulativePnl)
        {
            Region = region;
            CumulativePnl = cumulativePnl;
            Date = date.ToString(ModelDefaults.DateFormat);
            TheDate = date;
        }

        public DateTime TheDate { get; private set; }
        public string Region { get; private set; }
        public string Date { get; private set; }
        public decimal CumulativePnl { get; private set; }
    }
}
