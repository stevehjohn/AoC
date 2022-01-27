namespace AoC.Solutions.Common.Ocr;

public class CharacterTemplate
{
    public char Character { get; }

    public readonly bool[,] Template = new bool[5, 6]; // 6 x 10

    public CharacterTemplate(char character, string[] template)
    {
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