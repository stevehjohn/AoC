//using AoC.Solutions.Common;
//using JetBrains.Annotations;

//namespace AoC.Solutions.Solutions._2017._22;

//[UsedImplicitly]
//public class Part2 : Base
//{
//    public override string GetAnswer()
//    {
//        ParseInput();

//        var infections = 0;

//        for (var i = 0; i < 10_000_000; i++)
//        {
//            infections += RunCycle() ? 1 : 0;
//        }

//        return infections.ToString();
//    }

//    private bool RunCycle()
//    {
//        var infected = Infected.SingleOrDefault(i => i.Position.Equals(Position));

//        var infects = false;

//        if (infected != null)
//        {
//            Direction = new Point(-Direction.Y, Direction.X);

//            Infected.Remove(infected);
//        }
//        else
//        {
//            Direction = new Point(Direction.Y, -Direction.X);

//            Infected.Add(new Cell(new Point(Position)));

//            infects = true;
//        }

//        Position.X += Direction.X;

//        Position.Y += Direction.Y;

//        return infects;
//    }
//}