using SvgUtilities;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap
{
    class Program
    {
        static void Main(string[] args)
        {
            var document = SvgTextReader.GetDocumentFromFile("base-map.svg");
            var mapfiller = new SvgPainter(document, Color.FromArgb(64, 79, 54));
            mapfiller.Fill(Color.Red, "TX_Red_River");
            mapfiller.Fill(Color.Blue, "TX_Bailey");
            mapfiller.Fill("Ref_WithPresence", "TX_Grayson", "TX_Lamar", "TX_Arkansas");

            document.Save("out.svg");

            Console.WriteLine("Enter to continue . . . ");
            Console.ReadLine();
        }
    }
}
