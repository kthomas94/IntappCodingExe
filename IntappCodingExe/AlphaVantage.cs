using System;
using System.Collections.Generic;
using System.Text;

namespace IntappCodingExe
{
    public class AlphaVantage: IComparable<AlphaVantage>
    {
        public DateTime timestamp;
        public decimal open;
        public decimal high;
        public decimal low;
        public decimal close;
        public decimal volume;
        public int CompareTo(AlphaVantage other) { return close.CompareTo(other.close); }

        public static AlphaVantage FromCsv(string csvLine)
        {
            string[] values = csvLine.Split(',');
            AlphaVantage dailyValues = new AlphaVantage();
            dailyValues.timestamp = Convert.ToDateTime(values[0]);
            dailyValues.open = Convert.ToDecimal(values[1]);
            dailyValues.high = Convert.ToDecimal(values[2]);
            dailyValues.low = Convert.ToDecimal(values[3]);
            dailyValues.close = Convert.ToDecimal(values[4]);
            dailyValues.volume = Convert.ToDecimal(values[5]);
            return dailyValues;
        }
    }
}
