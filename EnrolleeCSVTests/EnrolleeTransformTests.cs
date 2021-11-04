using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;

namespace EnrolleeCSV.Tests
{
    [TestClass()]
    public class EnrolleeTransformTests
    {
        [TestMethod()]
        public void TransformTest()
        {
            var csvStrings = new List<string> { 
                "User1,John SortMeSecnd Doe,1,Company0",
                "User2,Jane SortMeThird Zoe,1,Company0",
                "User3,Jane SortMeFirst Doe,1,Company0",

                "User1,John SortMeSecond Doe,1,Company1",
                "User2,Jane OmitVersion0 Doe,0,Company1",
                "User2,Jane KeepVersion1 Doe,1,Company1"
            };
            var enrolleeTransformer = new EnrolleeTransform();

            List<InsCoList> result = enrolleeTransformer.Transform(csvStrings);

            InsCoList ico = result[0];
            Assert.AreEqual("Company0.csv", ico.FileName);
            Assert.IsTrue(ico.CsvLines[0].Contains("Jane SortMeFirst Doe"));
            Assert.IsTrue(ico.CsvLines[1].Contains("John SortMeSecond Doe"));
            Assert.IsTrue(ico.CsvLines[2].Contains("Jane SortMeThird Zoe"));

            ico = result[1];
            Assert.AreEqual("Company1.csv", ico.FileName);
            Assert.IsTrue(ico.CsvLines[0].Contains("Jane KeepVersion1 Doe"));
        }
    }
}