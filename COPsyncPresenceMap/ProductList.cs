using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap
{
    public class Products
    {
        const string DEFAULT_PRODUCT_NAME = "COPsync";
        public const string COPSYNC_ENTERPRISE = "COPsync Enterprise";
        public const string COPSYNC911 = "COPsync911";
        public const string WARRANTSYNC = "WARRANTsync";


        public string[] ProductNames { get; private set; }

        public static readonly Products AllProducts = new Products(new[] { COPSYNC_ENTERPRISE, COPSYNC911, WARRANTSYNC });

        private Products(string[] productNames)
        {
            ProductNames = productNames;
        }

        public string GetWithPresenceText()
        {
            var productName = ProductNames.Length == 1 ? ProductNames[0] : DEFAULT_PRODUCT_NAME;
            return string.Format("County with {0} Presence", productName);
        }

        public string GetWithoutPresenceText()
        {
            var productName = ProductNames.Length == 1 ? ProductNames[0] : DEFAULT_PRODUCT_NAME;
            return string.Format("County without {0} Presence", productName);
        }

        public Products FilterByProductName(IEnumerable<string> productNames)
        {
            return FilterByProductName(new HashSet<string>(productNames));
        }

        public Products FilterByProductName(HashSet<string> productNames)
        {
            return new Products(ProductNames.Where(x => productNames.Contains(x)).ToArray());
        }
    }
}
