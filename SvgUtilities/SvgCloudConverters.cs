using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SvgUtilities
{
    public abstract class SvgCloudConverterBase : ISvgConverter
    {
        public abstract string DefaultExtension { get; }
        public abstract string OutputFormat { get; }

        private readonly string _apikey;
        public SvgCloudConverterBase(string apikey)
        {
            _apikey = apikey;
        }

        public void Convert(XmlDocument xmlDocument, string outputFilename)
        {
            var temporalFilename = Path.GetTempFileName();
            xmlDocument.Save(temporalFilename);

            try
            {
                using (WebClient client = new WebClient())
                {
                    client.Headers["Content-Type"] = "binary/octet-stream";
                    StringBuilder sb = new StringBuilder();
                    var result = client.UploadFile(
                        "https://api.cloudconvert.com/convert?" +
                            "apikey=" + _apikey +
                            "&input=upload" +
                            "&inputformat=svg" +
                            "&outputformat=" + OutputFormat,
                        temporalFilename);
                    File.WriteAllBytes(outputFilename, result);
                }
            }
            finally
            {
                File.Delete(temporalFilename);
            }
        }

        public bool CheckAvailability()
        {
            //TODO: check connectivity and available conversion minutes
            return true;
        }

    }

    public class SvgToWmfCloudConverter : SvgCloudConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".wmf"; } }
        public override string OutputFormat { get { return "wmf"; } }
        public SvgToWmfCloudConverter(string apikey) : base(apikey) { }
    }

    public class SvgToEmfCloudConverter : SvgCloudConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".emf"; } }
        public override string OutputFormat { get { return "emf"; } }
        public SvgToEmfCloudConverter(string apikey) : base(apikey) { }
    }

    public class SvgToEpsCloudConverter : SvgCloudConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".eps"; } }
        public override string OutputFormat { get { return "eps"; } }
        public SvgToEpsCloudConverter(string apikey) : base(apikey) { }
    }

    public class SvgToPsCloudConverter : SvgCloudConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".ps"; } }
        public override string OutputFormat { get { return "ps"; } }
        public SvgToPsCloudConverter(string apikey) : base(apikey) { }
    }

    public class SvgToPdfCloudConverter : SvgCloudConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".pdf"; } }
        public override string OutputFormat { get { return "pdf"; } }
        public SvgToPdfCloudConverter(string apikey) : base(apikey) { }
    }

    public class SvgToPngCloudConverter : SvgCloudConverterBase, ISvgConverter
    {
        public override string DefaultExtension { get { return ".png"; } }
        public override string OutputFormat { get { return "png"; } }
        public SvgToPngCloudConverter(string apikey) : base(apikey) { }
    }
}
