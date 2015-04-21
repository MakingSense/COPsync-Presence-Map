using System;
using COPsyncPresenceMap.WPF.Services.Interfaces;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using SvgUtilities;
using System.IO;
using COPsyncPresenceMap.WPF.Helpers;

namespace COPsyncPresenceMap.WPF.Services
{
    public class PainterService : IPainterService
    {
        public string Process(string inputSvgFilePath, ISvgConverter converter, string outputFolderPath, Color color, IEnumerable<string> ids, string textWithPresence, string textWithoutPresence)
        {
            XmlDocument document;

            try
            {
                document = SvgTextReader.GetDocumentFromFile(inputSvgFilePath);
            }
            catch
            {
                throw new ApplicationException("Error opening " + inputSvgFilePath + ".\nA valid SVG file is expected.");
            }

            var painter = new SvgPainter(document, color);

            try
            {
                painter.Fill(ids);
                painter.Fill(PresenceSpreadsheetHelpers.ELEMENTID_REF_PRESENCE_BOX);
                painter.SetText(PresenceSpreadsheetHelpers.ELEMENTID_REF_PRESENCE_TEXT, textWithPresence);
                painter.SetText(PresenceSpreadsheetHelpers.ELEMENTID_REF_NO_PRESENCE_TEXT, textWithoutPresence);
            }
            catch
            {
                throw new ApplicationException("Error applying color to elements, verify elementIds and base-map.svg file.");
            }

            try
            {
                var baseFilename = Path.Combine(outputFolderPath, string.Format("COPsync-presence-map-{0:yyyyMMddHHmmss}", DateTime.Now));
                var convertedFilename = Path.ChangeExtension(baseFilename, converter.DefaultExtension);
                var svgFilename = Path.ChangeExtension(baseFilename, ".svg");

                converter.Convert(document, convertedFilename);
                document.Save(svgFilename);
                return convertedFilename;
            }
            catch (Exception e)
            {
                throw new ApplicationException("Error converting the file: \n" + e.Message);
            }

        }
    }
}
