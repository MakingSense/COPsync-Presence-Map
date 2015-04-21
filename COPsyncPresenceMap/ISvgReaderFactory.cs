using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap
{
    public interface ISvgReaderFactory
    {
        XmlTextReader FromFile(string path);
        XmlTextReader FromString(string source);
        XmlDocument GetDocumentFromFile(string path);
        XmlDocument GetDocumentFromString(string source);
    }
}
