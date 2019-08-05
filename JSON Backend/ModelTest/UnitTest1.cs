using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations.Model;
using System.Threading.Tasks;
using Model;
using Model.ViewModels;
using Moq;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public async Task Test1()
        {
            DateTime dateTime1 = new DateTime(2019,07,10,14,00,00);
            DateTime dateTime2 = new DateTime(2019, 07, 09, 14, 00, 00);
            DateTime dateTimeTo = new DateTime(2019, 07, 10, 18, 00, 00);
            DateTime dateTimeFrom = new DateTime(2019, 07, 09, 10, 00, 00);
            TargetViewModel targetView = new TargetViewModel("ControllerCount");
            TargetViewModel[] targetViewArray = new TargetViewModel[1]{ targetView };
            DataPoint<int> expectDataPoint1 = new DataPoint<int>(dateTime1 ,1000);
            DataPoint<int> expectDataPoint2 = new DataPoint<int>(dateTime2, 1100);
            DataPoint<int>[] expectedDataPoints = new DataPoint<int>[2]{ expectDataPoint1, expectDataPoint2 };
            DataSeries<int> expectedDataSeries = new DataSeries<int>(targetView.ToString(), expectedDataPoints);
            List<DataSeries<int>> expectedDataSeriesList = new List<DataSeries<int>>();
            expectedDataSeriesList.Add(expectedDataSeries);

            var mockDataAccess = new Mock<ITimeSeriesData>();
            mockDataAccess.Setup(data => data.GetDataTimeSeriesData(It.IsAny<DateTime>(), It.IsAny<DateTime>(), "DataTimeSeries"))
                .ReturnsAsync(new List<DeviceCounts>
                    {new DeviceCounts(dateTime1, "10000","1000", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100"), new DeviceCounts(dateTime2, "11000","1100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100", "100") });
            var target = new RequestHandler(mockDataAccess.Object);

            var result = await target.GetOutputTimeSeries(dateTimeTo, dateTimeFrom, targetViewArray, "DataTimeSeries");

            CollectionAssert.AreEqual(expectedDataSeriesList[0].Datapoints, result[0].Datapoints);
           
        }
    }
}