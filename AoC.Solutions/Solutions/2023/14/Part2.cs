using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._14;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        PerformCycle();

        var result = GetLoad();

        return result.ToString();
    }

    private void PerformCycle()
    {
        MoveRocks(l => l.OrderBy(r => r.Point.Y).ToList(), r => r.Y > 0, (rock, other) => other.X == rock.X && other.Y < rock.Y, p => p.Y = 0, (rock, other) => rock.Y = other.Y + 1);

        MoveRocks(l => l.OrderBy(r => r.Point.X).ToList(), r => r.X > 0, (rock, other) => other.Y == rock.Y && other.X < rock.X, p => p.X = 0, (rock, other) => rock.X = other.X + 1);

        MoveRocks(l => l.OrderByDescending(r => r.Point.Y).ToList(), r => r.Y < Rows - 1, (rock, other) => other.X == rock.X && other.Y > rock.Y, p => p.Y = Rows - 1, (rock, other) => rock.Y = other.Y - 1);
        
        MoveRocks(l => l.OrderByDescending(r => r.Point.X).ToList(),r => r.X < Columns - 1, (rock, other) => other.Y == rock.Y && other.X > rock.X, p => p.X = Columns - 1, (rock, other) => rock.X = other.X - 1);
    }
}