using System;
using Calculator.Logic.Abstractions;
using Calculator.Logic.Implementations;
using JetBrains.Annotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Serilog;

namespace Calculator.Logic.Extensions
{
    public static class IServiceCollectionExtensions
    {
        [PublicAPI]
        public static IServiceCollection AddCalculator<TCalculator>([NotNull] this IServiceCollection services)
            where TCalculator : class, ICalculator
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            return services.AddTransient<ICalculator, TCalculator>();
        }

        public static IServiceCollection AddCalculator([NotNull] this IServiceCollection services,
            [NotNull] IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));
            
            var calculatorConfiguration = configuration
                                              .GetSection(CalculatorConstants.ConfigurationSectionName)
                                              .Get<CalculatorConfiguration>()
                                          ?? throw new ApplicationException(
                                              $"Can't load '{CalculatorConstants.ConfigurationSectionName}' configuration section");
            
            var implementationType = typeof(ICalculator).Assembly
                .GetType(calculatorConfiguration.CalculatorTypeName
                         ?? throw new ArgumentNullException(nameof(CalculatorConfiguration.CalculatorTypeName)));

            return services.AddTransient(
                typeof(ICalculator),
                implementationType);
        }

        public static IServiceCollection AddEquationValidator<TEquationValidator>(
            [NotNull] this IServiceCollection services)
            where TEquationValidator : class, IEquationValidator
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            return services.AddTransient<IEquationValidator, TEquationValidator>();
        }

        public static IServiceCollection AddCustomLogger([NotNull] this IServiceCollection services,
            [NotNull] IConfiguration configuration)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            var logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
            return services.AddLogging(x
                => x.AddSerilog(logger));
        }
    }
}