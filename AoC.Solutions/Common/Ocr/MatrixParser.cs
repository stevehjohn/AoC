namespace AoC.Solutions.Common.Ocr;

public class MatrixParser
{
    private readonly List<CharacterTemplate> _templates = new();

    public MatrixParser()
    {
        var templateData = File.ReadAllLines($"Common{Path.DirectorySeparatorChar}Ocr{Path.DirectorySeparatorChar}OcrTemplate.txt");

        var line = 0;

        while (line < templateData.Length)
        {
            var character = templateData[line][0];

            line += 2;

            var template = templateData.Skip(line).Take(6).ToArray();

            var characterTemplate = new CharacterTemplate(character, template);

            _templates.Add(characterTemplate);

            line += 7;
        }
    }

    public string Parse(string input)
    {
        // Note: Input may not be an exact "round" length due to missing blank segments.

        return input;
    }
}