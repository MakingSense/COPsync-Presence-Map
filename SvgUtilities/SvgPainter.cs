using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SvgUtilities
{
    public class SvgPainter
    {
        public XmlDocument Document { get; private set; }
        public Color DefaultColor { get; set; }

        public SvgPainter(XmlDocument document, Color defaultColor)
        {
            Document = document;
            DefaultColor = defaultColor;
        }

        public void Fill(params string[] ids)
        {
            Fill(DefaultColor, ids);
        }

        public void Fill(Color color, params string[] ids)
        {
            Fill(color, ids.AsEnumerable());
        }

        public void Fill(IEnumerable<string> ids)
        {
            Fill(DefaultColor, ids.AsEnumerable());
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

    }
}
