using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2017._08;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var result = RunProgram();

        return result.ToString();
    }

    private int RunProgram()
    {
        var registers = new Dictionary<string, int>();

        foreach (var line in Input)
        {
            var operation = ParseLine(line);

            registers.TryGetValue(operation.ConditionRegister, out var value);

            var conditionMet = false;

            switch (operation.Operator)
            {
                case "<":
                    conditionMet = value < operation.Condition;

                    break;
                case ">":
                    conditionMet = value > operation.Condition;

                    break;
                case "<=":
                    conditionMet = value <= operation.Condition;

                    break;
                case ">=":
                    conditionMet = value >= operation.Condition;

                    break;
                case "==":
                    conditionMet = value == operation.Condition;

                    break;
                case "!=":
                    conditionMet = value != operation.Condition;

                    break;
            }

            if (conditionMet)
            {
                if (registers.ContainsKey(operation.TargetRegister))
                {
                    registers[operation.TargetRegister] += operation.Delta;
                }
                else
                {
                    registers.Add(operation.TargetRegister, operation.Delta);
                }
            }
        }

        return registers.Max(r => r.Value);
    }

    private static (string TargetRegister, int Delta, string ConditionRegister, string Operator, int Condition) ParseLine(string line)
    {
        var parts = line.Split(' ', StringSplitOptions.TrimEntries);

        var delta = int.Parse(parts[2]) * (parts[1] == "inc" ? 1 : -1);

        return (parts[0], delta, parts[4], parts[5], int.Parse(parts[6]));
    }
}