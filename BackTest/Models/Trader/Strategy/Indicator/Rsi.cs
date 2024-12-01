using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Indicator
{
    public class Rsi : IIndicator
    {
        private readonly int _window;
        private readonly Queue<double> _gains;
        private readonly Queue<double> _losses;
        private double _currentValue;

        public Rsi(int window)
        {
            Name = "Relative Strength Index";
            _window = window;
            _gains = new Queue<double>();
            _losses = new Queue<double>();
        }

        public string Name { get; }
        public double CurrentValue => _currentValue;

        public void Update(params double[] values)
        {
            if (values.Length != 1)
                throw new ArgumentException("RSI requires exactly one input value.");

            var change = values[0];
            if (change > 0)
            {
                _gains.Enqueue(change);
                _losses.Enqueue(0);
            }
            else
            {
                _gains.Enqueue(0);
                _losses.Enqueue(-change);
            }

            if (_gains.Count > _window)
            {
                _gains.Dequeue();
                _losses.Dequeue();
            }

            var avgGain = _gains.Average();
            var avgLoss = _losses.Average();

            if (avgLoss == 0)
            {
                _currentValue = 100;
            }
            else
            {
                var rs = avgGain / avgLoss;
                _currentValue = 100 - (100 / (1 + rs));
            }
        }
    }
}
