using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Threading;

namespace DAL.Utils
{
    public class SearchManager<T>
    {
        private readonly DispatcherTimer _timer;
        private readonly IEnumerable<T> _source;
        private readonly Action<IEnumerable<T>> _updateResults;
        private readonly Func<T, string, bool> _filterLogic;

        private int Delay { get; set; } = 500; // Thời gian chờ mặc định (ms)

        public SearchManager(IEnumerable<T> source, Action<IEnumerable<T>> updateResults, Func<T, string, bool> filterLogic)
        {
            _source = source;
            _updateResults = updateResults;
            _filterLogic = filterLogic;

            _timer = new DispatcherTimer();
            _timer.Tick += OnTimerElapsed;
            _timer.Interval = TimeSpan.FromMilliseconds(Delay);
        }

        private string _searchText = string.Empty;

        public void OnSearchTextChanged(string searchText)
        {
            _searchText = searchText.ToLower();

            if (_timer.IsEnabled)
                _timer.Stop();

            _timer.Start();
        }

        private void OnTimerElapsed(object? sender, EventArgs e)
        {
            _timer.Stop();

            // Lọc dữ liệu dựa trên logic filter
            var results = _source.Where(item => _filterLogic(item, _searchText)).ToList();

            // Cập nhật kết quả
            _updateResults(results);
        }
    }
}
