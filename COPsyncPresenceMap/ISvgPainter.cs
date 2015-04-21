using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap
{
    public interface ISvgPainter
    {
        XmlDocument Document { get; }
        Color DefaultColor { get; }
        void Fill(params string[] ids);
        void Fill(Color color, params string[] ids);
        void Fill(IEnumerable<string> ids);
        void Fill(Color color, IEnumerable<string> ids);
        void SetText(string elementId, string text);
    }
}
