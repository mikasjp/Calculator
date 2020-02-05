using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;

namespace Calculator.Logic.Tests.EquationVerifiersTests
{
    [ExcludeFromCodeCoverage]
    public class EquationValidatorTestBase : TestBase
    {
        public static IEnumerable<object[]> InvalidEquations()
        {
            yield return new object[] {string.Empty};
            yield return new object[] {Environment.NewLine};
            yield return new object[] {"Test"};
            yield return new object[] {"-"};
            yield return new object[] {"1+"};
            yield return new object[] {"1-"};
            yield return new object[] {"1*"};
            yield return new object[] {"1/"};
            yield return new object[] {"-1+"};
            yield return new object[] {"-1-"};
            yield return new object[] {"-1*"};
            yield return new object[] {"-1/"};
        }
    }
}