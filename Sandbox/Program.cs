using System;
using System.Collections.Generic;
using System.Linq;

namespace Sandbox
{
    // the Counter class is information
    // that is stored for a "yes", "no" or "maybe" object
    public class Counter
    {
        // percentage is a nullable value and not a zero
        private double? _percentage;

        public Counter(string name, int count)
        {
            Name = name;
            Count = count;
        }

        // created properties for choice options 
        // and the count for each
        public string Name { get; }
        public int Count { get; private set; }

        // return as a checked null, then assigned to percent
        public double GetPercent(int total) => _percentage ?? (_percentage = Math.Round(Count * 100.0 / total, 2)).Value;

        public void AddExcess(double excess) => _percentage += excess;

    }

    public class CounterManager
    {
        // makes the CounterManager constructor messier
        public CounterManager(params Counter[] counters)
        {
            Counters = new List<Counter>(counters);
        }

        // Counters property is a List of Counter
        public List<Counter> Counters { get; set; }

        public int Total() => Counters.Sum(x => x.Count);

        public double TotalPercent() => Counters.Sum(x => x.GetPercent(Total()));

        public void AnnouncWinner()
        {
            // calculate the remaining 0.01 percent
            var excess = Math.Round(100 - TotalPercent(), 2);

            // display that excess 0.01 percent
            Console.WriteLine($"Excess: {excess}");

            var biggestAmountOfVotes = Counters.Max(x => x.Count);

            var winners = Counters.Where(x => x.Count == biggestAmountOfVotes).ToList();

            if (winners.Count == 1)
            {
                var winner = winners.First();
                winner.AddExcess(excess);
                Console.WriteLine($"{winner.Name} Won");
            }
            else
            {
                if (winners.Count != Counters.Count)
                {
                    var lowestAmountOfVotes = Counters.Min(x => x.Count);
                    var loser = Counters.First(x => x.Count == biggestAmountOfVotes);
                    loser.AddExcess(excess);

                }
                Console.WriteLine(string.Join(" -DRAW- ", winners.Select(x => x.Name)));
            }

            // application statistics
            foreach (var c in Counters)
            {
                Console.WriteLine($"{c.Name} Counts: {c.Count}, Percentage: {c.GetPercent(Total())}%");
            }

            Console.WriteLine($"Total Percentage: {Math.Round(TotalPercent(), 2)}%");
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            // create variables to access the information
            // of the Counter class
            var yes = new Counter("Yes", 4);
            var no = new Counter("No", 4);
            var maybe = new Counter("Maybe", 4);
            var hopefully = new Counter("Hopefully", 4);

            // makes the CounterManager contructor cleaner
            var manager = new CounterManager(yes, no, maybe, hopefully);

            manager.AnnouncWinner();
        }
    }
}
