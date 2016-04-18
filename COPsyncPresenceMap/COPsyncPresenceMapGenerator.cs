using System;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using System.IO;
using COPsyncPresenceMap.Helpers;
using System.Linq;
using COPsyncPresenceMap.Spreadsheet;
using COPsyncPresenceMap.Graphics;

namespace COPsyncPresenceMap
{
    public class COPsyncPresenceMapGenerator : ICOPsyncPresenceMapGenerator
    {
        private readonly IMapGraphicParser _svgReaderFactory;
        private readonly ISpreadsheetParser _spreadsheetParser;

        public const string ELEMENTID_REF_PRESENCE_TEXT = "Ref_WithPresence_text";
        public const string ELEMENTID_REF_NO_PRESENCE_TEXT = "Ref_WithoutPresence_text";
        public const string ELEMENTID_REF_PRESENCE_BOX = "Ref_WithPresence_box";
        public const string ELEMENTID_REF_NO_PRESENCE_BOX = "Ref_WithoutPresence_box";
        private const string ID_COLUMN = "ElementId";

        public COPsyncPresenceMapGenerator(ISpreadsheetParser spreadsheetParser, IMapGraphicParser svgReaderFactory)
        {
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
                    spreadsheet = _spreadsheetParser.ParseFromFile(tfh.TemporalFileName);
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

        public IMapGraphic ParseSvg(string svgPath)
        {
            try
            {
                return _svgReaderFactory.ParseFromFile(svgPath);
            }
            catch
            {
                throw new ApplicationException("Error opening " + svgPath + ".\nA valid SVG file is expected.");
            }
        }

        public IList<string> FullProcess(ISpreadsheet spreadsheet, IMapGraphic mapGraphic, IEnumerable<IMapGraphicConverter> converters, string outputFolderPath, RenderPreferences preferences, Products products)
        {
            var countyIds = spreadsheet
                .Where(x => x.HasTruthyValue(products))
                .Select(x => x[ID_COLUMN]);

            try
            {
                mapGraphic.FillReferenceBoxes(preferences.DefaultColor);
                mapGraphic.FillCounties(preferences.DefaultColor);
                mapGraphic.StrokeReferenceBoxes(preferences.LineColor);
                mapGraphic.StrokeOuterBorder(preferences.LineColor);
                mapGraphic.Fill(preferences.PresenceColor, countyIds);
                mapGraphic.Fill(preferences.PresenceColor, ELEMENTID_REF_PRESENCE_BOX);
                if (!preferences.ShowCountyNames)
                {
                    mapGraphic.HideCountyNames();
                }
                if (preferences.ShowCountyLines)
                {
                    mapGraphic.StrokeCounties(preferences.LineColor);
                }
                else
                {
                    mapGraphic.ReduceStrokeCounties();
                    mapGraphic.StrokeCounties(preferences.DefaultColor);
                    mapGraphic.Stroke(preferences.PresenceColor, countyIds);
                }
                mapGraphic.SetText(ELEMENTID_REF_PRESENCE_TEXT, products.GetWithPresenceText());
                mapGraphic.SetText(ELEMENTID_REF_NO_PRESENCE_TEXT, products.GetWithoutPresenceText());
            }
            catch
            {
                throw new ApplicationException("Error applying color to elements, verify elementIds and SVG file.");
            }

            var resultFileNames = new List<string>();
            var baseFilename = Path.Combine(outputFolderPath, string.Format("COPsync-presence-map-{0:yyyyMMddHHmmss}", DateTime.Now));

            foreach (var converter in converters)
            {
                try
                {
                    var convertedFilename = Path.ChangeExtension(baseFilename, converter.DefaultExtension);
                    converter.Convert(mapGraphic, convertedFilename);
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
