using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Indicator
{
    public class Ema : IIndicator
    {
        private readonly int _window;
        private readonly double _multiplier;
        private double? _currentValue;

        public Ema(int window)
        {
            Name = "Exponential Moving Average";
            _window = window;
            _multiplier = 2.0 / (window + 1);
        }

        public string Name { get; }
        public double CurrentValue => _currentValue ?? 0;

        public void Update(params double[] values)
        {
            if (values.Length != 1)
                throw new ArgumentException("EMA requires exactly one input value.");

            if (_currentValue == null)
            {
                _currentValue = values[0]; // 初期化
            }
            else
            {
                _currentValue = (values[0] - _currentValue.Value) * _multiplier + _currentValue.Value;
            }
        }
    }
}
