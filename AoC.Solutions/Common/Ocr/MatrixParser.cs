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
        var matrix = ConvertToMatrix(input);

        return input;
    }

    private bool[,] ConvertToMatrix(string input)
    {
        var length = input.Length;

        if (length % 30 != 0)
        {
            length += 30 - length % 30;
        }

        var matrix = new bool[length / 6, length / 5];

        var x = 0;

        var y = 0;

        for (var i = 0; i < input.Length; i++)
        {
            matrix[x, y] = input[i] == '*';

            x++;

            if (x >= matrix.GetLength(0))
            {
                x = 0;

                y++;
            }
        }

        return matrix;
    }
}