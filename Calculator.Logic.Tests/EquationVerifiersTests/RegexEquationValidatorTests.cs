using System;
using System.Diagnostics.CodeAnalysis;
using Calculator.Logic.Implementations.Validators;
using Xunit;

namespace Calculator.Logic.Tests.EquationVerifiersTests
{
    public class RegexEquationValidatorTests : EquationValidatorTestBase
    {
        [Theory]
        [MemberData(nameof(InvalidEquations))]
        public void EnsureEquationIsValid_Should_Throw(string equation)
        {
            // Arrange
            var loggerMock = CreateLoggerMock<RegexEquationValidator>();
            var validator = new RegexEquationValidator(loggerMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentException>(() => validator.EnsureEquationIsValid(equation));
        }

        [Fact]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void EnsureEquationIsValid_Should_Fail()
        {
            // Arrange
            var loggerMock = CreateLoggerMock<RegexEquationValidator>();
            var validator = new RegexEquationValidator(loggerMock.Object);

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => validator.EnsureEquationIsValid(null));
        }
    }
}