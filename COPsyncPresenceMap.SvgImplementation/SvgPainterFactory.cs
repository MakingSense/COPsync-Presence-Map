
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public class SvgPainterFactory : ISvgPainterFactory
    {
        public ISvgPainter Create(XmlDocument document, Color defaultColor)
        {
            return new SvgPainter(document, defaultColor);
        }
    }
}
