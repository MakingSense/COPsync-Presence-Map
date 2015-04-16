using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SvgUtilities
{
    public class SvgTextReader : XmlTextReader
    {
        private TextReader readerToDispose = null;

        public SvgTextReader(TextReader reader)
            : base(reader)
        {
            XmlResolver = new SvgDtdResolver();
            WhitespaceHandling = WhitespaceHandling.None;
            EntityHandling = EntityHandling.ExpandEntities;
        }

        protected SvgTextReader(TextReader reader, bool readerShouldBeDisposed)
            : this(reader)
        {
            if (readerShouldBeDisposed)
            {
                readerToDispose = reader;
            }
        }

        protected override void Dispose(bool disposing)
        {
 	        base.Dispose(disposing);
            if (readerToDispose != null)
            {
                readerToDispose.Dispose();
            }
        }

        public static SvgTextReader FromFile(string path)
        {
            var textReader = new System.IO.StreamReader(path);
            var svgReader = new SvgTextReader(textReader, true);
            return svgReader;
        }

        public static SvgTextReader FromString(string source)
        {
            var textReader = new System.IO.StringReader(source);
            var svgReader = new SvgTextReader(textReader, true);
            return svgReader;
        }

        public static XmlDocument GetDocumentFromFile(string path)
        {
            using (var reader = FromFile(path))
            {
                var document = new XmlDocument();
                document.Load(reader);
                return document;
            }
        }

        public static XmlDocument GetDocumentFromString(string source)
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
