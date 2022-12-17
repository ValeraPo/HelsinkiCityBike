using HelsinkiCityBike.DAL.Entities;
using NUnit.Framework;

namespace HelsinkiCityBike.DAL.Tests
{
    public class JourneyTests
    {
        [TestCase(1500, 1.5F)]
        [TestCase(0, 0)]
        [TestCase(6789.99F, 6.79F)]
        public void CoveredDistanceTest_ShouldConvertCoveredDistance(float distance, float expected)
        {
            //given
            var journey = new Journey { CoveredDistance = distance };

            //when
            var actual = journey.CoveredDistance;

            //then
            Assert.AreEqual(expected, actual);
        }

        [TestCase(60, 1)]
        [TestCase(0, 0)]
        [TestCase(876, 14.6F)]
        public void DurationTest_ShouldConvertCoveredDuration(int duration, float expected)
        {
            //given
            var journey = new Journey { Duration = duration };

            //when
            var actual = journey.Duration;

            //then
            Assert.AreEqual(expected, actual);
        }
    }
}
