namespace AoC.Solutions.Exceptions;

public class IncorrectAnswerException : Exception
{
    public IncorrectAnswerException(string message) : base(message)
    {
    }
}