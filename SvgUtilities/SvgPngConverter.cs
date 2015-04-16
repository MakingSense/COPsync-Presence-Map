using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SvgUtilities
{
    public class SvgToPngConverter : ISvgConverter
    {
        public void Convert(XmlDocument xmlDocument, string outputFilename)
        {
            var svgDocument = SvgDocument.Open(xmlDocument);
            var image = svgDocument.Draw();
            image.Save(outputFilename);
        }
    }
}
