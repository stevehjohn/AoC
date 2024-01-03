using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._19;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var sum = 0;
        
        foreach (var part in Parts)
        {
            sum += ProcessPart(part);
        }
        
        return sum.ToString();
    }

    private int ProcessPart(Part part)
    {
        var name = "in";

        while (name is not ("A" or "R"))
        {
            var workflow = Workflows[name];

            foreach (var rule in workflow)
            {
                if (rule.Condition == '\0')
                {
                    name = rule.Destination;
                    
                    break;
                }

                var property = rule.Property switch
                {
                    'x' => part.X,
                    'm' => part.M,
                    'a' => part.A,
                    _ => part.S
                };

                bool pass;
                
                if (rule.Condition == '<')
                {
                    pass = property < rule.Value;
                }
                else
                {
                    pass = property > rule.Value;
                }

                if (pass)
                {
                    name = rule.Destination;
                    
                    break;
                }
            }
        }

        return name == "A" ? part.X + part.M + part.A + part.S : 0;
    }
}