using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaColor = System.Windows.Media.Color;
using DrawingColor = System.Drawing.Color;

namespace COPsyncPresenceMap.WPF.Helpers
{
    public static class ColorHelpers
    {
        public static DrawingColor ToDrawingColor(this MediaColor color)
        {
            return DrawingColor.FromArgb(color.A, color.R, color.G, color.B);
        }

        public static MediaColor ToMediaColor(this DrawingColor color)
        {
            return MediaColor.FromArgb(color.A, color.R, color.G, color.B);
        }

    }
}
