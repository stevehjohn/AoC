using System.Diagnostics.CodeAnalysis;

namespace AoC.Solutions.Exceptions;

[ExcludeFromCodeCoverage]
public class PuzzleException : Exception
{
    public PuzzleException(string message) : base(message)
    {
    }
}