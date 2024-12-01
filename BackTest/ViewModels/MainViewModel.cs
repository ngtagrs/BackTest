using BackTest.Models.Data.DataClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BackTest.ViewModels
{
    public partial class MainViewModel
    {
        private Dictionary<DateTime, EquityData> _equityData;

        public MainViewModel() { }

        public Dictionary<DateTime, EquityData> EquityData
        {
            get { return _equityData; }
        }
    }
}
