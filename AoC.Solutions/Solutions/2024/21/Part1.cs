using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2024._21;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var numPad = new NumPad();

        var dPad1 = new DPad();

        var dPad2 = new DPad();

        var result = 0;
        
        foreach (var line in Input)
        {
            var sequence = numPad.GetSequence(line);
            
            Console.WriteLine(sequence);
            
            sequence = dPad1.GetSequence(sequence);
            
            Console.WriteLine(sequence);
            
            sequence = dPad2.GetSequence(sequence);
            
            Console.WriteLine(sequence);

            result += sequence.Length * int.Parse(line[..3]);
        }
        
        return result.ToString();
    }
}