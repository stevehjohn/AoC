using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._03;

[UsedImplicitly]
public class Part2 : Base
{
    private readonly PriorityQueue<string, int> _queue = new();
    
    public override string GetAnswer()
    {
        var answer = 0L;

        var program = string.Join(string.Empty, Input);

        ParseForInstruction(program, "mul(");
        
        ParseForInstruction(program, "do()");
        
        ParseForInstruction(program, "don't()");

        var enabled = true;
        
        while (_queue.Count > 0)
        {
            var instruction = _queue.Dequeue();

            switch (instruction)
            {
                case "do()":
                    enabled = true;
                
                    continue;
                case "don't()":
                    enabled = false;
                
                    continue;
            }

            if (enabled)
            {
                var result = ParseMulInstruction(instruction);

                if (result == int.MinValue)
                {
                    continue;
                }

                answer += result;
            }
        }

        return answer.ToString();
    }

    private void ParseForInstruction(string program, string instruction)
    {
        var index = 0;
        
        while (index < program.Length)
        {
            var result = FindNextInstruction(program, instruction, index);
        
            if (result.Index == -1)
            {
                break;
            }
        
            index = result.Index + 1;

            _queue.Enqueue(result.Instruction, result.Index);
        }
    }
}