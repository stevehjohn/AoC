namespace AoC.Visualisations.Exceptions;

public class VisualisationParameterException : Exception
{
    public VisualisationParameterException()
    {
    }

    public VisualisationParameterException(string message) : base(message)
    {
    }

    public VisualisationParameterException(string message, Exception innerException) : base(message, innerException)
    {
    }
}