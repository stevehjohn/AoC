namespace AoC.Solutions.Common.Ocr;

public class CharacterTemplate
{
    public char Character { get; }

    private readonly bool[,] _template = new bool[5, 6];

    public CharacterTemplate(char character, string[] template)
    {
        Character = character;

        for (var y = 0; y < template.Length; y++)
        {
            for (var x = 0; x < template[y].Length; x++)
            {
                _template[x, y] = template[y][x] == '*';
            }
        }
    }
}