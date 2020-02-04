using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Calculator.Logic.Abstractions;
using Moq;

namespace Calculator.Logic.Tests.CalculatorsTests
{
    [ExcludeFromCodeCoverage]
    public class CalculatorTestBase : TestBase
    {
        protected static Mock<IEquationValidator> CreateEquationValidatorMock()
        {
            var equationValidatorMock = new Mock<IEquationValidator>(MockBehavior.Strict);
            equationValidatorMock.Setup(x
                    => x.EnsureEquationIsValid(It.IsAny<string>()))
                .Verifiable();
            return equationValidatorMock;
        }

        public static IEnumerable<object[]> ValidEquations()
        {
            yield return new object[] {"4+5*2", 14m};
            yield return new object[] {"4+5/2", 6.5m};
            yield return new object[] {"4+5/2-1", 5.5m};
        }
    }
}