using BackTest.Models.Data;
using BackTest.Models.Trader.Strategy.Indicator;
using BackTest.Models.Trader.Strategy.Signal;
using IronPython.Hosting;
using ScottPlot;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace BackTest
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private List<IIndicator> _indicators;
        private DynamicSignal _currentSignal;
        private readonly List<Condition> _conditions; // 条件リスト

        public MainWindow()
        {
            InitializeComponent();

            // Initialize Indicators (Mock Example)
            _indicators = new List<IIndicator>
            {
                new Sma(10),
                new Sma(20),
                new Ema(15)
            };

            IndicatorListBox.ItemsSource = _indicators.Select(i => i.Name);
            IndicatorSelector.ItemsSource = _indicators.Select(i => i.Name);

            _currentSignal = new DynamicSignal("Custom Signal");
            _conditions = new List<Condition>();
        }

        private void SaveSignalButton_Click(object sender, RoutedEventArgs e)
        {
            var json = JsonSerializer.Serialize(_conditions);
            File.WriteAllText("signal.json", json);
            MessageBox.Show("Signal saved to signal.json");
        }

        private void LoadSignalButton_Click(object sender, RoutedEventArgs e)
        {
            if (File.Exists("signal.json"))
            {
                var json = File.ReadAllText("signal.json");
                var loadedConditions = JsonSerializer.Deserialize<List<Condition>>(json);
                _conditions.Clear();
                _conditions.AddRange(loadedConditions);

                // UI更新
                ConditionListBox.Items.Clear();
                foreach (var condition in _conditions)
                {
                    ConditionListBox.Items.Add($"{condition.IndicatorName} {condition.Operator} {condition.Value} ({condition.LogicOperator})");
                }

                MessageBox.Show("Signal loaded from signal.json");
            }
            else
            {
                MessageBox.Show("No saved signal found.");
            }
        }

        private void LoadMarketData_Click(object sender, RoutedEventArgs e)
        {
            // サンプルデータ生成 (価格データ)
            var random = new Random();
            var prices = Enumerable.Range(0, 100).Select(_ => random.NextDouble() * 100).ToList();

            // 各Indicatorを更新
            foreach (var price in prices)
            {
                foreach (var indicator in _indicators.OfType<IIndicator>())
                {
                    indicator.Update(price);
                }
            }

            MessageBox.Show("Market data loaded and indicators updated.");
        }

        private void AddConditionButton_Click(object sender, RoutedEventArgs e)
        {
            var selectedIndicatorName = IndicatorSelector.SelectedItem as string;
            var selectedOperator = OperatorSelector.SelectedItem as ComboBoxItem;
            var selectedLogicOperator = LogicSelector.SelectedItem as ComboBoxItem;
            var valueText = ValueInput.Text;

            if (selectedIndicatorName == null || selectedOperator == null || selectedLogicOperator == null || string.IsNullOrWhiteSpace(valueText))
            {
                MessageBox.Show("Please complete all fields to add a condition.");
                return;
            }

            if (!double.TryParse(valueText, out var targetValue))
            {
                MessageBox.Show("Please enter a valid numeric value.");
                return;
            }

            var condition = new Condition
            {
                IndicatorName = selectedIndicatorName,
                Operator = selectedOperator.Content.ToString(),
                Value = targetValue,
                LogicOperator = selectedLogicOperator.Content.ToString()
            };

            _conditions.Add(condition);
            ConditionListBox.Items.Add($"{condition.IndicatorName} {condition.Operator} {condition.Value} ({condition.LogicOperator})");
        }

        // 条件クラス
        public class Condition
        {
            public string IndicatorName { get; set; }
            public string Operator { get; set; }
            public double Value { get; set; }
            public string LogicOperator { get; set; }
        }

        private void ModeSwitch_Checked(object sender, RoutedEventArgs e)
        {
            if (GuiModePanel == null) return;

            if (GuiModeRadio.IsChecked == true)
            {
                GuiModePanel.Visibility = Visibility.Visible;
                PythonModePanel.Visibility = Visibility.Collapsed;
            }
            else
            {
                GuiModePanel.Visibility = Visibility.Collapsed;
                PythonModePanel.Visibility = Visibility.Visible;
            }
        }

        // Pythonモード: スクリプト検証
        private void ValidatePythonScriptButton_Click(object sender, RoutedEventArgs e)
        {
            var script = PythonScriptInput.Text;

            // サンプル: Pythonスクリプトのバリデーション
            if (string.IsNullOrWhiteSpace(script))
            {
                PythonScriptResult.Text = "Script Result: Invalid (empty script)";
                PythonScriptResult.Foreground = System.Windows.Media.Brushes.Red;
                PythonScriptResult.Visibility = Visibility.Visible;
                return;
            }

            try
            {
                // 実際のPythonバリデーション（例: IronPythonを使用）
                // var result = ValidatePython(script);
                PythonScriptResult.Text = "Script Result: OK";
                PythonScriptResult.Foreground = System.Windows.Media.Brushes.Green;
            }
            catch (Exception ex)
            {
                PythonScriptResult.Text = $"Script Result: Invalid ({ex.Message})";
                PythonScriptResult.Foreground = System.Windows.Media.Brushes.Red;
            }

            PythonScriptResult.Visibility = Visibility.Visible;
        }

        private bool ValidatePython(string script)
        {
            var engine = Python.CreateEngine();
            try
            {
                engine.CreateScriptSourceFromString(script).Compile();
                return true;
            }
            catch
            {
                return false;
            }
        }

        private void RemoveConditionButton_Click(object sender, RoutedEventArgs e)
        {

        }

    }
} 