using System;
using System.Threading;
using System.Threading.Tasks;
using Calculator.Logic.Abstractions;
using Calculator.Logic.Implementations.Calculators;
using FluentAssertions;
using Moq;
using Xunit;

namespace Calculator.Logic.Tests.CalculatorsTests
{
    public class SimpleCalculatorTests : CalculatorTestBase
    {
        [Theory]
        [MemberData(nameof(ValidEquations))]
        public async Task Calculate_Should_Succeed(string equation, decimal expected)
        {
            // Arrange
            var equationValidatorMock = CreateEquationValidatorMock();
            var loggerMock = CreateLoggerMock<SimpleCalculator>();
            ICalculator calculator = new SimpleCalculator(
                equationValidatorMock.Object,
                loggerMock.Object);

            // Act
            var result = await calculator.CalculateAsync(equation, CancellationToken.None);

            // Assert
            result.Should().Be(expected);
            equationValidatorMock.Verify(x
                => x.EnsureEquationIsValid(It.IsAny<string>()), Times.Once);
        }

        [Fact]
        public async Task Calculate_Should_Throw()
        {
            // Arrange
            var equationValidatorMock = CreateEquationValidatorMock();
            var loggerMock = CreateLoggerMock<SimpleCalculator>();
            ICalculator calculator = new SimpleCalculator(
                equationValidatorMock.Object,
                loggerMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() =>
                calculator.CalculateAsync(null, CancellationToken.None));
        }
    }
}