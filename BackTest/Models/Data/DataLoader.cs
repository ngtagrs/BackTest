using BackTest.Models.Data.DataClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Data
{
    public class DataLoader
    {
        public enum DataType
        {
            Nikke,
            NikkeiVi,
            Jpx400,
        }

        public static Dictionary<DateTime, EquityData> LoadHistoricalData(DataType type_)
        {
            switch(type_) 
            {
                case DataType.Nikke:
                    return ReadCsvFile("./Models/Data/CSV/nikkei_stock_average_daily_jp.csv");
                case DataType.NikkeiVi:
                    return ReadCsvFile("./Models/Data/CSV/nikkei_stock_average_vi_daily_jp.csv");
                case DataType.Jpx400:
                    return ReadCsvFile("./Models/Data/CSV/jpx_nikkei_index_400_daily_jp.csv");
                default:
                    throw new NotImplementedException();
            }
        }

        private static Dictionary<DateTime, EquityData> ReadCsvFile(string path_)
        {
            var result = new Dictionary<DateTime, EquityData>();
            var lines = File.ReadAllLines(path_);
            for(int i=1;i<lines.Length-1; i++)
            {
                var splitLine = lines[i].Replace("\"", "").Split(",");
                var date = DateTime.Parse(splitLine[0]);
                var close = double.Parse(splitLine[1]);
                var open = double.Parse(splitLine[2]);
                var high = double.Parse(splitLine[3]);
                var low = double.Parse(splitLine[4]);
                result.Add(date, new EquityData(date, close, open, high, low));
            }
            return result;
        }
    }
}
