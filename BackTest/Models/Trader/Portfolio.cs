using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader
{
    public class Portfolio
    {
        private List<Position> _positions;
        private double _pv;
        private double _risk;
        private double _pl;

        public Portfolio()
        {
            _positions = new List<Position>();
        }

        public void AddPosition(Position position)
        {
            _positions.Add(position);
        }
    }
}
