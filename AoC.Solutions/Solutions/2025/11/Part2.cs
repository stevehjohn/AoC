// using JetBrains.Annotations;
//
// namespace AoC.Solutions.Solutions._2025._11;
//
// [UsedImplicitly]
// public class Part2 : Base
// {
//     public override string GetAnswer()
//     {
//         ParseInput();
//
//         var result = CountPaths();
//
//         return result.ToString();
//     }
//
//     private int CountPaths()
//     {
//         var queue = new Queue<(Node Node, HashSet<string> Visited)>();
//
//         queue.Enqueue((Nodes["svr"], []));
//
//         var paths = 0;
//
//         while (queue.TryDequeue(out var item))
//         {
//             if (! item.Visited.Add(item.Node.Name))
//             {
//                 continue;
//             }
//
//             foreach (var connection in item.Node.Connections)
//             {
//                 if (connection.Name == "out")
//                 {
//                     if (item.Visited.Contains("dac") && item.Visited.Contains("fft"))
//                     {
//                         paths++;
//                     }
//
//                     continue;
//                 }
//
//                 queue.Enqueue((Nodes[connection.Name], [..item.Visited]));
//             }
//         }
//
//         return paths;
//     }
// }