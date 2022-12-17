using HelsinkiCityBike.API.Controllers;
using HelsinkiCityBike.BLL.Exceptions;
using HelsinkiCityBike.BLL.Models;
using HelsinkiCityBike.BLL.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace HelsinkiCityBike.API.Tests
{
    public class StationControllerTests
    {
        private Mock<IStationService> _stationService;
        private StationController _sut;

        [SetUp]
        public void Setup()
        {
            _stationService = new Mock<IStationService>();
            _sut = new StationController(_stationService.Object);
        }

        [Test]
        public async Task GetAllStations_ShouldReturnListOfStations()
        {
            //given
            _stationService
                .Setup(s => s.GetAllStations())
                .ReturnsAsync(new List<StationShortModel>());

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
            var name = "Kaivopuisto";
            _stationService
                .Setup(s => s.GetStationByName(name))
                .ReturnsAsync(new StationLongModel());

            //when
            var actual = await _sut.GetStationByName(name);

            //then
            _stationService.Verify(s => s.GetStationByName(name), Times.Once());
            Assert.IsInstanceOf(typeof(OkObjectResult), actual.Result);
        }

        [Test]
        public async Task GetStationByName_WrongName_ShouldThrowMissingEntryException()
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
