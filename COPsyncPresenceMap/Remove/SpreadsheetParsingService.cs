using System;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using COPsyncPresenceMap.WPF.Helpers;
using System.Linq;
using COPsyncPresenceMap.Spreadsheet;
using COPsyncPresenceMap.Utils;

namespace COPsyncPresenceMap.WPF.Services
{
    public class SpreadsheetParsingService : ISpreadsheetParsingService
    {
        readonly ISpreadsheetReader _spreadsheetReader;

        public SpreadsheetParsingService(ISpreadsheetReader spreadsheetReader)
        {
            _spreadsheetReader = spreadsheetReader;
        }

        public ISpreadsheet Process(string inputFilePath)
        {
            // Ugly patch to allow to read opened XLSX files, FileShare.Read is not enough
            using (var tfh = new TemporalFileHelper(inputFilePath))
            {
                ISpreadsheet spreadsheet;
                try
                {
                    spreadsheet = _spreadsheetReader.Read(tfh.TemporalFileName).CreateNewParsingHeaders();
                }
                catch
                {
                    throw new ApplicationException("Error parsing the file " + inputFilePath + ".\nA valid Excel Workbook file (.xlsx) is expected.");
                }

                if (!spreadsheet.HasAllRequiredColumns())
                {
                    throw new ApplicationException("Excel file format is not valid.\nIt requires the columns 'ElementId', 'COPsync Enterprise', 'COPsync911' and 'WARRANTsync'.");
                }

                return spreadsheet;
            }
        }
    }
}
