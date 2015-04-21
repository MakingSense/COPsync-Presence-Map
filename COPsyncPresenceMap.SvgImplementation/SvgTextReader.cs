using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
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

        public SvgTextReader(TextReader reader, bool readerShouldBeDisposed)
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

    }
}
