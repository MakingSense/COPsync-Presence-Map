﻿using COPsyncPresenceMap.Spreadsheet;
using System;
using System.Collections.Generic;
namespace COPsyncPresenceMap
{
    public interface ICOPsyncPresenceMapGenerator
    {
        IList<string> FullProcess(string spreadsheetFileName, IEnumerable<IMapGraphicConverter> converters, string outputFolderPath, ColorSet colors, Products products);
        ISpreadsheet ParseSpreadsheet(string spreadsheetFileName);
    }
}
