using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COPsyncPresenceMap.Spreadsheet
{
    public interface ISpreadsheet : IEnumerable<ISpreadsheetRow>
    {
        int ColumnCount { get; }
        int RowCount { get; }

        ISpreadsheetHeader Header { get; }

        ISpreadsheetRow this[int rowIndex] { get; }

        string this[int rowIndex, int colIndex] { get; }

        string this[int rowIndex, string colName] { get; }

        ISpreadsheet CreateNewParsingHeaders();

    }
}
