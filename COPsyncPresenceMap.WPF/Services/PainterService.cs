using System;
using COPsyncPresenceMap.WPF.Services.Interfaces;
using System.Xml;
using System.Drawing;
using System.Collections.Generic;
using SvgUtilities;
using System.IO;

namespace COPsyncPresenceMap.WPF.Services
{
    public class PainterService : IPainterService
    {
        public string Process(string inputSvgFilePath, ISvgConverter converter, string outputFolderPath, Color color, IEnumerable<string> ids)
        {
            var document = SvgTextReader.GetDocumentFromFile(inputSvgFilePath);
            var painter = new SvgPainter(document, color);
            painter.Fill(ids);

            var resultPath = Path.Combine(outputFolderPath, string.Format("COPsync-presence-map-{0:yyyyMMddHHmmss}{1}", DateTime.Now, converter.DefaultExtension));

            converter.Convert(document, resultPath);
            return resultPath;
        }
    }
}
