﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace COPsyncPresenceMap.WPF.Services.Interfaces
{
    public interface IPainterService
    {
        string Process(string inputSvgFilePath, string outputFolderPath, Color color, IEnumerable<string> ids);
    }
}
