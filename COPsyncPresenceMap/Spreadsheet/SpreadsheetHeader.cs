using COPsyncPresenceMap.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap.Spreadsheet
{
    public class SpreadsheetHeader : ISpreadsheetHeader
    {
        readonly Dictionary<int, string> _byIndex;
        readonly Dictionary<string, int> _byName;
        public int ColumnCount { get; private set; }

        public SpreadsheetHeader(int columCount, IEnumerable<KeyValuePair<int, string>> columnHeaders)
        {
            columnHeaders = columnHeaders.Where(x => x.Value != null);
            _byIndex = columnHeaders.ToDictionary(x => x.Key, x => x.Value);
            _byName = columnHeaders.ToDictionary(x => x.Value, x => x.Key, StringComparer.CurrentCultureIgnoreCase);
            ColumnCount = columCount;
        }

        public string this[int index]
        {
            get
            {
                if (index >= ColumnCount)
                {
                    throw new IndexOutOfRangeException();
                }
                string name;
                return _byIndex.TryGetValue(index, out name) ? name : null;
            }
        }

        public int this[string name]
        {
            get
            {
                int index;
                if (_byName.TryGetValue(name, out index))
                {
                    return index;
                }
                else
                {
                    throw new ArgumentOutOfRangeException();
                }
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

        public bool HasHeader(string name)
        {
            return _byName.ContainsKey(name);
        }
    }
}
