using System.Linq;
using System.Threading.Tasks;
using CosmosDBConsole;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IntegrationTests
    {
        [Test]
        public async Task Test1()
        {
            DataRetrivalModel model = new DataRetrivalModel();
            var result = await model.ReadPopulationCount("iTRV");

            Assert.AreEqual(33, result.Count);
            Assert.Greater(result.First(), 6590);
            Assert.Greater(result[1], 460);
            Assert.Less(result[1], 1000);

            //CollectionAssert.AreEqual(new[]{6591,463,100}, result);
        }
    }
}