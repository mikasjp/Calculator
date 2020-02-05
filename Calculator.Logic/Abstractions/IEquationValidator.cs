namespace Calculator.Logic.Abstractions
{
    public interface IEquationValidator
    {
        void EnsureEquationIsValid(string equation);
    }
}