using HelsinkiCityBike.API.Controllers;
using HelsinkiCityBike.BLL.Services;
using HelsinkiCityBike.DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

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
            _journeyService
                .Setup(s => s.GetAllJourneys())
                .ReturnsAsync(new List<Journey>());

            //when
            var actual = await _sut.GetAllJourneys();

            //then
            _journeyService.Verify(s => s.GetAllJourneys(), Times.Once());
            Assert.IsInstanceOf(typeof(OkObjectResult), actual.Result);
        }
    }
}
