using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public class MapSvgConverter : IMapGraphicConverter
    {
        public MapSvgConverter()
        {
        }

        public string DefaultExtension { get { return ".svg"; } }
        public void Convert(IMapGraphic mapGraphic, string outputFilename)
        {
            var xmlDocument = mapGraphic.GetSvgXmlDocument();
            xmlDocument.Save(outputFilename);
        }

        public bool CheckAvailability()
        {
            return true;
        }
    }
}
