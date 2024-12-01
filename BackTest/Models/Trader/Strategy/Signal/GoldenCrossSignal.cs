using BackTest.Models.Trader.Strategy.Indicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Signal
{
    public class GoldenCrossSignal : ITradingSignal
    {
        private readonly IIndicator _shortTermIndicator;
        private readonly IIndicator _longTermIndicator;

        public event Action<string> SignalGenerated;

        private double _previousShortTermValue;
        private double _previousLongTermValue;

        public GoldenCrossSignal(IIndicator shortTermIndicator, IIndicator longTermIndicator)
        {
            _shortTermIndicator = shortTermIndicator;
            _longTermIndicator = longTermIndicator;

            _previousShortTermValue = shortTermIndicator.CurrentValue;
            _previousLongTermValue = longTermIndicator.CurrentValue;
        }

        public void Update(double newValue)
        {
            _shortTermIndicator.Update(newValue);
            _longTermIndicator.Update(newValue);

            if (IsGoldenCross(_previousShortTermValue, _previousLongTermValue,
                               _shortTermIndicator.CurrentValue, _longTermIndicator.CurrentValue))
            {
                SignalGenerated?.Invoke("Golden Cross detected!");
            }

            _previousShortTermValue = _shortTermIndicator.CurrentValue;
            _previousLongTermValue = _longTermIndicator.CurrentValue;
        }

        private bool IsGoldenCross(double prevShort, double prevLong, double currShort, double currLong)
        {
            return prevShort <= prevLong && currShort > currLong;
        }
    }
}
