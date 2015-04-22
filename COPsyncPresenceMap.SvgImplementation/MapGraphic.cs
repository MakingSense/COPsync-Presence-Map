using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public class MapGraphic : IMapGraphic
    {
        private XmlDocument _svgXmlDocument;

        public MapGraphic(XmlDocument document)
        {
            _svgXmlDocument = document;
        }

        public XmlDocument GetSvgXmlDocument()
        {
            return _svgXmlDocument;
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
                var mapElement = _svgXmlDocument.GetElementById(id);
                mapElement.SetAttribute("fill", htmlColor);
            }
        }

        public void FillByTagName(Color color, string name)
        {
            var htmlColor = ColorTranslator.ToHtml(color);
            var list = _svgXmlDocument.GetElementsByTagName(name);
            foreach (XmlElement mapElement in list)
            {
                mapElement.SetAttribute("fill", htmlColor);
            }
        }

        public void StrokeByTagName(Color color, string name)
        {
            var htmlColor = ColorTranslator.ToHtml(color);
            var list = _svgXmlDocument.GetElementsByTagName(name);
            foreach (XmlElement mapElement in list)
            {
                mapElement.SetAttribute("stroke", htmlColor);
            }
        }

        public void SetText(string id, string text)
        {
            var mapElement = _svgXmlDocument.GetElementById(id);
            mapElement.InnerText = text;
        }

    }
}
