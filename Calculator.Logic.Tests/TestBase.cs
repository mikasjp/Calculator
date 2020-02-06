using Microsoft.Extensions.Logging;
using Moq;

namespace Calculator.Logic.Tests
{
    public class TestBase
    {
        protected static Mock<ILogger<T>> CreateLoggerMock<T>()
            => new Mock<ILogger<T>>();
    }
}