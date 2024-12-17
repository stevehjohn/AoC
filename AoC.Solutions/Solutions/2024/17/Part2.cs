using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._17;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var queue = new Queue<(long A, int Offest)>();
        
        queue.Enqueue((0, Program.Length - 1));

        while (queue.Count > 0)
        {
            var input = queue.Dequeue();

            for (var i = 0; i < 8; i++)
            {
                var a = (input.A << 3) + i;
                
                var result = RunProgram(a);

                if (result == null)
                {
                    continue;
                }

                if (result.SequenceEqual(Program[input.Offest..]))
                {
                    if (input.Offest == 0)
                    {
                        return a.ToString();
                    }
                    
                    queue.Enqueue((a, input.Offest - 1));
                }
            }
        }

        return "Failed";
    }
}