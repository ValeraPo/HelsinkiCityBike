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

       

        [SetUp]
        public void Setup()
        {
            _journeyRepositoryMock = new Mock<IJourneyRepository>();
            _sut = new JourneyService(_journeyRepositoryMock.Object);
        }

        [TestCase(1, 2, 0)]
        [TestCase(2, 10, 10)]
        public async Task GetAllJourneys_ShouldReturnListOfJourneys(int page, int rows, int firstRow)
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
                .Setup(m => m.GetAllJourneys(firstRow, rows))
                .ReturnsAsync(expected);
            _journeyRepositoryMock
                .Setup(m => m.GetAmountOfJourneys())
                .ReturnsAsync(1000);

            //when
            var actual = await _sut.GetAllJourneys(page, rows);

            //then
            _journeyRepositoryMock.Verify(m => m.GetAmountOfJourneys(), Times.Once);
            _journeyRepositoryMock.Verify(m => m.GetAllJourneys(firstRow, rows), Times.Once);
            CollectionAssert.AreEqual(expected, actual); 
        }

        [Test]
        public async Task GetAllJourneys_NumberOfPageIsTooBig_ShouldThrowArgumentOutOfRangeException()
        {
            //given
            var page = 5;
            var rows = 100;
            var amount = 200;
            var expected = "Specified argument was out of the range of valid values. " +
                           "(Parameter 'The page you are trying to access is out of bounds.')";
            _journeyRepositoryMock
                .Setup(m => m.GetAmountOfJourneys())
                .ReturnsAsync(amount);

            //when
            var actual = Assert
                .ThrowsAsync<ArgumentOutOfRangeException>(async () => await _sut.GetAllJourneys(page, rows))!
                .Message;

            //then
            _journeyRepositoryMock.Verify(m => m.GetAmountOfJourneys(), Times.Once);
            _journeyRepositoryMock.Verify(m => m.GetAllJourneys(It.IsAny<int>(), It.IsAny<int>()), Times.Never);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetAmountOfJourneys_ShouldReturnAmountOfJourneys()
        {
            //given
            var expected = 42;
            _journeyRepositoryMock
                .Setup(s => s.GetAmountOfJourneys())
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetAmountOfJourneys();

            //then
            _journeyRepositoryMock.Verify(s => s.GetAmountOfJourneys(), Times.Once());
            Assert.AreEqual(expected, actual);
        }
    }
}