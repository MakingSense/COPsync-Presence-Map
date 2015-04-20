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
        public float Scale { get; set; }

        public SvgToPngConverter(int scale = 1)
        {
            Scale = scale;
        }

        public string DefaultExtension { get { return ".png"; } }
        public void Convert(XmlDocument xmlDocument, string outputFilename)
        {
            var svgDocument = SvgDocument.Open(xmlDocument);
            var image = new System.Drawing.Bitmap((int)(svgDocument.Width.Value * Scale), (int)(svgDocument.Height.Value * Scale));
            svgDocument.Draw(image);
            image.Save(outputFilename);
        }

        public bool CheckAvailability()
        {
            return true;
        }
    }
}
