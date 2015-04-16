using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SvgUtilities
{
    public interface ISvgConverter
    {
        void Convert(XmlDocument xmlDocument, string outputFilename);
    }
}
