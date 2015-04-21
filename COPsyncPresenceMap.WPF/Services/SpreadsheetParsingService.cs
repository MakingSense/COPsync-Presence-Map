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
            // Ugly patch to allow to read opened XLSX files, FileShare.Read is not enough
            using (var tfh = new TemporalFileHelper(inputFilePath))
            {
                Spreadsheet spreadsheet;
                try
                {
                    var reader = new XlsxReader();
                    spreadsheet = reader.Read(tfh.TemporalFileName).CreateNewParsingHeaders();
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
