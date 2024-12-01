using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Data.DataClass
{
    public class EquityData
    {
        private DateTime _date;
        private double _close;
        private double _open;
        private double _high;
        private double _low;

        public EquityData(
            DateTime date_, 
            double close_, 
            double open_, 
            double high_, 
            double low_) 
        {
            _date = date_;
            _close = close_;
            _open = open_;  
            _high = high_;
            _low = low_;
        }

        #region getter
        public DateTime Date => _date;
        public double Close => _close;
        public double Open => _open;
        public double High => _high;
        public double Low => _low;
        #endregion

    }
}
