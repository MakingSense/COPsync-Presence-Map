using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap.Spreadsheet
{
    public interface ISpreadsheetRow : IEnumerable<string>
    {
        int ColumnCount { get; }

        string this[int index] { get; }

        string this[string columnName] { get; }

        ISpreadsheetRow CloneWithOtherHeader(ISpreadsheetHeader spreadsheetHeader);
    }
}
