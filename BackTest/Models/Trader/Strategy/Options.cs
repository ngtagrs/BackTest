using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Strategy
{
    public enum Category
    {
        None,
        EQ,
        FX,
        IR,
        Bond,
        Credit,
    }

    public enum EqTradeType
    {
        None,
        Cash,
        Future,
        Option,
    }

    public enum FxTradeType
    {
        None,
        Cash,
        Future,
        Option,
    }

    public enum IrTradeType
    {
        None,
        Swap,
        Swaption,
    }

    public enum OptionType
    {
        None,
        European,
        American,
    }

    public enum OptionSubType
    {
        None,
        Call,
        Put,
    }


}
