using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public class SvgToSvgConverter : ISvgConverter
    {
        public SvgToSvgConverter()
        {
        }

        public string DefaultExtension { get { return ".svg"; } }
        public void Convert(XmlDocument xmlDocument, string outputFilename)
        {
            xmlDocument.Save(outputFilename);
        }

        public bool CheckAvailability()
        {
            return true;
        }
    }
}
