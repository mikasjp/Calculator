using System;
using System.Linq;
using System.Threading.Tasks;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Calculator.Logic.Abstractions;
using Calculator.Logic.Extensions;
using Calculator.Logic.Implementations.Validators;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Calculator.App
{
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        private static IConfiguration Configuration
            => new ConfigurationBuilder()
                .AddJsonFile("appSettings.json", false)
                .Build();

        private static IServiceProvider ServiceProvider
            => new ServiceCollection()
                .AddCustomLogger(Configuration)
                .AddCalculator(Configuration)
                .AddEquationValidator<RegexEquationValidator>()
                .BuildServiceProvider();

        private static ICalculator Calculator
            => ServiceProvider.GetRequiredService<ICalculator>();

        private static async Task Main(string[] args)
        {
            if (!args.Any())
                throw new ArgumentException("Provide at least one equation to be calculated");

            var results = args
                .Select(x => Calculator.CalculateAsync(x, CancellationToken.None))
                .ToList();

            await Task.WhenAll(results);

            foreach (var (result, equation) in results.Zip(args))
            {
                Console.WriteLine($"{equation} = {await result}");
            }
        }
    }
}