using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2018._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        (int Units, Type Side) Test(int boost)
        {
            ParseInput();
            return Play(boost);
        }

        var low = 0;
        
        var high = 1;

        while (Test(high).Side != Type.ImmuneSystem)
        {
            low = high;
            
            high *= 2;
        }

        while (high - low > 1)
        {
            var mid = low + (high - low) / 2;

            if (Test(mid).Side == Type.ImmuneSystem)
            {
                high = mid;
            }
            else
            {
                low = mid;
            }
        }

        return Test(high).Units.ToString();
    }
}