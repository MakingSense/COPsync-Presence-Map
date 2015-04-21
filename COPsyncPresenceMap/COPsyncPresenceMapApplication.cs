using System;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using COPsyncPresenceMap.Helpers;
using System.Linq;
using COPsyncPresenceMap.Spreadsheet;

namespace COPsyncPresenceMap
{
    public class COPsyncPresenceMapApplication
    {
        //TODO:
        // * Implement different interfaces
        // * Provide a default constructor with default dependencies ready

        private readonly ISvgReaderFactory _svgReaderFactory;
        private readonly ISvgPainterFactory _svgPainterFactory;
        private readonly ISpreadsheetParser _spreadsheetParser;

        public const string ELEMENTID_REF_PRESENCE_TEXT = "Ref_WithPresence_text";
        public const string ELEMENTID_REF_NO_PRESENCE_TEXT = "Ref_WithoutPresence_text";
        public const string ELEMENTID_REF_PRESENCE_BOX = "Ref_WithPresence_box";
        public const string ELEMENTID_REF_NO_PRESENCE_BOX = "Ref_WithoutPresence_box";
        private const string ID_COLUMN = "ElementId";
        private const string BASE_MAP_PATH = "base-map.svg";

        public COPsyncPresenceMapApplication(ISvgPainterFactory svgPainterFactory, ISpreadsheetParser spreadsheetParser, ISvgReaderFactory svgReaderFactory)
        {
            _svgPainterFactory = svgPainterFactory;
            _spreadsheetParser = spreadsheetParser;
            _svgReaderFactory = svgReaderFactory;
        }

        public ISpreadsheet ParseSpreadsheet(string spreadsheetFileName)
        {
            // Ugly patch to allow to read opened XLSX files, FileShare.Read is not enough
            using (var tfh = new TemporalFileHelper())
            {
                ISpreadsheet spreadsheet;

                try
                {
                    File.Copy(spreadsheetFileName, tfh.TemporalFileName, true);
                    spreadsheet = _spreadsheetParser.Parse(tfh.TemporalFileName);
                }
                catch
                {
                    throw new ApplicationException(string.Format("Error reading the file {0}", spreadsheetFileName));
                }

                try
                {
                    spreadsheet = spreadsheet.CreateNewParsingHeaders();
                }
                catch
                {
                    throw new ApplicationException(string.Format("Error parsing the file {0}.\nA valid Excel Workbook file (.xlsx) is expected.", spreadsheetFileName));
                }

                var requiredColumns = new[] { ID_COLUMN }.Union(Products.AllProducts.ProductNames).ToArray();
                if (!spreadsheet.HasAllColumns(requiredColumns))
                {
                    var requiredColumnsAsText = string.Join(", ", requiredColumns.Select(x => string.Format("\"{0}\"")));
                    throw new ApplicationException(string.Format("Excel file format is not valid.\nIt requires the columns {0}.", requiredColumnsAsText));
                }

                return spreadsheet;
            }
        }

        public IList<string> FullProcess(string spreadsheetFileName, IEnumerable<ISvgConverter> converters, string outputFolderPath, Color color, Products products)
        {
            var spreadsheet = ParseSpreadsheet(spreadsheetFileName);

            var countyIds = spreadsheet
                .Where(x => x.HasTruthyValue(products))
                .Select(x => x[ID_COLUMN]);

            XmlDocument document;

            try
            {
                document = _svgReaderFactory.GetDocumentFromFile(BASE_MAP_PATH);
            }
            catch
            {
                throw new ApplicationException("Error opening " + BASE_MAP_PATH + ".\nA valid SVG file is expected.");
            }

            var painter = _svgPainterFactory.Create(document, color);

            try
            {
                painter.Fill(countyIds);
                painter.Fill(ELEMENTID_REF_PRESENCE_BOX);
                painter.SetText(ELEMENTID_REF_PRESENCE_TEXT, products.GetWithPresenceText());
                painter.SetText(ELEMENTID_REF_NO_PRESENCE_TEXT, products.GetWithoutPresenceText());
            }
            catch
            {
                throw new ApplicationException("Error applying color to elements, verify elementIds and base-map.svg file.");
            }

            var resultFileNames = new List<string>();
            var baseFilename = Path.Combine(outputFolderPath, string.Format("COPsync-presence-map-{0:yyyyMMddHHmmss}", DateTime.Now));

            foreach (var converter in converters)
            {
                try
                {
                    var convertedFilename = Path.ChangeExtension(baseFilename, converter.DefaultExtension);
                    converter.Convert(document, convertedFilename);
                    resultFileNames.Add(convertedFilename);
                }
                catch (Exception e)
                {
                    throw new ApplicationException(string.Format("Error converting the file using {0}:\n{1}", converter.GetType(), e.Message));
                }
            }
            return resultFileNames;
        }

    }
}
