using AoC.Visualisations.Exceptions;

namespace AoC.Visualisations.Infrastructure;

public static class EntryPoint
{
    [STAThread]
    private static void Main(string[] arguments)
    {
        if (arguments.Length != 1)
        {
            throw new VisualisationParameterException("Please specify the visualisation to run, format: year.day.part, e.g. 2021.05.2.");
        }

        string year;

        string day;

        string part;

        try
        {
            year = arguments[0][..4];

            day = arguments[0][5..7];

            part = arguments[0][8].ToString();

            if (! int.TryParse(year, out _))
            {
                throw new VisualisationParameterException();
            }

            if (! int.TryParse(day, out _))
            {
                throw new VisualisationParameterException();
            }

            if (! int.TryParse(part, out _))
            {
                throw new VisualisationParameterException();
            }
        }
        catch
        {
            throw new VisualisationParameterException("Unable to parse required visualisation parameter, format: year.day.part, e.g. 2021.05.2.");
        }

        var visualisationClass = $"AoC.Visualisations.Visualisations._{year}._{day}.Visualisation";

        VisualisationBase visualisation;

        try
        {
            // ReSharper disable once PossibleNullReferenceException - that's what the try... catch is for.
            visualisation = (VisualisationBase) Activator.CreateInstance(null, visualisationClass).Unwrap();
        }
        catch
        {
            throw new VisualisationParameterException($"Unable to find visualisation for {year}.{day}.{part}.");
        }

        if (visualisation == null)
        {
            throw new VisualisationParameterException($"Unable to find visualisation for {year}.{day}.{part}.");
        }

        try
        {
            visualisation.SetPart(int.Parse(part));

            visualisation.Run();
        }
        finally
        {
            visualisation.Dispose();
        }
    }
}