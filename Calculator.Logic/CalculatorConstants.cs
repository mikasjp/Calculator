using System.Diagnostics.CodeAnalysis;
using JetBrains.Annotations;

namespace Calculator.Logic
{
    [ExcludeFromCodeCoverage]
    [PublicAPI]
    public static class CalculatorConstants
    {
        public static string ConfigurationSectionName
            => "Calculator";
    }
}