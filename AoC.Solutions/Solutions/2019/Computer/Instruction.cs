namespace AoC.Solutions.Solutions._2019.Computer;

public class Instruction
{
    public int Length { get; init; }

    public Func<OperationState> Execute { get; init; }
}