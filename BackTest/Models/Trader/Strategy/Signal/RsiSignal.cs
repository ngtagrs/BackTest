using BackTest.Models.Trader.Strategy.Indicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Signal
{
    public class RsiSignal : ITradingSignal
    {
        private readonly IIndicator _rsiIndicator;

        public event Action<string> SignalGenerated;

        public RsiSignal(IIndicator rsiIndicator)
        {
            _rsiIndicator = rsiIndicator;
        }

        public void Update(double newValue)
        {
            _rsiIndicator.Update(newValue);

            if (_rsiIndicator.CurrentValue > 70)
            {
                SignalGenerated?.Invoke("RSI overbought detected!");
            }
            else if (_rsiIndicator.CurrentValue < 30)
            {
                SignalGenerated?.Invoke("RSI oversold detected!");
            }
        }
    }
}
