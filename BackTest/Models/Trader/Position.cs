using BackTest.Models.Strategy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader
{
    public class Position
    {
        private ITrade _trade;

        private double _pv;
        private double _risk;
        private double _pl;
        private double _amount;

        public Position(ITrade trade_)
        {
            _trade = trade_;
        }
    }
}
