using System;
using System.Text.RegularExpressions;
using Calculator.Logic.Abstractions;
using JetBrains.Annotations;
using Microsoft.Extensions.Logging;

namespace Calculator.Logic.Implementations.Validators
{
    public class RegexEquationValidator : IEquationValidator
    {
        private readonly ILogger<RegexEquationValidator> _logger;

        public RegexEquationValidator(
            [NotNull] ILogger<RegexEquationValidator> logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }
        
        public void EnsureEquationIsValid([NotNull] string equation)
        {
            if (equation == null) throw new ArgumentNullException(nameof(equation));

            const string regexPattern = @"^([\-]?\d+)([\+\-\*\/]\-?\d+)*$";
            
            _logger.LogDebug("Validating equation '{Equation}' with '{RegexPattern}' regex pattern",
                equation, regexPattern);
            
            var regex = new Regex(regexPattern);
            if (!regex.IsMatch(equation))
            {
                throw new ArgumentException($"'{equation}' is invalid equation");
            }
            _logger.LogDebug("'{Equation}' is valid equation",
                equation);
        }
    }
}