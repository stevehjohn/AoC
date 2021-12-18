using System.Text;
using AoC.Solutions.Exceptions;

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

        var result = OcrMatrix(matrix);

        return input;
    }

    private static bool[,] ConvertToMatrix(string input)
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

    private string OcrMatrix(bool[,] matrix)
    {
        var result = new StringBuilder();

        for (var x = 0; x < matrix.GetLength(0); x += 5)
        {
            result.Append(MatchCharacter(matrix, x));
        }

        return result.ToString();
    }

    private char MatchCharacter(bool[,] matrix, int position)
    {
        // TODO: This double break with flag is nasty.
        foreach (var template in _templates)
        {
            var matched = true;

            for (var y = 0; y < 6; y++)
            {
                for (var x = 0; x < 5; x++)
                {
                    // TODO: Maybe make character class do the match.
                    if (matrix[position + x, y] != template.Template[x, y])
                    {
                        matched = false;

                        break;
                    }
                }

                if (! matched)
                {
                    break;
                }
            }

            if (matched)
            {
                return template.Character;
            }
        }

        throw new PuzzleException("Character not recognised during OCR.");
    }
}