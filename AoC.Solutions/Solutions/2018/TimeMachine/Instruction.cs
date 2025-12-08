namespace AoC.Solutions.Solutions._2018.TimeMachine;

public readonly struct Instruction
{
    public readonly OpCode OpCode;
    
    public readonly int A;
    
    public readonly int B;
    
    public readonly int C;

    public Instruction(OpCode opCode, int a, int b, int c)
    {
        OpCode = opCode;
        
        A = a;
        
        B = b;
        
        C = c;
    }
}