using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpreadsheetUtilities
{
    public class SpreadsheetRow : IEnumerable<string>
    {
        readonly Dictionary<int, string> _byColumnIndex;
        readonly SpreadsheetHeader _spreadsheetHeader;
        public int ColumnCount { get; private set; }

        public SpreadsheetRow(SpreadsheetHeader spreadsheetHeader, IEnumerable<KeyValuePair<int, string>> cells)
            :this(spreadsheetHeader, cells.ToDictionary(x => x.Key, x => x.Value))
        {
        }

        private SpreadsheetRow(SpreadsheetHeader spreadsheetHeader, Dictionary<int, string> valuesColumnIndex)
        {
            ColumnCount = spreadsheetHeader.ColumnCount;
            _byColumnIndex = valuesColumnIndex;
            _spreadsheetHeader = spreadsheetHeader;
        }

        public string this[int index]
        {
            get
            {
                if (index >= ColumnCount)
                {
                    throw new IndexOutOfRangeException();
                }
                string value;
                return _byColumnIndex.TryGetValue(index, out value) ? value : null;
            }
        }

        public string this[string columnName]
        {
            get
            {
                var index = _spreadsheetHeader[columnName];
                string value;
                return _byColumnIndex.TryGetValue(index, out value) ? value : null;
            }
        }

        public IEnumerator<string> GetEnumerator()
        {
            for (var i = 0; i < ColumnCount; i++)
            {
                yield return this[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public SpreadsheetRow CloneWithOtherHeader(SpreadsheetHeader spreadsheetHeader)
        {
            return new SpreadsheetRow(spreadsheetHeader, _byColumnIndex);
        }
    }
}
