using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL.Entities;
using HelsinkiCityBike.DAL.Repositories;
using Moq;
using NUnit.Framework;

namespace HelsinkiCityBike.BLL.Tests
{
    public class JourneyServiceTests
    {
        private Mock<IJourneyRepository> _journeyRepositoryMock;
        private JourneyService _sut;

        public JourneyServiceTests()
        {
            _journeyRepositoryMock = new Mock<IJourneyRepository>();
        }

        [SetUp]
        public void Setup()
        {
            _sut = new JourneyService(_journeyRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllJourneys_ShouldReturnListOfJourneys()
        {
            //given
            var expected = new List<Journey>
            {
                new Journey
                {
                    DepartureStationName = "Kaivopuisto",
                    ReturnStationName = "Design Museum",
                },
                new Journey
                {
                    DepartureStationName = "Kiasma", 
                    ReturnStationName = "Porthania",
                }
            };
            _journeyRepositoryMock
                .Setup(m => m.GetAllJourneys())
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetAllJourneys();

            //then
            _journeyRepositoryMock.Verify(m => m.GetAllJourneys(), Times.Once);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}