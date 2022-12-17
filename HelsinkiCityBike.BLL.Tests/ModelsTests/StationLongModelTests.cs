using HelsinkiCityBike.BLL.Models;
using NUnit.Framework;

namespace HelsinkiCityBike.BLL.Tests.ModelsTests
{
    public class StationLongModelTests
    {
        [TestCase(15000, 10, 1.5F)]
        [TestCase(0, 24, 0)]
        [TestCase(67867869.99F, 666, 101.9F)]
        public void AvgDistanceOfJourneyStartingFrom_ShouldConvertCoveredAvgDistance(float avgDistance, int amountJourneys, float expected)
        {
            //given
            var station = new StationLongModel
            {
                NumberOfJourneysStartingFrom = amountJourneys,
                AvgDistanceOfJourneyStartingFrom = avgDistance
            };

            //when
            var actual = station.AvgDistanceOfJourneyStartingFrom;

            //then
            Assert.AreEqual(expected, actual);
        }

        [TestCase(15000, 10, 1.5F)]
        [TestCase(0, 24, 0)]
        [TestCase(67867869.99F, 666, 101.9F)]
        public void AvgDistanceOfJourneyEndingAt_ShouldConvertCoveredAvgDistance(float avgDistance, int amountJourneys, float expected)
        {
            //given
            var station = new StationLongModel 
            {
                NumberOfJourneysEndingAt = amountJourneys,
                AvgDistanceOfJourneyEndingAt = avgDistance
            };

            //when
            var actual = station.AvgDistanceOfJourneyEndingAt;

            //then
            Assert.AreEqual(expected, actual);
        }
    }
}
