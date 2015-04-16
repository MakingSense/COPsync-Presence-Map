using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace SpreadsheetUtilities
{
    public class XlsxReader
    {
        private static class ExcelNamespaces
        {
            public static XNamespace Main = XNamespace.Get("http://schemas.openxmlformats.org/spreadsheetml/2006/main");
            public static XNamespace Relationships = XNamespace.Get("http://schemas.openxmlformats.org/officeDocument/2006/relationships");
        }

        /// <summary>
        /// Run to read file from Open File Dialog as an XLSX file
        /// </summary>
        public Spreadsheet Read(string fileName)
        {
            Package xlsxPackage = Package.Open(fileName, FileMode.Open, FileAccess.Read);
            List<Cell> parsedCells = new List<Cell>();
            try
            {
                PackagePartCollection allParts = xlsxPackage.GetParts();
                PackagePart sharedStringsPart = allParts.Where(x => x.ContentType == "application/vnd.openxmlformats-officedocument.spreadsheetml.sharedStrings+xml").Single();
                XElement sharedStringsElement = XElement.Load(XmlReader.Create(sharedStringsPart.GetStream()));

                Dictionary<int, string> sharedStrings = new Dictionary<int, string>();
                parseSharedStrings(sharedStringsElement, sharedStrings);

                XElement worksheetElement = getWorksheet(1, allParts);

                IEnumerable<XElement> cells = worksheetElement.Descendants(ExcelNamespaces.Main + "c");

                foreach (XElement cell in cells)
                {
                    string cellPosition = cell.Attribute("r").Value;
                    int index = indexOfNumber(cellPosition);
                    int column = getColumnNumber(cellPosition.Substring(0, index));
                    int row = Convert.ToInt32(cellPosition.Substring(index, cellPosition.Length - index));
                    if (cell.HasElements)
                    {
                        if (cell.Attribute("t") != null && cell.Attribute("t").Value == "s")
                        {
                            int valueIndex = Convert.ToInt32(cell.Descendants(ExcelNamespaces.Main + "v").Single().Value);
                            parsedCells.Add(new Cell(column - 1, row - 1, sharedStrings[valueIndex]));
                        }
                        else
                        {
                            string value = cell.Descendants(ExcelNamespaces.Main + "v").Single().Value;
                            parsedCells.Add(new Cell(column - 1, row - 1, value));
                        }
                    }
                }
                return new Spreadsheet(parsedCells);
            }
            finally
            {
                xlsxPackage.Close();
            }
        }

        /// <summary>
        /// Gets a numerical column index from an Excel alphabetic one
        /// </summary>
        /// <param name="name">The Excel column name. Ex: A, B, C,... AA, AB, AC</param>
        /// <returns>The index integer value of the column</returns>
        public int getColumnNumber(string name)
        {
            int number = 0;
            int pow = 1;
            for (int i = name.Length - 1; i >= 0; i--)
            {
                number += (name[i] - 'A' + 1) * pow;
                pow *= 26;
            }
            return number;
        }

        private void parseSharedStrings(XElement SharedStringsElement, Dictionary<int, string> sharedStrings)
        {
            IEnumerable<XElement> sharedStringsElements =
                from s in SharedStringsElement.Descendants(ExcelNamespaces.Main + "t")
                select s;

            int Counter = 0;
            foreach (XElement sharedString in sharedStringsElements)
            {
                sharedStrings.Add(Counter, sharedString.Value);
                Counter++;
            }
        }

        private XElement getWorksheet(int worksheetID, PackagePartCollection allParts)
        {
            var worksheetName = String.Format("/xl/worksheets/sheet{0}.xml", worksheetID);
            PackagePart worksheetPart = allParts.Where(x => x.Uri.OriginalString == worksheetName).Single();

            return XElement.Load(XmlReader.Create(worksheetPart.GetStream()));
        }

        private int indexOfNumber(string value)
        {
            for (int counter = 0; counter < value.Length; counter++)
            {
                if (Char.IsNumber(value[counter]))
                {
                    return counter;
                }
            }
            return 0;
        }

    }
}
