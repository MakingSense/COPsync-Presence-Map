using System;
using COPsyncPresenceMap.WPF.Services.Interfaces;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using SvgUtilities;
using System.IO;
using SpreadsheetUtilities;
using COPsyncPresenceMap.WPF.Helpers;
using System.Linq;

namespace COPsyncPresenceMap.WPF.Services
{
    public class SpreadsheetParsingService : ISpreadsheetParsingService
    {
        public Spreadsheet Process(string inputFilePath)
        {
            try
            {
                var reader = new XlsxReader();
                var spreadsheet = reader.Read(inputFilePath).CreateNewParsingHeaders();
                return spreadsheet;
            }
            catch
            {
                throw new ApplicationException("Error parsing the file " + inputFilePath + ".\nA valid Excel Workbook file (.xlsx) is expected.");
            }
        }
    }
}
