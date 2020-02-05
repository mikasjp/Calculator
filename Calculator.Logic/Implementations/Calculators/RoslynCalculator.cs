using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Calculator.Logic.Abstractions;
using JetBrains.Annotations;
using Microsoft.CodeAnalysis.CSharp.Scripting;
using Microsoft.Extensions.Logging;

namespace Calculator.Logic.Implementations.Calculators
{
    public class RoslynCalculator : ICalculator
    {
        private readonly IEquationValidator _equationValidator;
        private readonly ILogger<RoslynCalculator> _logger;

        public RoslynCalculator(
            [NotNull] IEquationValidator equationValidator,
            [NotNull] ILogger<RoslynCalculator> logger)
        {
            _equationValidator = equationValidator ?? throw new ArgumentNullException(nameof(equationValidator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public Task<decimal> CalculateAsync([NotNull] string equation, CancellationToken cancellationToken)
        {
            if (equation == null) throw new ArgumentNullException(nameof(equation));
            _equationValidator.EnsureEquationIsValid(equation);

            _logger.LogDebug("Calculating '{Equation}' equation", equation);
            const string regexPattern = @"(\d+)";
            var regex = new Regex(regexPattern);
            var equationCode = regex.Replace(equation, "$1m");
            _logger.LogDebug(
                "Converted equation to '{RoslynEquationCode}' code using '{RoslynRegexPattern}' regex pattern",
                equationCode, regexPattern);
            return CSharpScript
                .EvaluateAsync<decimal>(equationCode, cancellationToken: cancellationToken);
        }
    }
}