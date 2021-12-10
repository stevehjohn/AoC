using AoC.Solutions.Extensions;

namespace AoC.Solutions.Solutions._2021._08;

public class Digit
{
    private readonly List<string> _inputs;

    private readonly string[] _digits = new string[10];

    private readonly string[] _output;

    public Digit(string[] inputs, string[] output)
    {
        _inputs = inputs.Select(i => new string(i.OrderBy(c => c).ToArray())).ToList();

        _output = output.Select(i => new string(i.OrderBy(c => c).ToArray())).ToArray();
    }

    public int GetValue()
    {
        DetermineDigits();

        return DecodeOutput();
    }

    private void DetermineDigits()
    {
        _digits[1] = _inputs.Single(i => i.Length == 2);

        _inputs.Remove(_digits[1]);

        _digits[4] = _inputs.Single(i => i.Length == 4);

        _inputs.Remove(_digits[4]);

        _digits[7] = _inputs.Single(i => i.Length == 3);

        _inputs.Remove(_digits[7]);

        _digits[8] = _inputs.Single(i => i.Length == 7);

        _inputs.Remove(_digits[8]);

        _digits[9] = _inputs.Single(i => i.CommonCharacterCount(_digits[4]) == 4);

        _inputs.Remove(_digits[9]);

        _digits[2] = _inputs.Single(i => i.CommonCharacterCount(_digits[9]) == 4);

        _inputs.Remove(_digits[2]);

        _digits[5] = _inputs.Single(i => i.CommonCharacterCount(_digits[2]) == 3);

        _inputs.Remove(_digits[5]);

        _digits[6] = _inputs.Single(i => i.CommonCharacterCount(_digits[5]) == 5);

        _inputs.Remove(_digits[6]);

        _digits[3] = _inputs.Single(i => i.CommonCharacterCount(_digits[6]) == 4);

        _inputs.Remove(_digits[3]);

        _digits[0] = _inputs.Single();

        _inputs.Remove(_digits[0]);
    }

    private int DecodeOutput()
    {
        var result = 0;

        var multiplier = 1;

        for (var i = _output.Length - 1; i >= 0; i--)
        {
            result += Array.IndexOf(_digits, _output[i]) * multiplier;

            multiplier *= 10;
        }

        return result;
    }
}