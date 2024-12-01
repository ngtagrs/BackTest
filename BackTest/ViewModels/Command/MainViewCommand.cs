using BackTest.Models.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace BackTest.ViewModels
{
    public partial class MainViewModel
    {
        private ICommand _testIndicatorCommand;
        public ICommand TestIndicatorCommand
        {
            get { return _testIndicatorCommand ?? (_testIndicatorCommand = new CommonCommand(TestIndicatorCommandCallback)); }
        }

        public void TestIndicatorCommandCallback()
        {
            _equityData = DataLoader.LoadHistoricalData(DataLoader.DataType.Nikke);
        }
    }
}
