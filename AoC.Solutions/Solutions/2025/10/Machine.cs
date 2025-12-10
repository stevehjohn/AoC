namespace AoC.Solutions.Solutions._2025._10;

public class Machine
{
    private int _display;

    private int[] _buttons;
    
    public Machine(string configuration)
    {
        var parts = configuration.Split(' ');

        for (var i = 1; i < parts[0].Length; i++)
        {
            if (parts[0][i] == '#')
            {
                _display |= 1 << (i - 1);
            }
        }
        
        Console.WriteLine(Convert.ToString(_display, 2));

        _buttons = new int[parts.Length - 2];

        for (var i = 1; i < parts.Length - 1; i++)
        {
            var digits = parts[i][1..^1].Split(',');

            var button = 0;

            for (var d = 0; d < digits.Length; d++)
            {
                button |= 1 << (digits[d][0] - '0');
            }

            _buttons[i - 1] = button;
        
            Console.Write($"{Convert.ToString(button, 2)} ");
        }
        
        Console.WriteLine();
    }

    public int SwitchOn()
    {
        return 0;
    }
}