using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.Models.Trader.Strategy.Signal
{
    public class DynamicSignal : ITradingSignal
    {
        private readonly List<Func<bool>> _conditions; // 条件リスト
        private readonly string _name;

        public DynamicSignal(string name)
        {
            _name = name;
            _conditions = new List<Func<bool>>();
        }

        public string Name => _name;

        public event Action<string> SignalGenerated;

        // 条件を追加
        public void AddCondition(Func<bool> condition)
        {
            _conditions.Add(condition);
        }

        // 条件を満たすかチェック
        public bool CheckSignal()
        {
            foreach (var condition in _conditions)
            {
                if (!condition.Invoke())
                {
                    return false; // 一つでも条件を満たさない場合はFalse
                }
            }
            return true; // すべての条件を満たす場合のみTrue
        }

        public void Update(double newValue)
        {
            throw new NotImplementedException();
        }
    }
}
