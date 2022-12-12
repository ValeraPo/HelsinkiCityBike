using HelsinkiCityBike.BLL.Exceptions;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL.Entities;
using HelsinkiCityBike.DAL.Repositories;
using Moq;
using NUnit.Framework;

namespace HelsinkiCityBike.BLL.Tests
{
    public class StationServiceTests
    {
        private Mock<IStationRepository> _stationRepositoryMock;
        private StationService _sut;

        [SetUp]
        public void Setup()
        {
            _stationRepositoryMock = new Mock<IStationRepository>();
            _sut = new StationService(_stationRepositoryMock.Object);
        }

        [Test]
        public async Task GetAllStations_ShouldReturnListOfStations()
        {
            //given
            var expected = new List<Station>
            {
               new Station
               {
                   Name = "Kaivopuisto",
                   Address = "Havstorget 1",
                   NumberOfJourneysStartingFrom = 23802,
                   NumberOfJourneysEndingAt = 24288
               }
            };
            _stationRepositoryMock
                .Setup(m => m.GetAllStations())
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetAllStations();

            //then
            _stationRepositoryMock.Verify(m => m.GetAllStations(), Times.Once);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetStationByName_ShouldReturnStation()
        {
            //given
            var id = 1;
            var expected = new Station
            {
                Name = "Kaivopuisto",
                Address = "Havstorget 1",
                NumberOfJourneysStartingFrom = 23802,
                NumberOfJourneysEndingAt = 24288
            };
            _stationRepositoryMock
                .Setup(m => m.GetIdByName(expected.Name))
                .ReturnsAsync(id);
            _stationRepositoryMock
                .Setup(m => m.GetStationById(id))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetStationByName(expected.Name);

            //then
            _stationRepositoryMock.Verify(m => m.GetIdByName(expected.Name), Times.Once);
            _stationRepositoryMock.Verify(m => m.GetStationById(id), Times.Once);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetStationByName_WrongName_ShouldThrowMissingEntryException()
        {
            //given
            var name = "WrongName";
            var expected = $"Station '{name}' not found";
            _stationRepositoryMock
                .Setup(m => m.GetIdByName(name))
                .ReturnsAsync(default(int));

            //when
            var actual = Assert
                .ThrowsAsync<MissingEntryException>(async () => await _sut.GetStationByName(name))!
                .Message;

            //then
            _stationRepositoryMock.Verify(m => m.GetIdByName(name), Times.Once);
            _stationRepositoryMock.Verify(m => m.GetStationById(It.IsAny<int>()), Times.Never);
            Assert.AreEqual(expected, actual);
        }
    }
}
