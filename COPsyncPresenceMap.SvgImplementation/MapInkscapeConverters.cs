using COPsyncPresenceMap.Graphics;
using COPsyncPresenceMap.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public abstract class MapInkscapeConverterBase : IMapGraphicConverter
    {
        public abstract string DefaultExtension { get; }
        public abstract string ConvertOption { get; }

        public void Convert(IMapGraphic mapGraphic, string outputFilename)
        {
            var inkscapeExe = GetInkscapePath();
            if (inkscapeExe == null)
            {
                throw new ApplicationException("Inkscape is required to convert.");
            }
            using (var tfh = new TemporalFileHelper())
            {
                var xmlDocument = mapGraphic.GetSvgXmlDocument();
                xmlDocument.Save(tfh.TemporalFileName);

                var arguments = string.Format("--file {0} {1} {2}", tfh.TemporalFileName, ConvertOption, outputFilename);
                Process p = Process.Start(inkscapeExe, arguments);
                p.WaitForExit();
            }
        }

        public bool CheckAvailability()
        {
            return GetInkscapePath() != null;
        }

        private string GetInkscapePath()
        {
            var exeName = "inkscape.exe";
            var possiblePaths = new[]
                {
                    Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFiles), "Inkscape"),
                    Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.ProgramFilesX86), "Inkscape")
                }.Distinct(StringComparer.CurrentCultureIgnoreCase);

            foreach (var path in possiblePaths)
            {
                var fullName = Path.Combine(path, exeName);
                if (File.Exists(fullName))
                {
                    return fullName;
                }
            }
            return null;
        }
    }

    public class MapWmfInkscapeConverter : MapInkscapeConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".wmf"; } }
        public override string ConvertOption { get { return "--export-wmf"; } }
    }

    public class MapEmfInkscapeConverter : MapInkscapeConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".emf"; } }
        public override string ConvertOption { get { return "--export-emf"; } }
    }

    public class MapEpsInkscapeConverter : MapInkscapeConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".eps"; } }
        public override string ConvertOption { get { return "--export-eps"; } }
    }

    public class MapPsInkscapeConverter : MapInkscapeConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".ps"; } }
        public override string ConvertOption { get { return "--export-ps"; } }
    }

    public class MapPdfInkscapeConverter : MapInkscapeConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".pdf"; } }
        public override string ConvertOption { get { return "--export-pdf"; } }
    }

    public class MapPlainSvgInkscapeConverter : MapInkscapeConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".svg"; } }
        public override string ConvertOption { get { return "--export-plain-svg"; } }
    }

    public class MapPngInkscapeConverter : MapInkscapeConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".png"; } }
        public override string ConvertOption { get { return "--export-png"; } }
    }
}
