using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Indicator
{
    public class Macd : IIndicator
    {
        private readonly Ema _fastEma;
        private readonly Ema _slowEma;
        private readonly Ema _signalEma;

        private double _macdValue;
        private double _signalLine;
        private double _histogram;

        public Macd(int fastWindow, int slowWindow, int signalWindow)
        {
            Name = "MACD";
            _fastEma = new Ema(fastWindow);
            _slowEma = new Ema(slowWindow);
            _signalEma = new Ema(signalWindow);
        }

        public string Name { get; }
        public double CurrentValue => _histogram; // MACDヒストグラムを現在値として返す

        public double MacdValue => _macdValue;
        public double SignalLine => _signalLine;

        public void Update(params double[] values)
        {
            if (values.Length != 1)
                throw new ArgumentException("MACD requires exactly one input value.");

            _fastEma.Update(values[0]);
            _slowEma.Update(values[0]);

            _macdValue = _fastEma.CurrentValue - _slowEma.CurrentValue;
            _signalEma.Update(_macdValue);

            _signalLine = _signalEma.CurrentValue;
            _histogram = _macdValue - _signalLine;
        }
    }
}
