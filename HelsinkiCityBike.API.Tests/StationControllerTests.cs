using AutoMapper;
using HelsinkiCityBike.API.Configuration;
using HelsinkiCityBike.API.Controllers;
using HelsinkiCityBike.BLL.Exceptions;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace HelsinkiCityBike.API.Tests
{
    public class StationControllerTests
    {
        private Mock<IStationService> _stationService;
        private StationController _sut;
        private IMapper _autoMapper;

        [SetUp]
        public void Setup()
        {
            _stationService = new Mock<IStationService>();
            _autoMapper = new Mapper(new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperApi>()));
            _sut = new StationController(_stationService.Object, _autoMapper);
        }

        [Test]
        public async Task GetAllStations_ShouldReturnListOfStations()
        {
            //given
            _stationService
                .Setup(s => s.GetAllStations())
                .ReturnsAsync(new List<Station>());

            //when
            var actual = await _sut.GetAllStations();

            //then
            _stationService.Verify(s => s.GetAllStations(), Times.Once());
            Assert.IsInstanceOf(typeof(OkObjectResult), actual.Result);
        }

        [Test]
        public async Task GetStationByName_ShouldReturnStation()
        {
            //given
            var expected = new Station
            {
                Name = "Kaivopuisto",
                Address = "Havstorget 1",
                NumberOfJourneysStartingFrom = 23802,
                NumberOfJourneysEndingAt = 24288
            };
            _stationService
                .Setup(s => s.GetStationByName(expected.Name))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetStationByName(expected.Name);

            //then
            _stationService.Verify(s => s.GetStationByName(expected.Name), Times.Once());
            Assert.IsInstanceOf(typeof(OkObjectResult), actual.Result);
        }

        [Test]
        public async Task GetStationByName_WrommgName_ShouldThrowMissingEntryException()
        {
            //given
            var name = "WrongName";
            var expected = $"Station '{name}' not found";
            _stationService
                .Setup(s => s.GetStationByName(name))
                .Callback(() => throw new MissingEntryException(expected));

            //when
            var actual = Assert
                .ThrowsAsync<MissingEntryException>(async () => await _sut.GetStationByName(name))!
                .Message;

            //then
            _stationService.Verify(s => s.GetStationByName(name), Times.Once());
            Assert.AreEqual(expected, actual);
        }
    }
}
