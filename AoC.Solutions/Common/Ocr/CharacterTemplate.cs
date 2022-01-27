namespace AoC.Solutions.Common.Ocr;

public class CharacterTemplate
{
    public char Character { get; }

    public readonly bool[,] Template;

    public CharacterTemplate(char character, string[] template, int width, int height)
    {
        Template = new bool[width, height];

        Character = character;

        for (var y = 0; y < template.Length; y++)
        {
            for (var x = 0; x < template[y].Length; x++)
            {
                Template[x, y] = template[y][x] == '*';
            }
        }
    }
}