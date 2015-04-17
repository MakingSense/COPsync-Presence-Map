using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SvgUtilities
{
    public abstract class SvgInkscapeConverterBase : ISvgConverter
    {
        public abstract string DefaultExtension { get; }
        public abstract string ConvertOption { get; }

        public void Convert(XmlDocument xmlDocument, string outputFilename)
        {
            var inkscapeExe = GetInkscapePath();
            if (inkscapeExe == null)
            {
                throw new ApplicationException("Inkscape is required to convert.");
            }
            var temporalFilename = Path.ChangeExtension(Path.GetTempFileName(), ".svg");
            xmlDocument.Save(temporalFilename);

            try
            {
                var arguments = string.Format("--file {0} {1} {2}", temporalFilename, ConvertOption, outputFilename);
                Process p = Process.Start(inkscapeExe, arguments);
                p.WaitForExit();
            }
            finally
            {
                File.Delete(temporalFilename);
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

    public class SvgToWmfInkscapeConverter : SvgInkscapeConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".wmf"; } }
        public override string ConvertOption { get { return "--export-wmf"; } }
    }

    public class SvgToEmfInkscapeConverter : SvgInkscapeConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".emf"; } }
        public override string ConvertOption { get { return "--export-emf"; } }
    }

    public class SvgToEpsInkscapeConverter : SvgInkscapeConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".eps"; } }
        public override string ConvertOption { get { return "--export-eps"; } }
    }

    public class SvgToPsInkscapeConverter : SvgInkscapeConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".ps"; } }
        public override string ConvertOption { get { return "--export-ps"; } }
    }

    public class SvgToPdfInkscapeConverter : SvgInkscapeConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".pdf"; } }
        public override string ConvertOption { get { return "--export-pdf"; } }
    }

    public class SvgToPlainSvgInkscapeConverter : SvgInkscapeConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".svg"; } }
        public override string ConvertOption { get { return "--export-plain-svg"; } }
    }

    public class SvgToPngInkscapeConverter : SvgInkscapeConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".png"; } }
        public override string ConvertOption { get { return "--export-png"; } }
    }
}
