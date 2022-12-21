using HelsinkiCityBike.API.Controllers;
using HelsinkiCityBike.BLL.Exceptions;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Xml.Linq;

namespace HelsinkiCityBike.API.Tests
{
    public class JourneyControllerTests
    {
        private Mock<IJourneyService> _journeyService;
        private JourneyController _sut;

        [SetUp]
        public void Setup()
        {
            _journeyService = new Mock<IJourneyService>();
            _sut = new JourneyController(_journeyService.Object);
        }

        [Test]
        public async Task GetAllJourneys_ShouldReturnListOfJourneys()
        {
            //given
            var page = 1;
            var rows = 42;
            _journeyService
                .Setup(s => s.GetAllJourneys(page, rows))
                .ReturnsAsync(new List<Journey>());

            //when
            var actual = await _sut.GetAllJourneys(page, rows);

            //then
            _journeyService.Verify(s => s.GetAllJourneys(page, rows), Times.Once());
            Assert.IsInstanceOf(typeof(OkObjectResult), actual.Result);
        }

        [Test]
        public async Task GetAllJourneys_NumberOfPageIsTooBig_ShouldThrowArgumentOutOfRangeException()
        {
            //given
            var page = int.MaxValue;
            var rows = 42;
            var expected = "Specified argument was out of the range of valid values.";
            _journeyService
                .Setup(s => s.GetAllJourneys(page, rows))
                .Callback(() => throw new ArgumentOutOfRangeException());

            //when
            var actual = Assert
                .ThrowsAsync<ArgumentOutOfRangeException>(async () => await _sut.GetAllJourneys(page, rows))!
                .Message;

            //then
            _journeyService.Verify(s => s.GetAllJourneys(page, rows), Times.Once());
            Assert.AreEqual(expected, actual);
        }

    }
}
