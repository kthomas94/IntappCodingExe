using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

namespace IntappCodingExe
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<AlphaVantage> values = File.ReadAllLines("daily_MSFT.csv")
                                           .Skip(1)
                                           .Select(v => AlphaVantage.FromCsv(v))
                                           .ToList(); 
            Console.WriteLine("hello world!\nAverage MSFT for the past 7 days = $" + (float)avgMSFT(values));

            List<AlphaVantage> values2 = File.ReadAllLines("daily_AAPL.csv")
                                           .Skip(1)
                                           .Select(v => AlphaVantage.FromCsv(v))
                                           .ToList();

            Console.WriteLine("Highest Closing Price within the last 6 months = $" + (float)hiCloseAAPL(values2));

            List<AlphaVantage> values3 = File.ReadAllLines("daily_BA.csv")
                                           .Skip(1)
                                           .Select(v => AlphaVantage.FromCsv(v))
                                           .ToList();
            Console.WriteLine("Here are the differences between the opening and closing prices of BA");
            foreach (decimal temp in diffOpClBA(values3))
                Console.WriteLine("$" + temp);
        }

        /// <summary>
        /// Finding average volume of MSFT
        /// </summary>
        /// <param name="alphas"></param>
        /// <returns></returns>
        public static decimal avgMSFT(List<AlphaVantage> alphas)
        {
            decimal avg = 0;
            var sevenDays = alphas.Take(7);

            foreach(AlphaVantage vantage in sevenDays)
                avg += vantage.volume;

            return avg/7;
        }

        /// <summary>
        /// Finding highest closing price of AAPL
        /// </summary>
        /// <param name="alphas"></param>
        /// <returns></returns>
        public static decimal hiCloseAAPL(List<AlphaVantage> alphas)
        {
            DateTime sixMonths = new DateTime(2020, 3, 1);
            var aapl6Months = alphas.Where(x => (x.timestamp > sixMonths) && (x.timestamp < DateTime.Now))
                                    .Max(s => s.close);
            return aapl6Months;
        }

        /// <summary>
        /// Difference between Opening and Closing Prices of BA
        /// NOTE - I couldn't tell if by differences you meant closing - open or vice versa. But regardless the code should work fine by swapping out either or.
        /// </summary>
        /// <param name="alphas"></param>
        /// <returns></returns>
        public static List<decimal> diffOpClBA(List<AlphaVantage> alphas)
        {
            List<decimal> diffPrices = new List<decimal>();
            DateTime august = new DateTime(2020, 8, 1);

            var lastMonth = alphas.Where(x => (x.timestamp > august) && (x.timestamp < DateTime.Now));
            foreach (AlphaVantage temp in lastMonth)
                diffPrices.Add(temp.open-temp.close);

            return diffPrices;
        }
    }
}
