using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Indicator
{
    public class BollingerBands : IIndicator
    {
        private readonly Sma _sma;
        private readonly int _window;
        private readonly Queue<double> _values;
        private double _upperBand;
        private double _lowerBand;

        public BollingerBands(int window)
        {
            Name = "Bollinger Bands";
            _window = window;
            _values = new Queue<double>();
            _sma = new Sma(window);
        }

        public string Name { get; }
        public double CurrentValue => _sma.CurrentValue;

        public double UpperBand => _upperBand;
        public double LowerBand => _lowerBand;

        public void Update(params double[] values)
        {
            if (values.Length != 1)
                throw new ArgumentException("Bollinger Bands requires exactly one input value.");

            _values.Enqueue(values[0]);
            if (_values.Count > _window)
            {
                _values.Dequeue();
            }

            _sma.Update(values[0]);
            var mean = _sma.CurrentValue;
            var stdDev = Math.Sqrt(_values.Select(v => Math.Pow(v - mean, 2)).Average());

            _upperBand = mean + 2 * stdDev;
            _lowerBand = mean - 2 * stdDev;
        }
    }
}
