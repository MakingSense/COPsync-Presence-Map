﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.Graphics
{
    public interface IMapGraphicParser
    {
        IMapGraphic ParseFromFile(string path);
        IMapGraphic ParseFromString(string source);
    }
}
