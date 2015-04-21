using SpreadsheetUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COPsyncPresenceMap.WPF.Helpers
{
    public static class PresenceSpreadsheetHelpers
    {
        private const string ID_COLUMN = "ElementId";
        public const string CHECKCOLUMN_COPSYNC_ENTERPRISE = "COPsync Enterprise";
        public const string CHECKCOLUMN_COPSYNC911 = "COPsync911";
        public const string CHECKCOLUMN_WARRANTSYNC = "Warrantsync";
        public const string ELEMENTID_REF_PRESENCE_TEXT = "Ref_WithPresence_text";
        public const string ELEMENTID_REF_NO_PRESENCE_TEXT = "Ref_WithoutPresence_text";
        public const string ELEMENTID_REF_PRESENCE_BOX = "Ref_WithPresence_box";
        public const string ELEMENTID_REF_NO_PRESENCE_BOX = "Ref_WithoutPresence_box";

        public static bool HasAllRequiredColumns(this Spreadsheet spreadsheet)
        {
            var requiredColumns = new[] { ID_COLUMN, CHECKCOLUMN_COPSYNC_ENTERPRISE, CHECKCOLUMN_COPSYNC911, CHECKCOLUMN_WARRANTSYNC };
            return requiredColumns.All(x => spreadsheet.Header.HasHeader(x));
        }

        public static IEnumerable<string> GetIdsToFill(this Spreadsheet spreadsheet, params string[] columns)
        {
            return spreadsheet
                .Where(x => x.HasTruthyValue(columns))
                .Select(x => x[ID_COLUMN]);
        }

        public static bool IsTruthy(string str)
        {
            return !string.IsNullOrEmpty(str)
                && !str.Equals("0")
                && !str.Equals("no", StringComparison.CurrentCultureIgnoreCase)
                && !str.Equals("false", StringComparison.CurrentCultureIgnoreCase);
        }

        public static bool HasTruthyValue(this SpreadsheetRow row, params string[] columns)
        {
            foreach (var key in columns)
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
