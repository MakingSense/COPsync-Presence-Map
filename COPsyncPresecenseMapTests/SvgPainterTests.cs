using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Drawing;
using COPsyncPresenceMap.Graphics;

namespace COPsyncPresecenseMapTests
{
    [TestClass]
    public class MapGraphicTests
    {
        [TestMethod]
        [DeploymentItem(@"Deployment\base-map.svg")]
        public void MapGraphic_GetSvgXmlDocument_ShouldNotBeNull()
        {
            var parser = new SvgMapGraphicParser();
            var map = parser.ParseFromFile(@"base-map.svg");
            var document = map.GetSvgXmlDocument();
            Assert.IsNotNull(document);
        }

        [TestMethod]
        [DeploymentItem(@"Deployment\base-map.svg")]
        public void Fill_ShouldFillTheRightElements()
        {
            var parser = new SvgMapGraphicParser();
            var map = parser.ParseFromFile(@"base-map.svg");

            map.Fill(Color.Beige, "TX_Crosby", "TX_Lubbock", "TX_Floyd");
            map.Fill(Color.Gainsboro, "TX_Bailey", "TX_Lubbock", "TX_Briscoe");
            var document = map.GetSvgXmlDocument();

            var colorDefault = ColorTranslator.FromHtml("#D0D0D0");
            var color_TX_Crosby = ColorTranslator.FromHtml(document.GetElementById("TX_Crosby").GetAttribute("fill"));
            var color_TX_Lubbock = ColorTranslator.FromHtml(document.GetElementById("TX_Lubbock").GetAttribute("fill"));
            var color_TX_Floyd = ColorTranslator.FromHtml(document.GetElementById("TX_Floyd").GetAttribute("fill"));
            var color_TX_Bailey = ColorTranslator.FromHtml(document.GetElementById("TX_Bailey").GetAttribute("fill"));
            var color_TX_Briscoe = ColorTranslator.FromHtml(document.GetElementById("TX_Briscoe").GetAttribute("fill"));
            var color_TX_Red_River = ColorTranslator.FromHtml(document.GetElementById("TX_Red_River").GetAttribute("fill"));


            Assert.AreEqual(Color.Gainsboro, color_TX_Bailey);
            Assert.AreEqual(Color.Gainsboro, color_TX_Lubbock);
            Assert.AreEqual(Color.Gainsboro, color_TX_Briscoe);

            Assert.AreEqual(Color.Beige, color_TX_Crosby);
            Assert.AreEqual(Color.Beige, color_TX_Floyd);

            Assert.AreEqual(colorDefault, color_TX_Red_River);
        }
    }
}
