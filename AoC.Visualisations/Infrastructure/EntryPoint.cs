using AoC.Visualisations.Exceptions;
using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Infrastructure;

public static class EntryPoint
{
    [STAThread]
    private static void Main(string[] arguments)
    {
        if (arguments.Length is < 1 or > 2)
        {
            throw new VisualisationParameterException("Please specify the visualisation to run, format: year.day.part, e.g. 2022.12.2.");
        }

        string year;

        string day;

        string part;

        try
        {
            year = arguments[0][..4];

            day = arguments[0][5..7];

            part = arguments[0][8].ToString();

            if (! int.TryParse(year, out _) || ! int.TryParse(day, out _) || ! int.TryParse(part, out _))
            {
                throw new VisualisationParameterException();
            }
        }
        catch
        {
            throw new VisualisationParameterException("Unable to parse required visualisation parameter, format: year.day.part, e.g. 2021.05.2.");
        }

        var visualisationClass = $"AoC.Visualisations.Visualisations._{year}._{day}.Visualisation";

        Game visualisation;

        try
        {
            // ReSharper disable once PossibleNullReferenceException - that's what the try... catch is for.
            visualisation = (Game) Activator.CreateInstance(null, visualisationClass).Unwrap();
        }
        catch (Exception exception)
        {
            throw new VisualisationParameterException($"Unable to instantiate visualisation for {year}.{day}.{part}.", exception);
        }

        if (visualisation == null)
        {
            throw new VisualisationParameterException($"Unable to instantiate visualisation for {year}.{day}.{part}.");
        }

        try
        {
            ((IMultiPartVisualiser) visualisation).SetPart(int.Parse(part));
        }
        catch (Exception exception)
        {
            throw new VisualisationParameterException($"Unable to call SetPart on visualisation for {year}.{day}.{part}.", exception);
        }

        if (arguments.Length == 2)
        {
            try
            {
                ((IRecordableVisualiser) visualisation).OutputAviPath = arguments[1];
            }
            catch (Exception exception)
            {
                throw new VisualisationParameterException($"Unable to set OutputAviPath on visualisation for {year}.{day}.{part}.", exception);
            }
        }

        try
        {
            visualisation.Run();
        }
        finally
        {
            visualisation.Dispose();
        }
    }
}