using System.Text;

namespace AoC.Solutions.Solutions._2024._21;

public class DPad
{
    private readonly Dictionary<string, string> _map = new()
    {
        { "A^", "<A" },
        { "A<", "<v<A" },
        { "Av", "<vA" },
        { "A>", "vA" },
        { "^A", ">A" },
        { "^<", "v<A" },
        { "^v", "vA" },
        { "^>", "v>A" },
        { "<^", ">^A" },
        { "<A", ">^>A" },
        { "<v", ">A" },
        { "<>", ">>A" },
        { "v^", "^A" },
        { "vA", "^>A" },
        { "v<", "<A" },
        { "v>", ">A" },
        { ">^", "<^A" },
        { ">A", "^A" },
        { "><", "<<A" },
        { ">v", "<A" }
    };

    private char _position = 'A';

    public string GetSequence(string code)
    {
        var sequence = new StringBuilder();
        
        for (var i = 0; i < code.Length; i++)
        {
            if (_position == code[i])
            {
                sequence.Append('A');
                
                continue;
            }

            sequence.Append(_map[$"{_position}{code[i]}"]);

            _position = code[i];
        }

        return sequence.ToString();
    }
}