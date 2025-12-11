// using JetBrains.Annotations;
//
// namespace AoC.Solutions.Solutions._2025._11;
//
// [UsedImplicitly]
// public class Part2 : Base
// {
//     private readonly HashSet<string> _visited = [];
//
//     private long _paths;
//     
//     public override string GetAnswer()
//     {
//         ParseInput();
//
//         _visited.Add("svr");
//         
//         CountPaths(Nodes["svr"]);
//         
//         return _paths.ToString();
//     }
//
//     private void CountPaths(Node from)
//     {
//         if (from.Name == "out")
//         {
//             if (_visited.Contains("dac") && _visited.Contains("fft"))
//             {
//                 _paths++;
//
//                 Console.WriteLine(_paths);
//             }
//
//             return;
//         }
//         
//         foreach (var connection in from.Connections)
//         {
//             if (! _visited.Add(connection.Name))
//             {
//                 continue;
//             }
//
//             CountPaths(connection);
//
//             _visited.Remove(connection.Name);
//         }        
//     }
// }