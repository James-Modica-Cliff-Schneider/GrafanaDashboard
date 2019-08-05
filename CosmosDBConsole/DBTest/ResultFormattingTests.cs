using System;
using System.Collections.Generic;
using System.Linq;
using CosmosDBConsole;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ResultFormattingTests
    {
        [Test]
        public void FormatPopulationCounts_WithSingleJObject_ReturnsSummedListWithOneEntry()
        {
            var input = "{\"ProdCount\": [3]}";
            var inputList = new List<JObject> {JsonConvert.DeserializeObject<JObject>(input)};

            var result = DataRetrivalModel.FormatPopulationCounts(inputList);

            Assert.AreEqual(33, result.Count);
            Assert.AreEqual(0, result[0]);
            Assert.AreEqual(1, result[3]);
            Assert.AreEqual(0, result[4]);
        }


        [Test]
        public void FormatPopulationCounts_WithLotsOfJObject_ReturnsSummedListWithSummedEntries()
        {
            var counts = new[] {10, 5, 4, 3, 2};
            var inputList = new List<JObject>();
            for (int i = 0; i < counts.Length; i++)
            {
                for (int j = 0; j < counts[i]; j++)
                {
                    var input = "{\"ProdCount\": [" + i + "]}";
                    inputList.Add(JsonConvert.DeserializeObject<JObject>(input));
                }
            }

            var result = DataRetrivalModel.FormatPopulationCounts(inputList);

            CollectionAssert.AreEqual(new []{10,5,4,3,2}, result.Take(5));
        }

        [TestCase("{\"SomeOtherProperty\": [3]}")]
        public void FormatPopulationCounts_WithUnexpectedJObject_ThrowsAMeaningfulNullReferenceException(string input)
        {
            var inputList = new List<JObject> { JsonConvert.DeserializeObject<JObject>(input) };

            var result = Assert.Throws<NullReferenceException>(() => DataRetrivalModel.FormatPopulationCounts(inputList), $"With input: {input}");

            StringAssert.Contains("Expected JObject with ProdCount property ", result.Message);

        }


        [TestCase("{\"ProdCount\": 3}")]
        public void FormatPopulationCounts_WithUnexpectedJObject_ThrowsAMeaningfulInvalidOperationException(string input)
        {
            var inputList = new List<JObject> { JsonConvert.DeserializeObject<JObject>(input) };

            var result = Assert.Throws<InvalidOperationException>(() => DataRetrivalModel.FormatPopulationCounts(inputList), $"With input: {input}");


            StringAssert.Contains(input, result.Message);
        }
    }
}