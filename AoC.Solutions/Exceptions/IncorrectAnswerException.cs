namespace AoC.Exceptions;

public class IncorrectAnswerException : Exception
{
    public IncorrectAnswerException(string message) : base(message)
    {
    }
}