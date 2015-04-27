using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.Graphics
{
    public interface IMapGraphic
    {
        XmlDocument GetSvgXmlDocument();
        void Fill(Color color, params string[] ids);
        void Fill(Color color, IEnumerable<string> ids);
        void FillCounties(Color color);
        void FillReferenceBoxes(Color color);
        void Stroke(Color color, params string[] ids);
        void Stroke(Color color, IEnumerable<string> ids);
        void StrokeCounties(Color color);
        void StrokeReferenceBoxes(Color color);
        void StrokeOuterBorder(Color color);
        void SetText(string elementId, string text);
        void ShowCountyNames(bool show = true);
        void HideCountyNames();
    }
}
