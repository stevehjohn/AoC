using System.Text;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Common.Ocr;

public class MatrixParser
{
    private readonly List<CharacterTemplate> _templates = [];

    private readonly int _width;

    private readonly int _height;

    public MatrixParser(Variant variant)
    {
        string[] templateData = null;

        try
        {
            templateData = File.ReadAllLines($"Common{Path.DirectorySeparatorChar}Ocr{Path.DirectorySeparatorChar}OcrTemplate-{variant}.txt");
        }
        catch
        {
            //
        }
        
        templateData ??= File.ReadAllLines($"AoC.Solutions/Common{Path.DirectorySeparatorChar}Ocr{Path.DirectorySeparatorChar}OcrTemplate-{variant}.txt");

        if (variant == Variant.Small)
        {
            _width = 5;

            _height = 6;
        }
        else
        {
            _width = 8;

            _height = 10;
        }

        var line = 0;

        while (line < templateData.Length)
        {
            var character = templateData[line][0];

            line += 2;

            var template = templateData.Skip(line).Take(_height).ToArray();

            var characterTemplate = new CharacterTemplate(character, template, _width, _height);

            _templates.Add(characterTemplate);

            line += _height + 1;
        }
    }

    public string Parse(string input)
    {
        var matrix = ConvertToMatrix(input);

        var result = OcrMatrix(matrix);

        return result;
    }

    private bool[,] ConvertToMatrix(string input)
    {
        var length = input.IndexOf('\0');

        if (length % _width != 0)
        {
            length += _width - length % _width;
        }

        var matrix = new bool[length, _height];

        var x = 0;

        var y = 0;

        for (var i = 0; i < input.Length; i++)
        {
            if (input[i] == '\0')
            {
                x = 0;

                y++;

                continue;
            }

            matrix[x, y] = input[i] == '*';

            x++;
        }

        return matrix;
    }

    private string OcrMatrix(bool[,] matrix)
    {
        var result = new StringBuilder();

        for (var x = 0; x < matrix.GetLength(0); x += _width)
        {
            result.Append(MatchCharacter(matrix, x));
        }

        return result.ToString();
    }

    private char MatchCharacter(bool[,] matrix, int position)
    {
        foreach (var template in _templates)
        {
            var matched = true;

            for (var y = 0; y < _height; y++)
            {
                for (var x = 0; x < _width; x++)
                {
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