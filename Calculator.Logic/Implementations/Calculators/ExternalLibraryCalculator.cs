using System;
using System.Threading;
using System.Threading.Tasks;
using Calculator.Logic.Abstractions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;
using org.mariuszgromada.math.mxparser;

namespace Calculator.Logic.Implementations.Calculators
{
    public class ExternalLibraryCalculator : ICalculator
    {
        private readonly IEquationValidator _equationValidator;
        private readonly ILogger<ExternalLibraryCalculator> _logger;

        public ExternalLibraryCalculator(
            [NotNull] IEquationValidator equationValidator,
            [NotNull] ILogger<ExternalLibraryCalculator> logger)
        {
            _equationValidator = equationValidator ?? throw new ArgumentNullException(nameof(equationValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public Task<decimal> CalculateAsync([NotNull] string equation, CancellationToken cancellationToken)
        {
            if (equation == null) throw new ArgumentNullException(nameof(equation));
            _equationValidator.EnsureEquationIsValid(equation);
            
            _logger.LogDebug("Calculating '{Equation}' equation", equation);
            var calculation = new Expression(equation)
                .calculate();
            var result = Convert.ToDecimal(calculation);
            _logger.LogDebug("Result of '{Equation}' equation is '{EquationResult}'",
                equation, result);
            return Task.FromResult(result);
        }
    }
}