using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._14;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        MoveRocks(r => r.Y > 0, (rock, other) => other.X == rock.X && other.Y < rock.Y, p => p.Y = 0, (rock, other) => rock.Y = other.Y + 1);

        var result = GetLoad();

        return result.ToString();
    }
}