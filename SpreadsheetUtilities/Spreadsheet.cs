using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{
    public class Spreadsheet : IEnumerable<SpreadsheetRow>
    {
        readonly SpreadsheetHeader _spreadsheetHeader;
        readonly Dictionary<int, SpreadsheetRow> _rowsByIndex;
        readonly SpreadsheetRow _emptyRow;
        public int ColumnCount { get; private set; }
        public int RowCount { get; private set; }


        public Spreadsheet(IEnumerable<Cell> cells, IEnumerable<KeyValuePair<int, string>> columnHeaders = null)
        {
            columnHeaders = columnHeaders ?? Enumerable.Empty<KeyValuePair<int, string>>();

            //TODO: improve performance here
            RowCount = cells.Max(x => x.Row) + 1;
            ColumnCount = cells.Select(x => x.Column).Union(columnHeaders.Select(x => x.Key)).Max() + 1;

            _spreadsheetHeader = new SpreadsheetHeader(ColumnCount, columnHeaders);

            _emptyRow = new SpreadsheetRow(_spreadsheetHeader, Enumerable.Empty<KeyValuePair<int, string>>());
            _rowsByIndex = cells.GroupBy(x => x.Row)
                .ToDictionary(
                    x => x.Key,
                    x => new SpreadsheetRow(_spreadsheetHeader, x.Select(y => new KeyValuePair<int, string>(y.Column, y.Value))));
        }

        private Spreadsheet(Dictionary<int, SpreadsheetRow> rows, SpreadsheetHeader spreadsheetHeader)
        {
            ColumnCount = spreadsheetHeader.ColumnCount;
            RowCount = rows.Keys.Max() + 1;
            _spreadsheetHeader = spreadsheetHeader;
            _emptyRow = new SpreadsheetRow(_spreadsheetHeader, Enumerable.Empty<KeyValuePair<int, string>>());
            _rowsByIndex = rows;
        }

        public SpreadsheetHeader Header
        {
            get { return _spreadsheetHeader; }
        }

        public SpreadsheetRow this[int rowIndex]
        {
            get
            {
                if (rowIndex >= RowCount)
                {
                    throw new IndexOutOfRangeException();
                }
                SpreadsheetRow row;
                return _rowsByIndex.TryGetValue(rowIndex, out row) ? row : _emptyRow;
            }
        }

        public string this[int rowIndex, int colIndex]
        {
            get
            {
                return this[rowIndex][colIndex];
            }
        }

        public string this[int rowIndex, string colName]
        {
            get
            {
                return this[rowIndex][colName];
            }
        }

        public IEnumerator<SpreadsheetRow> GetEnumerator()
        {
            for (var i = 0; i < RowCount; i++)
            {
                yield return this[i];
            }
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }


        public Spreadsheet CreateNewParsingHeaders()
        {
            var newHeader = new SpreadsheetHeader(ColumnCount, this[0].Select((colName, colIndex) => new KeyValuePair<int, string>(colIndex, colName)));
            var newRows = _rowsByIndex.Where(x => x.Key > 0).ToDictionary(x => x.Key - 1, x => x.Value.CloneWithOtherHeader(newHeader));
            return new Spreadsheet(newRows, newHeader);
        }

    }
}
