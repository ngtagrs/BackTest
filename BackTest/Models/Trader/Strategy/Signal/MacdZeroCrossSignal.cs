using BackTest.Models.Trader.Strategy.Indicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Signal
{
    public class MacdZeroCrossSignal : ITradingSignal
    {
        private readonly IIndicator _macdHistogram;

        public event Action<string> SignalGenerated;

        private double _previousValue;

        public MacdZeroCrossSignal(IIndicator macdHistogram)
        {
            _macdHistogram = macdHistogram;
            _previousValue = macdHistogram.CurrentValue;
        }

        public void Update(double newValue)
        {
            _macdHistogram.Update(newValue);

            if (IsZeroCross(_previousValue, _macdHistogram.CurrentValue))
            {
                SignalGenerated?.Invoke("MACD Zero Cross detected!");
            }

            _previousValue = _macdHistogram.CurrentValue;
        }

        private bool IsZeroCross(double previous, double current)
        {
            return (previous <= 0 && current > 0) || (previous >= 0 && current < 0);
        }
    }
}
