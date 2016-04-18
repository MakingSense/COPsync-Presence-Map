using COPsyncPresenceMap.Graphics;
using COPsyncPresenceMap.Spreadsheet;
using System;
using System.Collections.Generic;
namespace COPsyncPresenceMap
{
    public interface ICOPsyncPresenceMapGenerator
    {
        IList<string> FullProcess(ISpreadsheet spreadsheet, IMapGraphic svg, IEnumerable<IMapGraphicConverter> converters, string outputFolderPath, RenderPreferences colors, Products products);
        ISpreadsheet ParseSpreadsheet(string spreadsheetFileName);
        IMapGraphic ParseSvg(string svgPath);
    }
}
