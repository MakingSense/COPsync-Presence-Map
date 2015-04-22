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
    public interface IMapGraphicParser
    {
        IMapGraphic ParseMapFromFile(string path);
        IMapGraphic ParseMapFromString(string source);
    }
}
