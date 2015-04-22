using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap.Graphics
{
    public struct ColorSet
    {
        public readonly Color DefaultColor;
        public readonly Color LineColor;
        public readonly Color PresenceColor;


        public ColorSet(Color defaultColor, Color lineColor, Color presenceColor)
        {
            DefaultColor = defaultColor;
            LineColor = lineColor;
            PresenceColor = presenceColor;
        }

    }
}
