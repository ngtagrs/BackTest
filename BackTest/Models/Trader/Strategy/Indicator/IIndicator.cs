using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace BackTest.Models.Trader.Strategy.Indicator
{
    public interface IIndicator
    {
        string Name { get; }
        double CurrentValue { get; }
        void Update(params double[] values);
    }
}
