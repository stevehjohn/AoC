using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2017._16;

public abstract class Base : Solution
{
    public override string Description => "Permutation promenade";

    protected static void RunDance(ref char[] dancers, string moves)
    {
        var steps = moves.Split(',');

        foreach (var step in steps)
        {
            switch (step[0])
            {
                case 's':
                    var len = int.Parse(step[1..]);

                    var temp = new char[16];

                    Buffer.BlockCopy(dancers, 0, temp, 0, sizeof(char) * 16);

                    for (var i = 0; i < len; i++)
                    {
                        temp[i] = dancers[16 - len + i];
                    }

                    for (var i = len; i < 16; i++)
                    {
                        temp[i] = dancers[i - len];
                    }

                    dancers = temp;

                    break;
                case 'x':
                    var indexes = step[1..].Split('/').Select(int.Parse).ToArray();

                    (dancers[indexes[0]], dancers[indexes[1]]) = (dancers[indexes[1]], dancers[indexes[0]]);

                    break;
                case 'p':
                    var programs = step[1..].Split('/').Select(p => p[0]).ToArray();

                    var index1 = Array.IndexOf(dancers, programs[0]);

                    var index2 = Array.IndexOf(dancers, programs[1]);

                    (dancers[index1], dancers[index2]) = (dancers[index2], dancers[index1]);

                    break;
            }
        }
    }
}