using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpreadsheetUtilities
{
    public class Cell
    {
        public int Column { get; private set; }
        public int Row { get; private set; }
        public string Value { get; private set; }

        public Cell(int column, int row, string value)
        {
            Column = column;
            Row = row;
            Value = value;
        }
    }
}
