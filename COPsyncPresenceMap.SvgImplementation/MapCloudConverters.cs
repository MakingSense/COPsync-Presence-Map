using COPsyncPresenceMap;
using COPsyncPresenceMap.Helpers;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.SvgImplementation
{
    public abstract class MapCloudConverterBase : IMapGraphicConverter
    {
        public abstract string DefaultExtension { get; }
        public abstract string OutputFormat { get; }

        private readonly string _apikey;
        public MapCloudConverterBase(string apikey)
        {
            _apikey = apikey;
        }

        public void Convert(IMapGraphic mapGraphic, string outputFilename)
        {
            using (var tfh = new TemporalFileHelper())
            {
                var xmlDocument = mapGraphic.GetSvgXmlDocument();
                xmlDocument.Save(tfh.TemporalFileName);
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
                        tfh.TemporalFileName);
                    File.WriteAllBytes(outputFilename, result);
                }
            }
        }

        public bool CheckAvailability()
        {
            //TODO: check connectivity and available conversion minutes
            return true;
        }

    }

    public class MapWmfCloudConverter : MapCloudConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".wmf"; } }
        public override string OutputFormat { get { return "wmf"; } }
        public MapWmfCloudConverter(string apikey) : base(apikey) { }
    }

    public class MapEmfCloudConverter : MapCloudConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".emf"; } }
        public override string OutputFormat { get { return "emf"; } }
        public MapEmfCloudConverter(string apikey) : base(apikey) { }
    }

    public class MapEpsCloudConverter : MapCloudConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".eps"; } }
        public override string OutputFormat { get { return "eps"; } }
        public MapEpsCloudConverter(string apikey) : base(apikey) { }
    }

    public class MapPsCloudConverter : MapCloudConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".ps"; } }
        public override string OutputFormat { get { return "ps"; } }
        public MapPsCloudConverter(string apikey) : base(apikey) { }
    }

    public class MapPdfCloudConverter : MapCloudConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".pdf"; } }
        public override string OutputFormat { get { return "pdf"; } }
        public MapPdfCloudConverter(string apikey) : base(apikey) { }
    }

    public class MapPngCloudConverter : MapCloudConverterBase, IMapGraphicConverter
    {
        public override string DefaultExtension { get { return ".png"; } }
        public override string OutputFormat { get { return "png"; } }
        public MapPngCloudConverter(string apikey) : base(apikey) { }
    }
}
