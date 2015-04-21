using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public class SvgPainter : ISvgPainter
    {
        public XmlDocument Document { get; private set; }

        public SvgPainter(XmlDocument document)
        {
            Document = document;
        }

        public void Fill(Color color, params string[] ids)
        {
            Fill(color, ids.AsEnumerable());
        }

        public void Fill(Color color, IEnumerable<string> ids)
        {
            var htmlColor = ColorTranslator.ToHtml(color);
            foreach (var id in ids)
            {
                var mapElement = Document.GetElementById(id);
                mapElement.SetAttribute("fill", htmlColor);
            }
        }

        public void FillByTagName(Color color, string name)
        {
            var htmlColor = ColorTranslator.ToHtml(color);
            var list = Document.GetElementsByTagName(name);
            foreach (XmlElement mapElement in list)
            {
                mapElement.SetAttribute("fill", htmlColor);
            }
        }

        public void StrokeByTagName(Color color, string name)
        {
            var htmlColor = ColorTranslator.ToHtml(color);
            var list = Document.GetElementsByTagName(name);
            foreach (XmlElement mapElement in list)
            {
                mapElement.SetAttribute("stroke", htmlColor);
            }
        }

        public void SetText(string id, string text)
        {
            var mapElement = Document.GetElementById(id);
            mapElement.InnerText = text;
        }

    }
}
