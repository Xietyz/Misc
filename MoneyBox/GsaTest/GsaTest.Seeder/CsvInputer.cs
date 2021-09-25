using System;
using System.Collections.Generic;
using System.Linq;
using GsaTest.Core.Model;

namespace GsaTest.Seeder
{
    public class CsvInputer : IDataInputer
    {
        private readonly int Strat_Name_Idx = 0;
        private readonly int Strat_Region_Idx = 1;

        public IEnumerable<Strategy> ReadStrategies(string fileLoc)
        {
            var lines = ReadLines(fileLoc).Skip(1);
            return ParseStrategies(lines);
        }

        // Brittle Reader. Would add more error checking logic
        public IEnumerable<string> ReadLines(string fileLoc)
        {
            string[] lines = System.IO.File.ReadAllLines(fileLoc);
            var cleaned = lines.Where(x => x.Contains(","));

            return cleaned;
        }

        public IEnumerable<Strategy> ParseStrategies(IEnumerable<string> lines)
        {
            foreach (var line in lines)
            {
                var split = line.Split(',');
                if (split.Length != 2)
                    throw new Exception($"Cannot parse line {line}. Expected 2 columns but {split.Length} columns");

                var name = split[Strat_Name_Idx].Trim();
                var region = split[Strat_Region_Idx].Trim();
                yield return new Strategy { StratName = name, Region = region };
            }
        }

        public Dictionary<string, List<Capital>> ReadCapital(string fileLoc)
        {
            var lines = ReadLines(fileLoc);
            return ParseCapital(lines.ToList());
        }

        public Dictionary<string, List<Pnl>> ReadPnl(string fileLoc)
        {
            var lines = ReadLines(fileLoc);
            return ParsePnl(lines.ToList());
        }

        public Dictionary<string, List<Capital>> ParseCapital(List<string> lines)
        {
            string[] columnHeaders = ColumnHeaders(lines).ToArray();

            List<List<Capital>> columns = ReadColumns<Capital>(lines.Skip(1), columnHeaders.Length, 1, (date, amount) => new Capital() { Date = date, Amount = amount });

            return Enumerable.Range(0, columnHeaders.Length)
                            .ToDictionary(x => columnHeaders[x], y => columns[y]);
        }

        public Dictionary<string, List<Pnl>> ParsePnl(List<string> lines)
        {
            string[] columnHeaders = ColumnHeaders(lines).ToArray();

            List<List<Pnl>> columns = ReadColumns<Pnl>(lines.Skip(1), columnHeaders.Length, 1, (date, amount) => new Pnl() { Date = date, Amount = amount});

            return Enumerable.Range(0, columnHeaders.Length)
                            .ToDictionary(x => columnHeaders[x], y => columns[y]);
        }

        private static IEnumerable<string> ColumnHeaders(List<string> lines)
        {
            var headerline = lines.First();
            var headerSpilt = headerline.Split(',');
            if (!("Date".Equals(headerSpilt[0].Trim(), StringComparison.CurrentCultureIgnoreCase)) || headerSpilt.Length < 2)
                throw new Exception("Header not in correct format");
            return headerSpilt.Skip(1);
        }

        private static List<List<T>> ReadColumns<T>(IEnumerable<string> lines, int headerCount, int skipColumns, Func<DateTime, decimal, T> createFunc)
        {
            var columns = new List<List<T>>();
            for (var i = 0; i < headerCount; i++)
            {
                columns.Add(new List<T>());
            }

            foreach (var line in lines)
            {
                var lineCells = line.Split(',');
                var date = DateTime.Parse(lineCells.First());

                for (var i = skipColumns; i < lineCells.Length; i++)
                {
                    columns[i - skipColumns].Add(createFunc(date, decimal.Parse(lineCells[i])));
                }
            }

            return columns;
        }
    }
}
