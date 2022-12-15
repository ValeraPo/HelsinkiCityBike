using Dapper;
using HelsinkiCityBike.DAL.Entities;
using HelsinkiCityBike.DAL.Repositories;
using Moq;
using Moq.Dapper;
using NUnit.Framework;
using System.Data.Common;

namespace HelsinkiCityBike.DAL.Tests
{
    public class JourneyRepositoryTests
    {
        private Mock<IDbContext> _dbContextMock;
        private JourneyRepository _sut;
        private readonly Mock<DbConnection> _connection;

        public JourneyRepositoryTests()
        {
            _connection = new Mock<DbConnection>();
        }

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<IDbContext>();
            _sut = new JourneyRepository(_dbContextMock.Object);
        }

        [Test]
        public async Task GetAllJourneys_ShouldReturnListOfJourneys()
        {
            //given
            var rowStart = 0;
            var rows = 2;
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
            _dbContextMock.Setup(m => m.CreateConnection()).Returns(_connection.Object);
            _connection
                .SetupDapperAsync(m => m.QueryAsync<Journey>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetAllJourneys(rowStart, rows);

            //then
            _dbContextMock.Verify(m => m.CreateConnection(), Times.Once);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetAmountOfJourneys_ShouldReturnAmountOfJourneys()
        {
            //given
            var expected = 42;
            _dbContextMock.Setup(m => m.CreateConnection()).Returns(_connection.Object);
            _connection
                .SetupDapperAsync(m => m.QueryFirstOrDefaultAsync<int>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetAmountOfJourneys();

            //then
            _dbContextMock.Verify(m => m.CreateConnection(), Times.Once);
            Assert.AreEqual(expected, actual);
        }
    }
}