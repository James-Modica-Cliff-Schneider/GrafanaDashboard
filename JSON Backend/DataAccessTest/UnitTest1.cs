using System.Threading.Tasks;
using DataRepository;
using NUnit.Framework;


namespace DataAccessTest
{
    [TestFixture]
    public class GetDataTimeSeriesTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            GetData target = new GetData();
            var result = await target.GetDataTimeSeriesData(new System.DateTime(2019, 07, 10, 13, 0, 0), new System.DateTime(2019, 07, 08), "DataTimeSeries");
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual(12529, result[0].ControllerCount);
            Assert.AreEqual(12557, result[1].ControllerCount);
            Assert.That(result[1].Date, Is.EqualTo(new System.DateTime(2019, 07, 10, 12, 55, 27)).Within(1).Minutes);
            Assert.That(result[0].Date, Is.EqualTo(new System.DateTime(2019, 07, 08, 14, 48, 15)).Within(1).Minutes);
            Assert.AreEqual(144, result[0].HeatingActuatorCount);
            Assert.AreEqual(2505, result[1].HeimanSmartPlugCount);
            Assert.AreEqual(32890, result[0].ItrvsCount);
            Assert.AreEqual(7, result[1].LoadActuatorCount);
            Assert.AreEqual(532, result[0].OwonSmartPlugCount);
            Assert.AreEqual(15192, result[1].RoomStatCount);
            Assert.AreEqual(63939, result[0].ConnectedDeviceCount);


        }
    }
}