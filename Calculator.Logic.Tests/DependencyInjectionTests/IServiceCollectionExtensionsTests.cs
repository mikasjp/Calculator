using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Calculator.Logic.Abstractions;
using Calculator.Logic.Extensions;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Calculator.Logic.Tests.DependencyInjectionTests
{
    public class IServiceCollectionExtensionsTests : TestBase
    {
        [Fact]
        public void AddCalculator_Should_Succeed()
        {
            // Arrange & Act
            var serviceProvider = new ServiceCollection()
                .AddCalculator<TestCalculator>()
                .BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<ICalculator>();

            // Assert
            service.Should().NotBeNull();
            service.Should().BeOfType<TestCalculator>();
        }

        [Fact]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void AddCalculator_Should_Fail()
        {
            // Arrange
            IServiceCollection serviceProvider = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(() => serviceProvider.AddCalculator<TestCalculator>());
        }

        [Fact]
        public void AddEquationValidator_Should_Succeed()
        {
            // Arrange & Act
            var serviceProvider = new ServiceCollection()
                .AddEquationValidator<TestEquationValidator>()
                .BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<IEquationValidator>();

            // Assert
            service.Should().NotBeNull();
            service.Should().BeOfType<TestEquationValidator>();
        }

        [Fact]
        [SuppressMessage("ReSharper", "AssignNullToNotNullAttribute")]
        public void AddEquationValidator_Should_Fail()
        {
            // Arrange
            IServiceCollection serviceProvider = null;

            // Act & Assert
            Assert.Throws<ArgumentNullException>(()
                => serviceProvider.AddEquationValidator<TestEquationValidator>());
        }
        
        [Fact]
        public void AddCustomLogger_Should_Succeed()
        {
            // Arrange & Act
            var configurationStub = new ConfigurationBuilder().Build();
            var serviceProvider = new ServiceCollection()
                .AddCustomLogger(configurationStub)
                .BuildServiceProvider();
            var service = serviceProvider.GetRequiredService<ILogger<object>>();

            // Assert
            service.Should().NotBeNull();
            service.Should().BeOfType<Logger<object>>();
        }

        public static IEnumerable<object[]> AddCustomLogger_Should_Fail_TestData()
        {
            yield return new object[] { null, null };
            yield return new object[] { null, new ConfigurationBuilder().Build() };
            yield return new object[] { new ServiceCollection(), null };
        }
        
        [Theory]
        [MemberData(nameof(AddCustomLogger_Should_Fail_TestData), DisableDiscoveryEnumeration = true)]
        public void AddCustomLogger_Should_Fail(IServiceCollection serviceCollection, IConfiguration configuration)
        {
            // Act & Assert
            Assert.Throws<ArgumentNullException>(()
                => serviceCollection.AddCustomLogger(configuration));
        }
    }
}