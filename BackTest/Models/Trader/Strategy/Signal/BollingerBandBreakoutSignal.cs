using BackTest.Models.Trader.Strategy.Indicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Signal
{
    public class BollingerBandBreakoutSignal : ITradingSignal
    {
        private readonly IIndicator _upperBand;
        private readonly IIndicator _lowerBand;

        public event Action<string> SignalGenerated;

        public BollingerBandBreakoutSignal(IIndicator upperBand, IIndicator lowerBand)
        {
            _upperBand = upperBand;
            _lowerBand = lowerBand;
        }

        public void Update(double newValue)
        {
            if (newValue > _upperBand.CurrentValue)
            {
                SignalGenerated?.Invoke("Bollinger Band Upper Breakout detected!");
            }
            else if (newValue < _lowerBand.CurrentValue)
            {
                SignalGenerated?.Invoke("Bollinger Band Lower Breakout detected!");
            }
        }
    }
}
