using Dapper;
using HelsinkiCityBike.DAL.Entities;
using HelsinkiCityBike.DAL.Repositories;
using Moq;
using Moq.Dapper;
using NUnit.Framework;
using System.Data.Common;

namespace HelsinkiCityBike.DAL.Tests
{
    public class StationRepositoryTests
    {
        private Mock<IDbContext> _dbContextMock;
        private StationRepository _sut;
        private readonly Mock<DbConnection> _connection;

        public StationRepositoryTests()
        {
            _connection = new Mock<DbConnection>();
        }

        [SetUp]
        public void Setup()
        {
            _dbContextMock = new Mock<IDbContext>();
            _sut = new StationRepository(_dbContextMock.Object);
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
                },
                new Station
                {
                    Name = "Laivasillankatu",
                    Address = "Skeppsbrogatan 14",
                }
            };
            _dbContextMock.Setup(m => m.CreateConnection()).Returns(_connection.Object);
            _connection
                .SetupDapperAsync(m => m.QueryAsync<Station>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetAllStations();

            //then
            _dbContextMock.Verify(m => m.CreateConnection(), Times.Once);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetStationById_ShouldReturnStation()
        {
            //given
            var expected = new Station
            {
                Name = "Kaivopuisto",
                Address = "Havstorget 1",
                NumberOfJourneysStartingFrom = 23802,
                NumberOfJourneysEndingAt = 24288
            };
            _dbContextMock.Setup(m => m.CreateConnection()).Returns(_connection.Object);
            _connection
                .SetupDapperAsync(m => m.QueryFirstOrDefaultAsync<Station>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetStationById(It.IsAny<int>());

            //then
            _dbContextMock.Verify(m => m.CreateConnection(), Times.Once);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetIdByName_ShouldReturnId()
        {
            //given
            var name = "Kaivopuisto";
            var expected = 42;
            _dbContextMock.Setup(m => m.CreateConnection()).Returns(_connection.Object);
            _connection
                .SetupDapperAsync(m => m.QueryFirstOrDefaultAsync<int>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetIdByName("Kaivopuisto");

            //then
            _dbContextMock.Verify(m => m.CreateConnection(), Times.Once);
            Assert.AreEqual(expected, actual);
        }


        [Test]
        public async Task GetSumOfDistance_ShouldReturnSumOfDistance()
        {
            //given
            var expected = 42.3F;
            _dbContextMock.Setup(m => m.CreateConnection()).Returns(_connection.Object);
            _connection
                .SetupDapperAsync(m => m.QueryFirstOrDefaultAsync<float>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetSumOfDistance(It.IsAny<int>(), It.IsAny<string>());

            //then
            _dbContextMock.Verify(m => m.CreateConnection(), Times.Once);
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetTopReturnStations_ShouldReturnListOfStations()
        {
            //given
            var expected = new List<Station>
            {
                new Station
                {
                    Name = "Kaivopuisto",
                    Address = "Havstorget 1",
                },
                new Station
                {
                    Name = "Laivasillankatu",
                    Address = "Skeppsbrogatan 14",
                }
            };
            _dbContextMock.Setup(m => m.CreateConnection()).Returns(_connection.Object);
            _connection
                .SetupDapperAsync(m => m.QueryAsync<Station>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetTopReturnStations(It.IsAny<int>());

            //then
            _dbContextMock.Verify(m => m.CreateConnection(), Times.Once);
            CollectionAssert.AreEqual(expected, actual);
        }

        [Test]
        public async Task GetTopDepartureStations_ShouldReturnListOfStations()
        {
            //given
            var expected = new List<Station>
            {
                new Station
                {
                    Name = "Kaivopuisto",
                    Address = "Havstorget 1",
                },
                new Station
                {
                    Name = "Laivasillankatu",
                    Address = "Skeppsbrogatan 14",
                }
            };
            _dbContextMock.Setup(m => m.CreateConnection()).Returns(_connection.Object);
            _connection
                .SetupDapperAsync(m => m.QueryAsync<Station>(It.IsAny<string>(), null, null, null, null))
                .ReturnsAsync(expected);

            //when
            var actual = await _sut.GetTopDepartureStations(It.IsAny<int>());

            //then
            _dbContextMock.Verify(m => m.CreateConnection(), Times.Once);
            CollectionAssert.AreEqual(expected, actual);
        }
    }
}