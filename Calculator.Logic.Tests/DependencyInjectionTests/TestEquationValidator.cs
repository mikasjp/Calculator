using System.Diagnostics.CodeAnalysis;
using Calculator.Logic.Abstractions;
using JetBrains.Annotations;

namespace Calculator.Logic.Tests.DependencyInjectionTests
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class TestEquationValidator : IEquationValidator
    {
        public void EnsureEquationIsValid(string equation)
        {
        }
    }
}