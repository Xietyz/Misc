using System.Collections.Generic;
using GsaTest.Core.Model;

namespace GsaTest.Seeder
{
    public interface IDataInputer
    {
        IEnumerable<Strategy> ReadStrategies(string fileLoc);
        Dictionary<string, List<Capital>> ReadCapital(string fileLoc);
        Dictionary<string, List<Pnl>> ReadPnl(string fileLoc);
    }
}