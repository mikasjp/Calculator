using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Calculator.Logic.Abstractions;
using JetBrains.Annotations;

namespace Calculator.Logic.Tests.DependencyInjectionTests
{
    [ExcludeFromCodeCoverage]
    [UsedImplicitly]
    public class TestCalculator : ICalculator
    {
        public Task<decimal> CalculateAsync(string equation, CancellationToken cancellationToken)
            => Task.FromResult<decimal>(default);
    }
}