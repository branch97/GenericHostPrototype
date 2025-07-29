using Microsoft.Extensions.Logging;
using Moq;

namespace GenericHostPrototype.Tests
{
    public class MyWorkerServiceTests
    {
        [Fact]
        public void DoWork_LogsInformation()
        {
            // Arrange
            var loggerMock = new Mock<ILogger<MyWorkerService>>();
            var service = new MyWorkerService(loggerMock.Object);

            // Act
            service.DoWork();

            // Assert
            loggerMock.Verify(
                x => x.Log(
                    LogLevel.Information,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => v.ToString().Contains("MyService is doing work")),
                    null,
                    It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
                Times.Once);
        }
    }
}
