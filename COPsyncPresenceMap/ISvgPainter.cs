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
        void Fill(Color color, params string[] ids);
        void Fill(Color color, IEnumerable<string> ids);
        void FillByTagName(Color color, string name);
        void StrokeByTagName(Color color, string name);
        void SetText(string elementId, string text);
    }
}
