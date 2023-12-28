using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._22;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        SettleBricks();

        BuildStructure();

        var result = CountNonSupportingBricks();
        
        return result.ToString();
    }
    
    private int CountNonSupportingBricks()
    {
        var result = 0;

        for (var id = 1; id <= Count; id++)
        {
            var supporting = Supported.Where(b => b.SupportedById == id).ToList();
            
            if (supporting.Count == 0)
            { 
                result++;
                
                continue;
            }

            var supportedByOther = false; 
            
            foreach (var brick in supporting)
            {
                supportedByOther = Supported.Count(b => b.Id == brick.Id) > 1;
            
                if (supportedByOther)
                {
                    break;
                }
            }
            
            if (supportedByOther)
            {
                result++;
            }
        }

        return result;
    }
}