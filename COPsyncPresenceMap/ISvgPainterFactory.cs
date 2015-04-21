using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap
{
    public interface ISvgPainterFactory
    {
        ISvgPainter Create(XmlDocument document, Color defaultColor);
    }
}
