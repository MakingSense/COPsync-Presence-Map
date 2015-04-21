using COPsyncPresenceMap.Spreadsheet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace COPsyncPresenceMap
{
    public interface ISpreadsheetParser
    {
        ISpreadsheet Parse(string fileName);
    }
}
