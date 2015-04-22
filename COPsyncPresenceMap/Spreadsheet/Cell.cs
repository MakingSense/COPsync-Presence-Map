using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COPsyncPresenceMap.Spreadsheet
{
    public struct Cell
    {
        public readonly int Column;
        public readonly int Row;
        public readonly string Value;

        public Cell(int column, int row, string value)
        {
            Column = column;
            Row = row;
            Value = value;
        }
    }
}
