// using AoC.Solutions.Common;
// using JetBrains.Annotations;
//
// namespace AoC.Solutions.Solutions._2023._22;
//
// [UsedImplicitly]
// public class Part2 : Base
// {
//     public override string GetAnswer()
//     {
//         ParseInput();
//         
//         SettleBricks();
//         
//         var result = GetSupportingBricks();
//
//         var count = 0;
//
//         Parallel.ForEach(result,
//             () => 0,
//             (brickId, _, c) =>
//             {
//                 var settled = new List<(int Id, List<Point> Points)>();
//
//                 foreach (var brick in Bricks)
//                 {
//                     if (brick.Id != brickId)
//                     {
//                         settled.Add((brick.Id, brick.Points.Select(b => new Point(b)).ToList()));
//                     }
//                 }
//
//                 return c + SettleBricks(settled);
//             }, c => Interlocked.Add(ref count, c));
//         
//         return count.ToString();
//     }
//     
//     private List<int> GetSupportingBricks()
//     {
//         var result = new List<int>();
//
//         var settledState = Bricks.ToList();
//
//         foreach (var brick in settledState)
//         {
//             Bricks.Remove(brick);
//
//             if (SettleBricks(Bricks, false) > 0)
//             {
//                 result.Add(brick.Id);                    
//             }
//
//             Bricks.Add(brick);
//         }
//         
//         return result;
//     }
// }