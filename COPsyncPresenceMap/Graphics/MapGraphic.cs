﻿using COPsyncPresenceMap.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace COPsyncPresenceMap.Graphics
{
    public class MapGraphic : IMapGraphic
    {
        private XmlDocument _svgXmlDocument;

        public MapGraphic(XmlDocument document)
        {
            _svgXmlDocument = document;
        }

        public XmlDocument GetSvgXmlDocument()
        {
            return _svgXmlDocument;
        }

        public void Fill(Color color, params string[] ids)
        {
            Fill(color, ids.AsEnumerable());
        }

        public void Fill(Color color, IEnumerable<string> ids)
        {
            var htmlColor = ToHtmlColor(color);
            foreach (var id in ids)
            {
                var mapElement = _svgXmlDocument.GetElementById(id);
                mapElement.SetAttribute("fill", htmlColor);
            }
        }

        public void FillCounties(Color color)
        {
            var htmlColor = ToHtmlColor(color);
            var list = GetAllCounties();
            foreach (var mapElement in list)
            {
                mapElement.SetAttribute("fill", htmlColor);
            }
        }

        public void FillReferenceBoxes(Color color)
        {
            var htmlColor = ToHtmlColor(color);
            var list = GetAllReferenceBoxes();
            foreach (var mapElement in list)
            {
                mapElement.SetAttribute("fill", htmlColor);
            }
        }

        public void Stroke(Color color, params string[] ids)
        {
            Stroke(color, ids.AsEnumerable());
        }

        public void Stroke(Color color, IEnumerable<string> ids)
        {
            var htmlColor = ToHtmlColor(color);
            foreach (var id in ids)
            {
                var mapElement = _svgXmlDocument.GetElementById(id);
                mapElement.SetAttribute("stroke", htmlColor);
            }
        }

        public void StrokeCounties(Color color)
        {
            var htmlColor = ToHtmlColor(color);
            var list = GetAllCounties();
            foreach (XmlElement mapElement in list)
            {
                mapElement.SetAttribute("stroke", htmlColor);
            }
        }

        public void ReduceStrokeCounties()
        {
            var list = GetAllCounties();
            foreach (XmlElement mapElement in list)
            {
                mapElement.SetAttribute("stroke-width", "0.40");
            }
        }

        public void StrokeReferenceBoxes(Color color)
        {
            var htmlColor = ToHtmlColor(color);
            var list = GetAllReferenceBoxes();
            foreach (XmlElement mapElement in list)
            {
                mapElement.SetAttribute("stroke", htmlColor);
            }
        }

        public void StrokeOuterBorder(Color color)
        {
            var htmlColor = ToHtmlColor(color);
            var list = GetOuterBorder();
            foreach (XmlElement mapElement in list)
            {
                mapElement.SetAttribute("stroke", htmlColor);
            }
        }

        public void SetText(string id, string text)
        {
            var mapElement = _svgXmlDocument.GetElementById(id);
            mapElement.InnerText = text;
        }

        public void ShowCountyNames(bool show = true)
        {
            var elements = new[] { _svgXmlDocument.GetElementById("CountyNames"), _svgXmlDocument.GetElementById("CountiesNames") }
                .Where(x => x != null);
            foreach (var mapElement in elements)
            {
                mapElement.SetAttribute("display", show ? "inherit" : "none");
            }
        }

        public void HideCountyNames()
        {
            ShowCountyNames(false);
        }

        private string ToHtmlColor(Color color)
        {
            return color.A == 0 ? "TRANSPARENT" : ColorTranslator.ToHtml(color);
        }

        private IEnumerable<XmlElement> GetAllCounties()
        {
            //return _svgXmlDocument.GetElementsByTagName("path").OfType<XmlElement>();
            return GetElementsInGroups(
                groupIds: new[] { "Counties" },
                tagNames: new[] { "path", "polygon" });
        }

        private IEnumerable<XmlElement> GetAllReferenceBoxes()
        {
            return GetElementsInGroups(
                groupIds: new[] { "Legend" },
                tagNames: new[] { "rect" });
        }

        private IEnumerable<XmlElement> GetOuterBorder()
        {
            return GetElementsInGroups(
                groupIds: new[] { "Outer_border", "Outer_Border" },
                tagNames: new[] { "polygon", "path" });
        }

        private IEnumerable<XmlElement> GetElementsInGroups(string[] groupIds, string[] tagNames)
        {
            return groupIds.Select(groupId => _svgXmlDocument.GetElementById(groupId))
                .Where(group => group != null)
                .SelectMany(group => tagNames.SelectMany(
                    tagName => group.GetElementsByTagName(tagName).OfType<XmlElement>()));
        }
    }
}
