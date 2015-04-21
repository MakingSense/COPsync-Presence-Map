using COPsyncPresenceMap.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace COPsyncPresenceMap
{
    public interface ISpreadsheetParsingService
    {
        ISpreadsheet Process(string inputFilePath);
    }
}
