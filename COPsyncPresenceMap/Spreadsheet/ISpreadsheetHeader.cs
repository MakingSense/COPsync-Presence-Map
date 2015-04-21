using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap.Spreadsheet
{
    public interface ISpreadsheetHeader : IEnumerable<string>
    {
        int ColumnCount { get; }

        string this[int index]  { get; }

        int this[string name] { get; }

        bool HasHeader(string name);
    }
}
