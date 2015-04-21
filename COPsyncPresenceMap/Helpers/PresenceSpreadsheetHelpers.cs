using COPsyncPresenceMap.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap.Helpers
{
    public static class PresenceSpreadsheetHelpers
    {
        public static bool HasAllColumns(this ISpreadsheet spreadsheet, IEnumerable<string> columnNames)
        {
            var result = columnNames.All(x => spreadsheet.Header.HasHeader(x));
            return result;
        }

        public static bool IsTruthy(string str)
        {
            return !string.IsNullOrEmpty(str)
                && !str.Equals("0")
                && !str.Equals("no", StringComparison.CurrentCultureIgnoreCase)
                && !str.Equals("false", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool HasTruthyValue(this ISpreadsheetRow row, Products productList)
        {
            foreach (var key in productList.ProductNames)
            {
                var value = row[key];
                if (IsTruthy(value))
                {
                    return true;
                }
            }
            return false;
        }

    }
}
