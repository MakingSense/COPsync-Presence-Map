using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public class SvgReaderFactory : ISvgReaderFactory
    {
        public XmlTextReader FromFile(string path)
        {
            var textReader = new System.IO.StreamReader(path);
            var svgReader = new SvgTextReader(textReader, true);
            return svgReader;
        }

        public XmlTextReader FromString(string source)
        {
            var textReader = new System.IO.StringReader(source);
            var svgReader = new SvgTextReader(textReader, true);
            return svgReader;
        }

        public XmlDocument GetDocumentFromFile(string path)
        {
            using (var reader = FromFile(path))
            {
                var document = new XmlDocument();
                document.Load(reader);
                return document;
            }
        }

        public XmlDocument GetDocumentFromString(string source)
        {
            using (var reader = FromString(source))
            {
                var document = new XmlDocument();
                document.Load(reader);
                return document;
            }
        }
    }
}
