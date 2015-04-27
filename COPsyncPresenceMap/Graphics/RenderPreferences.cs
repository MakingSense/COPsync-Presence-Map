using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap.Graphics
{
    public struct RenderPreferences
    {
        public readonly Color DefaultColor;
        public readonly Color LineColor;
        public readonly Color PresenceColor;
        public readonly Boolean ShowCountyLines;

        public RenderPreferences(Color defaultColor, Color lineColor, Color presenceColor, Boolean showCountyLines = true)
        {
            DefaultColor = defaultColor;
            LineColor = lineColor;
            PresenceColor = presenceColor;
            ShowCountyLines = showCountyLines;
        }

    }
}
