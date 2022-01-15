using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Multithreading
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] nameList = new string[]
            {
                "Leo",
                "Sean",
                "Alex",
                "Wayne",
                "Bruce",
                "Connor",
                "Emmanuel",
                "Emma",
                "Johnny",
                "Lee",
                "Sena",
                "Alexa",
            };
            var breakIndex = nameList.Count() - 1;
            var matchedNameList = new ConcurrentBag<MatchedName>();
            Parallel.For(0, breakIndex, (i, state) =>
            {
                var newMatchedName = new MatchedName();
                newMatchedName.Name1 = nameList[(int)i];
                double score;
                for(int x = 0; x < breakIndex; x++)
                {
                    if (newMatchedName.Name1.Equals(nameList[x]))
                    {
                        continue;
                    }
                    var name2 = nameList[x];
                    score = FuzzySharp.Levenshtein.GetRatio(newMatchedName.Name1, name2);
                    if (score > newMatchedName.Score)
                    {
                        newMatchedName.Score = score;
                        newMatchedName.Name2 = name2;
                    }
                }
                matchedNameList.Add(newMatchedName);
            });
            var matchedNameList2 = matchedNameList.OrderByDescending(x => x.Score).AsEnumerable();
            foreach(var scoring in matchedNameList2)
            {
                Console.WriteLine(scoring.ToString());
            }
        }
    }
    public class MatchedName
    {
        public MatchedName()
        {
            Score = 0;
        }
        public string Name1 { get; set; }
        public string Name2 { get; set; }
        public double Score { get; set; }
        public override string ToString()
        {
            return $"{Name1} -> {Name2}: {Score}";
        }
    }
}
//foreach (var name in nameList)
//{
//    var name2 = nameList[(int)i];
//    score = FuzzySharp.Levenshtein.GetRatio(newMatchedName.Name1, name2);
//    if (score > newMatchedName.Score)
//    {
//        newMatchedName.Score = score;
//        newMatchedName.Name2 = name2;
//    }
//}

//var name1 = nameList[(int)i];
//var name2 = nameList[(int)i + 1];
//var score = FuzzySharp.Levenshtein.GetRatio(name1, name2);
//var newMatchedName = new MatchedName(name1, name2);
//matchedNameList.Add(newMatchedName);