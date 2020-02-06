using System.Threading;
using System.Threading.Tasks;

namespace Calculator.Logic.Abstractions
{
    public interface ICalculator
    {
        Task<decimal> CalculateAsync(string equation, CancellationToken cancellationToken);
    }
}