using BackTest.Models.Trader.Strategy.Indicator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Signal
{
    public interface ITradingSignal
    {
        event Action<string> SignalGenerated;
        void Update(double newValue);
    }
}
