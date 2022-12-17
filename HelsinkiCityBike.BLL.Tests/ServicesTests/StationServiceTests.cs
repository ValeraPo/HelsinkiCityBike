using AutoMapper;
using HelsinkiCityBike.BLL.Configurations;
using HelsinkiCityBike.BLL.Exceptions;
using HelsinkiCityBike.BLL.Models;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL.Entities;
using HelsinkiCityBike.DAL.Repositories;
using Moq;
using NUnit.Framework;

namespace HelsinkiCityBike.BLL.Tests
{
    public class StationServiceTests
    {
        private readonly IMapper _autoMapper;
        private Mock<IStationRepository> _stationRepositoryMock;
        private StationService _sut;

        public StationServiceTests()
        {
            _autoMapper = new Mapper(
                new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperBLL>()));
        }

        [SetUp]
        public void Setup()
        {
            _stationRepositoryMock = new Mock<IStationRepository>();
            _sut = new StationService(_stationRepositoryMock.Object, _autoMapper);
        }

        [Test]
        public async Task GetAllStations_ShouldReturnListOfStations()
        {
            //given
            var output = new List<Station>
            {
               new Station
               {
                   Name = "Kaivopuisto",
                   Address = "Havstorget 1",
                   NumberOfJourneysStartingFrom = 23802,
                   NumberOfJourneysEndingAt = 24288
               }
            };
            var expected = new List<StationShortModel>
            {
               new StationShortModel
               {
                   Name = "Kaivopuisto",
                   Address = "Havstorget 1"
               }
            };
            _stationRepositoryMock
                .Setup(m => m.GetAllStations())
                .ReturnsAsync(output);

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
            var name = "Kaivopuisto";
            var avgDistance = 1500F;
            var topStations = new List<StationShortModel>
            {
               new StationShortModel
               {
                   Name = "Unioninkatu",
                   Address = "Södra esplanaden 1"
               }
            };
            var topStationsOutput = new List<Station>
            {
               new Station
               {
                   Name = "Unioninkatu",
                   Address = "Södra esplanaden 1"
               }
            };
            var output = new Station
            {
                Name = name,
                Address = "Havstorget 1",
                NumberOfJourneysStartingFrom = 23802,
                NumberOfJourneysEndingAt = 24288
            };
            var expected = new StationLongModel
            {
                Name = name,
                Address = "Havstorget 1",
                NumberOfJourneysStartingFrom = 23802,
                NumberOfJourneysEndingAt = 24288,
                AvgDistanceOfJourneyStartingFrom = avgDistance,
                AvgDistanceOfJourneyEndingAt= avgDistance,
                TopDepartureStations = topStations,
                TopReturnStations = topStations 
            };
            _stationRepositoryMock.Setup(m => m.GetIdByName(name)).ReturnsAsync(id);
            _stationRepositoryMock.Setup(m => m.GetStationById(id)).ReturnsAsync(output);
            _stationRepositoryMock.Setup(m => m.GetTopDepartureStations(id)).ReturnsAsync(topStationsOutput);
            _stationRepositoryMock.Setup(m => m.GetTopReturnStations(id)).ReturnsAsync(topStationsOutput);
            _stationRepositoryMock
                .Setup(m => m.GetSumOfDistance(id, "Departure station id"))
                .ReturnsAsync(avgDistance);
            _stationRepositoryMock
                .Setup(m => m.GetSumOfDistance(id, "Return station id"))
                .ReturnsAsync(avgDistance);


            //when
            var actual = await _sut.GetStationByName(name);

            //then
            _stationRepositoryMock.Verify(m => m.GetIdByName(name), Times.Once);
            _stationRepositoryMock.Verify(m => m.GetStationById(id), Times.Once);
            _stationRepositoryMock.Verify(m => m.GetTopDepartureStations(id), Times.Once);
            _stationRepositoryMock.Verify(m => m.GetTopReturnStations(id), Times.Once);
            _stationRepositoryMock.Verify(m => m.GetSumOfDistance(id, "Departure station id"), Times.Once);
            _stationRepositoryMock.Verify(m => m.GetSumOfDistance(id, "Return station id"), Times.Once);
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
            _stationRepositoryMock.Verify(m => m.GetTopDepartureStations(It.IsAny<int>()), Times.Never);
            _stationRepositoryMock.Verify(m => m.GetTopReturnStations(It.IsAny<int>()), Times.Never);
            _stationRepositoryMock.Verify(m => m.GetSumOfDistance(It.IsAny<int>(), It.IsAny<string>()), Times.Never);
            Assert.AreEqual(expected, actual);
        }
    }
}
