using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Xml;

namespace COPsyncPresenceMap.WPF.Services.Interfaces
{

    public class PainterServiceEventArgs
    {
        public string ResultPath { get; set; }
    }
    public interface IPainterService
    {
        event EventHandler<PainterServiceEventArgs> Done;
        void Process(string inputFilePath, string outputFolderPath, Color color, IEnumerable<string> ids);
    }
}
