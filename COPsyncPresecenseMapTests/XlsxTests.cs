using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Drawing;
using SpreadsheetUtilities;
using System.Linq;


namespace COPsyncPresecenseMapTests
{
    [TestClass]
    public class XlsxTests
    {
        [TestMethod]
        [DeploymentItem(@"Deployment\Book1.xlsx")]
        public void GeneralTest()
        {
            var xlsxReader = new XlsxReader();
            var spreadsheet = xlsxReader.Read("Book1.xlsx");

            Assert.AreEqual(10, spreadsheet.ColumnCount);
            Assert.AreEqual(10, spreadsheet.RowCount);

            Assert.AreEqual("ColumnA", spreadsheet[0, 0]);
            Assert.AreEqual(null, spreadsheet[0, 1]);
            Assert.AreEqual("Cellb2", spreadsheet[1, 1]);
            Assert.AreEqual(null, spreadsheet[1, 2]);
            Assert.AreEqual("100", spreadsheet[2, 2]);
            Assert.AreEqual(null, spreadsheet[7, 7]);
            Assert.AreEqual("800", spreadsheet[9, 9]);
        }

        [TestMethod]
        [DeploymentItem(@"Deployment\Book1.xlsx")]
        public void WithHeadersTest()
        {
            var xlsxReader = new XlsxReader();
            var baseSpreadsheet = xlsxReader.Read("Book1.xlsx");
            var spreadsheet = baseSpreadsheet.CreateNewParsingHeaders();

            Assert.AreEqual(10, spreadsheet.ColumnCount);
            Assert.AreEqual(9, spreadsheet.RowCount);

            Assert.AreEqual("1", spreadsheet[0, 0]);
            Assert.AreEqual("1", spreadsheet[0, "ColumnA"]);

            Assert.AreEqual("Cellb2", spreadsheet[0, 1]);

            Assert.AreEqual("100", spreadsheet[1, 2]);
            Assert.AreEqual("100", spreadsheet[1, "ColumnC"]);

            Assert.AreEqual(null, spreadsheet[6, 7]);
            Assert.AreEqual("800", spreadsheet[8, 9]);
        }

        [TestMethod]
        [DeploymentItem(@"Deployment\COPsyncPresence.xlsx")]
        public void COPsyncPresenceAllIds()
        {
            var xlsxReader = new XlsxReader();
            var baseSpreadsheet = xlsxReader.Read("COPsyncPresence.xlsx");
            var spreadsheet = baseSpreadsheet.CreateNewParsingHeaders();

            var allIdsByColumnName = spreadsheet.Select(x => x["ElementId"]).ToArray();
            var allIdsByColumnIndex = spreadsheet.Select(x => x[0]).ToArray();
            var expected = new[] { "TX_Bailey", "TX_Lubbock", "TX_Briscoe", "TX_Red_River", "Ref_WithPresence" };

            CollectionAssert.AreEqual(expected, allIdsByColumnName);
            CollectionAssert.AreEqual(expected, allIdsByColumnIndex);
        }

        [TestMethod]
        [DeploymentItem(@"Deployment\COPsyncPresence.xlsx")]
        public void COPsyncPresenceCOPsyncEnterpriseIds()
        {
            var xlsxReader = new XlsxReader();
            var baseSpreadsheet = xlsxReader.Read("COPsyncPresence.xlsx");
            var spreadsheet = baseSpreadsheet.CreateNewParsingHeaders();

            var ids = spreadsheet
                .Where(x => IsTruthy(x["COPsync Enterprise"]))
                .Select(x => x["ElementId"]).ToArray();
            var expected = new[] { "TX_Bailey", "Ref_WithPresence" };

            CollectionAssert.AreEqual(expected, ids);
        }

        [TestMethod]
        [DeploymentItem(@"Deployment\COPsyncPresence.xlsx")]
        public void COPsyncPresenceAnyMark()
        {
            var xlsxReader = new XlsxReader();
            var baseSpreadsheet = xlsxReader.Read("COPsyncPresence.xlsx");
            var spreadsheet = baseSpreadsheet.CreateNewParsingHeaders();

            var ids = spreadsheet
                .Where(x => IsTruthy(x["COPsync Enterprise"]) || IsTruthy(x["COPsync911"]) || IsTruthy(x["Warrantsync"]))
                .Select(x => x["ElementId"]).ToArray();
            var expected = new[] { "TX_Bailey", "TX_Lubbock", "TX_Briscoe", "Ref_WithPresence" };

            CollectionAssert.AreEqual(expected, ids);
        }


        [TestMethod]
        [DeploymentItem(@"Deployment\COPsyncPresence.xlsx")]
        public void COPsyncPresenceMarkHeaders()
        {
            var xlsxReader = new XlsxReader();
            var baseSpreadsheet = xlsxReader.Read("COPsyncPresence.xlsx");
            var spreadsheet = baseSpreadsheet.CreateNewParsingHeaders();

            var headers = spreadsheet.Header.Where(x => !string.IsNullOrEmpty(x) && !x.Equals("ElementId", StringComparison.CurrentCultureIgnoreCase)).ToArray();
            CollectionAssert.AreEqual(new[] { "COPsync Enterprise", "COPsync911", "Warrantsync" }, headers);
        }

        private bool IsTruthy(string str)
        {
            return !string.IsNullOrEmpty(str)
                && !str.Equals("0")
                && !str.Equals("no", StringComparison.CurrentCultureIgnoreCase)
                && !str.Equals("false", StringComparison.CurrentCultureIgnoreCase);
        }
    }
}
