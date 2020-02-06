# Calculator application

## 1. Requirements

* .NET Core 3.1

## 2. Usage

In repository root type:

```
dotnet run --project Calculator.App/Calculator.App.csproj equation1 equation2 equationx
```

where equation1, equation2, equationx are equations to be calculated. For example:

```
dotnet run --project Calculator.App/Calculator.App.csproj 4+5*2 4+5/2 4+5/2-1
```

To calculate more equations just pass more parameters.

Alternatively, You can build the entire solution and use the compiled application.

```
dotnet build -c release -o Release
```

# 3. Configuration

Application configuration is stored in [`appSettings.json`](Calculator.App/appSettings.json) file.

# 3.1 Application settings

Application settings are stored in `Calculator` section.

There are three calculator types supported. You can switch between them by changing `CalculatorTypeName` value in `Calculator` section. Supported values:

* `Calculator.Logic.Implementations.Calculators.SimpleCalculator` - pure, native calculator build on top of few Linq expressions.
* `Calculator.Logic.Implementations.Calculators.RoslynCalculator` - calculator based on [`Microsoft.CodeAnalysis.CSharp.Scripting`](https://www.nuget.org/packages/Microsoft.CodeAnalysis.CSharp.Scripting/) package features. Equations are evaluated as C# code.

* `Calculator.Logic.Implementations.Calculators.ExternalLibraryCalculator` - calculator based on [`MathParser.org-mXparser`](https://www.nuget.org/packages/MathParser.org-mXparser/) package.

# 3.2 Logging settings

Logger settings are stored in `Serilog` section.

As You may expect, application logging engine is based on [`Serilog`](https://www.nuget.org/packages/Serilog/2.9.1-dev-01154) package. You can configure it as it is referenced in [Serilog documentation](https://github.com/serilog/serilog-settings-configuration).

I especially encourage you to change the default value `MinimumLevel` to `Debug` to see what is happening unther the hood.

# 4. A little bit of the technical details

The application was developed to be modular and configurable. The application has been divided into two layers:

* Logic Layer - the lowest layer which implements whole application logic.
* Application Layer - The highest layer, which uses the functionality implemented in the logical layer.

This division enables the logic layer to be used in other applications.

To achieve modularity of the application, the dependency injection pattern was used. This enables easy extension of the application with additional types of calculators and equation validators.

Tests have been written for all key components of the logical layer. To create the tests, the XUnit framework and the Moq library were used.
