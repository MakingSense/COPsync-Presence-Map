using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace COPsyncPresenceMap.WPF.Services.Interfaces
{
    public interface ISpreadsheetParsingService
    {
        Spreadsheet Process(string inputFilePath);
    }
}
