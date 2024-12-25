using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._25;

[UsedImplicitly]
public class Part1 : Base
{
    private ulong[] _locks;

    private ulong[] _keys;
    
    public override string GetAnswer()
    {
        ParseInput();

        var result = 0;

        for (var l = 0; l < _locks.Length; l++)
        {
            for (var k = 0; k < _keys.Length; k++)
            {
                if ((_locks[l] & _keys[k]) == 0)
                {
                    result++;
                }
            }
        }

        return result.ToString();
    }

    private void ParseInput()
    {
        var item = 0;

        var locks = new List<ulong>();

        var keys = new List<ulong>(); 

        while (item < Input.Length)
        {
            var isLock = Input[item] == "#####";

            var device = 0UL;

            for (var i = 1; i < 6; i++)
            {
                var line = Input[item + i];

                var segment = 0UL;

                for (var x = 0; x < 5; x++)
                {
                    if (line[x] == '#')
                    {
                        segment |= 1UL << x;
                    }
                }

                device |= segment << ((i - 1) * 5);
            }

            if (isLock)
            {
                locks.Add(device);
            }
            else
            {
                keys.Add(device);
            }

            item += 8;
        }

        _locks = locks.ToArray();

        _keys = keys.ToArray();
    }
}