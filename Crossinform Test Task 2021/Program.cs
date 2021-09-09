using System;
using System.Diagnostics;
using System.Linq;

namespace MainDen.Crossinform.TestTask2021
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine(Localization.Info);
            }
            else
            {
                var path = args[0];

                try
                {
                    var stopwatch = Stopwatch.StartNew();
                    var occurrences = TextData.FromFile(path).GetOccurrencesOfTokens(3);
                    var result = occurrences.OrderByDescending(pair => pair.Value).Take(10).Select(pair => pair.Key);
                    stopwatch.Stop();

                    Console.WriteLine(String.Join(',', result));
                    Console.WriteLine(stopwatch.ElapsedMilliseconds);
                }
                catch (Exception exc)
                {
                    Console.WriteLine(exc.Message);
                }
            }
        }
    }
}
