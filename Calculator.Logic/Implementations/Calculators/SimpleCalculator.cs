using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Calculator.Logic.Abstractions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Calculator.Logic.Implementations.Calculators
{
    public class SimpleCalculator : ICalculator
    {
        private readonly IEquationValidator _equationValidator;
        private readonly ILogger<SimpleCalculator> _logger;

        public SimpleCalculator(
            [NotNull] IEquationValidator equationValidator,
            [NotNull] ILogger<SimpleCalculator> logger)
        {
            _equationValidator = equationValidator ?? throw new ArgumentNullException(nameof(equationValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public Task<decimal> CalculateAsync([NotNull] string equation, CancellationToken cancellationToken)
        {
            if (equation == null) throw new ArgumentNullException(nameof(equation));
            _equationValidator.EnsureEquationIsValid(equation);
            
            _logger.LogDebug("Calculating '{Equation}' equation", equation);
            var regex = new Regex(@"(\d)(-)");
            var result = regex.Replace(equation, "$1+-")
                .Split('+')
                .Select(x => x.Split('/')
                    .Select(y => y
                        .Split('*')
                        .Select(decimal.Parse)
                        .Aggregate(decimal.Multiply))
                    .Aggregate(decimal.Divide)
                )
                .Sum();
            _logger.LogDebug("Result of '{Equation}' equation is '{EquationResult}'",
                equation, result);
            return Task.FromResult(result);
        }
    }
}