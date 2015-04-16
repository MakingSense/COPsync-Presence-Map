﻿using System;
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
        public string Process(string inputSvgFilePath, string outputFolderPath, Color color, IEnumerable<string> ids)
        {
            //TODO: allow to select converter and track progress
            var document = SvgTextReader.GetDocumentFromFile(inputSvgFilePath);
            var painter = new SvgPainter(document, color);
            painter.Fill(ids);

            var resultPath = Path.Combine(outputFolderPath, string.Format("COPsync-presence-map-{0:yyyyMMddHHmmss}.png", DateTime.Now));

            var converter = new SvgToPngConverter();
            converter.Convert(document, resultPath);
            return resultPath;
        }
    }
}
