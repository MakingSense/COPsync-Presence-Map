﻿using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Xml;
using System.Drawing;
using SpreadsheetUtilities;
using System.Linq;


namespace COPsyncPresecenseMapTests
{
    [TestClass]
    public class TexasXlsxDataTests
    {
        [TestMethod]
        [DeploymentItem(@"Deployment\COPsyncPresence.xlsx")]
        public void COPsyncPresenceAllIds()
        {
            var xlsxReader = new XlsxReader();
            var baseSpreadsheet = xlsxReader.Read("COPsyncPresence.xlsx");
            var spreadsheet = baseSpreadsheet.CreateNewParsingHeaders();

            var allIdsByColumnName = spreadsheet.Select(x => x["ElementId"]).Where(x => x.StartsWith("TX_")).OrderBy(x => x).ToArray();

            #region expected
            var expected = new[] {
                "TX_Anderson",
                "TX_Andrews",
                "TX_Angelina",
                "TX_Archer",
                "TX_Arkansas",
                "TX_Armstrong",
                "TX_Atascosa",
                "TX_Austin",
                "TX_Bailey",
                "TX_Bandera",
                "TX_Bastrop",
                "TX_Baylor",
                "TX_Bee",
                "TX_Bell",
                "TX_Bexar",
                "TX_Blanco",
                "TX_Borden",
                "TX_Bosque",
                "TX_Bowie",
                "TX_Brazoria",
                "TX_Brazos",
                "TX_Brewster",
                "TX_Briscoe",
                "TX_Brooks",
                "TX_Brown",
                "TX_Burleson",
                "TX_Burnet",
                "TX_Caldwell",
                "TX_Calhoun",
                "TX_Callahan",
                "TX_Cameron",
                "TX_Camp",
                "TX_Carson",
                "TX_Cass",
                "TX_Castro",
                "TX_Chambers",
                "TX_Cherokee",
                "TX_Childress",
                "TX_Clay",
                "TX_Cochran",
                "TX_Coke",
                "TX_Coleman",
                "TX_Collin",
                "TX_Collingsworth",
                "TX_Colorado",
                "TX_Comal",
                "TX_Comanche",
                "TX_Concho",
                "TX_Cooke",
                "TX_Coryell",
                "TX_Cottle",
                "TX_Crane",
                "TX_Crockett",
                "TX_Crosby",
                "TX_Culberson",
                "TX_Dallam",
                "TX_Dallas",
                "TX_Dawson",
                "TX_Deaf_Smith",
                "TX_Delta",
                "TX_Denton",
                "TX_DeWitt",
                "TX_Dickens",
                "TX_Dimmit",
                "TX_Donley",
                "TX_Duval",
                "TX_Eastland",
                "TX_Ector",
                "TX_Edwards",
                "TX_El_Paso",
                "TX_Ellis",
                "TX_Erath",
                "TX_Falls",
                "TX_Fannin",
                "TX_Fayette",
                "TX_Fisher",
                "TX_Floyd",
                "TX_Foard",
                "TX_Fort_Bend",
                "TX_Franklin",
                "TX_Freestone",
                "TX_Frio",
                "TX_Gaines",
                "TX_Galveston",
                "TX_Garza",
                "TX_Gillespie",
                "TX_Glasscock",
                "TX_Goliad",
                "TX_Gonzales",
                "TX_Gray",
                "TX_Grayson",
                "TX_Gregg",
                "TX_Grimes",
                "TX_Guadalupe",
                "TX_Hale",
                "TX_Hall",
                "TX_Hamilton",
                "TX_Hansford",
                "TX_Hardeman",
                "TX_Hardin",
                "TX_Harris",
                "TX_Harrison",
                "TX_Hartley",
                "TX_Haskell",
                "TX_Hays",
                "TX_Hemphill",
                "TX_Henderson",
                "TX_Hidalgo",
                "TX_Hill",
                "TX_Hockley",
                "TX_Hood",
                "TX_Hopkins",
                "TX_Houston",
                "TX_Howard",
                "TX_Hudspeth",
                "TX_Hunt",
                "TX_Hutchinson",
                "TX_Irion",
                "TX_Jack",
                "TX_Jackson",
                "TX_Jasper",
                "TX_Jeff_Davis",
                "TX_Jefferson",
                "TX_Jim_Hogg",
                "TX_Jim_Wells",
                "TX_Johnson",
                "TX_Jones",
                "TX_Karnes",
                "TX_Kaufman",
                "TX_Kendall",
                "TX_Kenedy",
                "TX_Kent",
                "TX_Kerr",
                "TX_Kimble",
                "TX_King",
                "TX_Kinney",
                "TX_Kleberg",
                "TX_Knox",
                "TX_La_Salle",
                "TX_Lamar",
                "TX_Lamb",
                "TX_Lampasas",
                "TX_Lavaca",
                "TX_Lee",
                "TX_Leon",
                "TX_Liberty",
                "TX_Limestone",
                "TX_Lipscomb",
                "TX_Live_Oak",
                "TX_Llano",
                "TX_Loving",
                "TX_Lubbock",
                "TX_Lynn",
                "TX_Madison",
                "TX_Marion",
                "TX_Martin",
                "TX_Mason",
                "TX_Matagorda",
                "TX_Maverick",
                "TX_McCulloch",
                "TX_McLennan",
                "TX_McMullen",
                "TX_Medina",
                "TX_Menard",
                "TX_Midland",
                "TX_Milam",
                "TX_Mills",
                "TX_Mitchell",
                "TX_Montague",
                "TX_Montgomery",
                "TX_Moore",
                "TX_Morris",
                "TX_Motley",
                "TX_Nacogdoches",
                "TX_Navarro",
                "TX_Newton",
                "TX_Nolan",
                "TX_Nueces",
                "TX_Ochiltree",
                "TX_Oldham",
                "TX_Orange",
                "TX_Palo_Pinto",
                "TX_Panola",
                "TX_Parker",
                "TX_Parmer",
                "TX_Pecos",
                "TX_Polk",
                "TX_Potter",
                "TX_Presidio",
                "TX_Rains",
                "TX_Randall",
                "TX_Reagan",
                "TX_Real",
                "TX_Red_River",
                "TX_Reeves",
                "TX_Refugio",
                "TX_Roberts",
                "TX_Robertson",
                "TX_Rockwall",
                "TX_Runnels",
                "TX_Rusk",
                "TX_Sabine",
                "TX_San_Augustine",
                "TX_San_Jacinto",
                "TX_San_Patricio",
                "TX_San_Saba",
                "TX_Schleicher",
                "TX_Scurry",
                "TX_Shackelford",
                "TX_Shelby",
                "TX_Sherman",
                "TX_Smith",
                "TX_Somervell",
                "TX_Starr",
                "TX_Stephens",
                "TX_Sterling",
                "TX_Stonewall",
                "TX_Sutton",
                "TX_Swisher",
                "TX_Tarrant",
                "TX_Taylor",
                "TX_Terrell",
                "TX_Terry",
                "TX_Throckmorton",
                "TX_Titus",
                "TX_Tom_Green",
                "TX_Travis",
                "TX_Trinity",
                "TX_Tyler",
                "TX_Upshur",
                "TX_Upton",
                "TX_Uvalade",
                "TX_Val_Verde",
                "TX_Van_Zandt",
                "TX_Victoria",
                "TX_Walker",
                "TX_Waller",
                "TX_Ward",
                "TX_Washington",
                "TX_Webb",
                "TX_Wharton",
                "TX_Wheeler",
                "TX_Wichita",
                "TX_Wilbarger",
                "TX_Willacy",
                "TX_Williamson",
                "TX_Wilson",
                "TX_Winkler",
                "TX_Wise",
                "TX_Wood",
                "TX_Yoakum",
                "TX_Young",
                "TX_Zapata",
                "TX_Zavala"
            };
            #endregion

            CollectionAssert.AreEqual(expected, allIdsByColumnName);
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

            #region expected
            var expected = new[] {
                "TX_Anderson",
                "TX_Armstrong",
                "TX_Bastrop",
                "TX_Blanco",
                "TX_Brazos",
                "TX_Briscoe",
                "TX_Burleson",
                "TX_Cameron",
                "TX_Carson",
                "TX_Chambers",
                "TX_Childress",
                "TX_Collin",
                "TX_Concho",
                "TX_Crockett",
                "TX_Dawson",
                "TX_Delta",
                "TX_Dickens",
                "TX_Ector",
                "TX_El_Paso",
                "TX_Falls",
                "TX_Fayette",
                "TX_Franklin",
                "TX_Garza",
                "TX_Gray",
                "TX_Hale",
                "TX_Hamilton",
                "TX_Hardin",
                "TX_Hays",
                "TX_Henderson",
                "TX_Hockley",
                "TX_Hopkins",
                "TX_Hutchinson",
                "TX_Jeff_Davis",
                "TX_Jones",
                "TX_Kent",
                "TX_Kimble",
                "TX_Kleberg",
                "TX_Lampasas",
                "TX_Lee",
                "TX_Limestone",
                "TX_Live_Oak",
                "TX_Madison",
                "TX_Maverick",
                "TX_Menard",
                "TX_Montague",
                "TX_Moore",
                "TX_Nacogdoches",
                "TX_Ochiltree",
                "TX_Orange",
                "TX_Parker",
                "TX_Pecos",
                "TX_Randall",
                "TX_Refugio",
                "TX_Rusk",
                "TX_San_Saba",
                "TX_Scurry",
                "TX_Sherman",
                "TX_Sterling",
                "TX_Sutton",
                "TX_Taylor",
                "TX_Terry",
                "TX_Trinity",
                "TX_Val_Verde",
                "TX_Ward",
                "TX_Wichita",
                "TX_Willacy",
                "TX_Winkler",
                "TX_Zapata"
            };
            #endregion

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
            var expectedCount = 219;

            Assert.AreEqual(expectedCount, ids.Count());
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