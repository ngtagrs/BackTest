using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Indicator
{
    public class Sma : IIndicator
    {
        private readonly int _window;
        private readonly Queue<double> _values;
        private double _currentValue;

        public Sma(int window)
        {
            Name = "Simple Moving Average";
            _window = window;
            _values = new Queue<double>();
            _currentValue = 0;
        }


        public void Update(params double[] values)
        {
            if (values.Length != 1)
                throw new ArgumentException("SMA requires exactly one input value.");

            _values.Enqueue(values[0]);
            if (_values.Count > _window)
            {
                _values.Dequeue();
            }
            _currentValue = _values.Average();
        }


        public string Name { get; }
        public double CurrentValue => _currentValue;
        public string[] Params => new string[] { "Window" };
    }
}
