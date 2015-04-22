using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap
{
    public interface IMapGraphicConverter
    {
        string DefaultExtension { get; }

        bool CheckAvailability();

        void Convert(IMapGraphic mapGraphic, string outputFilename);
    }
}
