using CsvPnl.Database;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CsvPnl
{
    public class Program
    {
        static void Main(string[] args)
        {
            StrategyList stratList = new StrategyList();
            StrategyReader _reader = stratList._reader;

            stratList.InitialiseStrategyList();
            //stratList.PopulateStrategyListCapital(stratList.CapitalDataFile);
            Console.WriteLine("COMMANDS: capital, cumulative-pnl, store");
            Console.WriteLine("ENTER COMMAND:");
            string[] commandArray = Console.ReadLine().Split(" ");
            switch (commandArray[0])
            {
                case "capital":
                    {
                        string[] outputArray;
                        outputArray = stratList.PrintStrategyCapitals(commandArray[1], stratList).ToArray();
                        Console.WriteLine(String.Join("\n", outputArray));
                    }
                    break;
                case "cumulative-pnl":
                    {
                        string[] outputArray;
                        outputArray = stratList.PrintRegionCumulativePnl(commandArray[1], stratList).ToArray();
                        Console.WriteLine(String.Join("\n", outputArray));
                    }
                    break;
                case "store":
                    {
                        PnlDbService dbService = new PnlDbService(new PnldbContext());
                        var pnls = new List<Pnl>();
                        foreach (var strat in stratList._list)
                        {
                            foreach (var pnl in strat.Pnls)
                            {
                                pnls.Add(pnl);
                            }
                        }
                        var caps = new List<Capital>();
                        foreach (var strat in stratList._list)
                        {
                            foreach (var cap in strat.Capitals)
                            {
                                caps.Add(cap);
                            }
                        }
                        var strats = new List<Strategy>();
                        foreach (var strat in stratList._list)
                        {
                            strats.Add(dbService.StrategyPnlToEntity(strat));
                        }
                        dbService.StoreToDb(pnls, strats, caps);
                        Console.WriteLine("saved?");
                    }
                    break;
            }
        }
    }
}
