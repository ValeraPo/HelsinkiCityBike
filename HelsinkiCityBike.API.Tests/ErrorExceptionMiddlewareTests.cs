using HelsinkiCityBike.API.Infrastructure;
using HelsinkiCityBike.API.Models;
using HelsinkiCityBike.BLL.Exceptions;
using Microsoft.AspNetCore.Http;
using NUnit.Framework;
using System.Text.Json;

namespace HelsinkiCityBike.API.Tests
{
    public class ErrorExceptionMiddlewareTests
    {
        private DefaultHttpContext _defaultContext;
        private const string ExceptionMassage = "Exception massage";

        [SetUp]
        public void Setup()
        {
            _defaultContext = new DefaultHttpContext
            {
                Response = { Body = new MemoryStream() },
                Request = { Path = "/" }
            };
        }

        [Test]
        public void Invoke_ValidRequestReceived_ShouldResponse()
        {
            //given
            const string expectedOutput = "Request handed over to next request delegate";
            var middlewareInstance = new ErrorExceptionMiddleware(innerHttpContext =>
            {
                innerHttpContext.Response.WriteAsync(expectedOutput);
                return Task.CompletedTask;
            });

            //when
            middlewareInstance.Invoke(_defaultContext);

            //then
            var actual = GetResponseBody();
            Assert.AreEqual(expectedOutput, actual);
        }

        [Test]
        public void Invoke_WhenThrowUriFormatException_ShouldExceptionResponseModel()
        {
            //given
            var expected = GetJsonExceptionResponseModel(400);
            var middlewareInstance = new ErrorExceptionMiddleware(_ => throw new MissingEntryException(ExceptionMassage));

            //when
            middlewareInstance.Invoke(_defaultContext);

            //then
            var actual = GetResponseBody();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Invoke_WhenThrowTimeoutException_ShouldExceptionResponseModel()
        {
            //given
            var expected = GetJsonExceptionResponseModel(504);
            var middlewareInstance = new ErrorExceptionMiddleware(_ => throw new TimeoutException(ExceptionMassage));

            //when
            middlewareInstance.Invoke(_defaultContext);

            //then
            var actual = GetResponseBody();
            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void Invoke_WhenOtherException_ShouldExceptionResponseModel()
        {
            //given
            var expected = GetJsonExceptionResponseModel(500);
            var middlewareInstance = new ErrorExceptionMiddleware(_ => throw new Exception(ExceptionMassage));

            //when
            middlewareInstance.Invoke(_defaultContext);

            //then
            var actual = GetResponseBody();
            Assert.AreEqual(expected, actual);
        }

        private string GetResponseBody()
        {
            _defaultContext.Response.Body.Seek(0, SeekOrigin.Begin);
            return new StreamReader(_defaultContext.Response.Body).ReadToEnd();
        }

        private static string GetJsonExceptionResponseModel(int statusCode) =>
            JsonSerializer.Serialize(new ExceptionResponse { Code = statusCode, Message = ExceptionMassage });

    }
}