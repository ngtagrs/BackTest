using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Signal
{
    public class ThresholdCrossSignal : ITradingSignal
    {
        private readonly double _threshold;

        public event Action<string> SignalGenerated;

        public ThresholdCrossSignal(double threshold)
        {
            _threshold = threshold;
        }

        public void Update(double newValue)
        {
            if (newValue > _threshold)
            {
                SignalGenerated?.Invoke($"Threshold Cross detected! Value: {newValue}");
            }
        }
    }
}
