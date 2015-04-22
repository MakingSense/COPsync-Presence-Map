using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Drawing;
using COPsyncPresenceMap.Spreadsheet;
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
            var xlsxParser = new XlsxSpreadsheetParser();
            var spreadsheet = xlsxParser.ParseFromFile("Book1.xlsx");

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
            var xlsxParser = new XlsxSpreadsheetParser();
            var baseSpreadsheet = xlsxParser.ParseFromFile("Book1.xlsx");
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

    }
}
