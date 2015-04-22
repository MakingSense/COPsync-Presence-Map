using COPsyncPresenceMap.Graphics;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public class SvgMapGraphicParser : IMapGraphicParser
    {
        private class SvgDtdResolver : XmlUrlResolver
        {
            public override object GetEntity(Uri absoluteUri, string role, Type ofObjectToReturn)
            {
                if (absoluteUri.ToString().IndexOf("svg", StringComparison.InvariantCultureIgnoreCase) > -1)
                {
                    return Assembly.GetExecutingAssembly().GetManifestResourceStream("COPsyncPresenceMap.SvgImplementation.Resources.svg11.dtd");
                }
                else
                {
                    return base.GetEntity(absoluteUri, role, ofObjectToReturn);
                }
            }
        }

        private XmlTextReader CreateXmlReader(TextReader textReader)
        {
            var svgReader = new XmlTextReader(textReader);
            svgReader.XmlResolver = new SvgDtdResolver();
            svgReader.WhitespaceHandling = WhitespaceHandling.None;
            svgReader.EntityHandling = EntityHandling.ExpandEntities;
            return svgReader;
        }

        public IMapGraphic ParseMapFromFile(string path)
        {
            using (var textReader = new StreamReader(path))
            using (var xmlReader = CreateXmlReader(textReader))
            {
                var document = new XmlDocument();
                document.Load(xmlReader);
                return new MapGraphic(document);
            }
        }

        public IMapGraphic ParseMapFromString(string source)
        {
            using (var textReader = new StringReader(source))
            using (var xmlReader = CreateXmlReader(textReader))
            {
                var document = new XmlDocument();
                document.Load(xmlReader);
                return new MapGraphic(document);
            }
        }
    }
}
