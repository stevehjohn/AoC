namespace AoC.Solutions.Solutions._2019.Computer;

public class Instruction
{
    public int Length { get; set; }

    public Func<OperationState> Execute { get; set; }
}