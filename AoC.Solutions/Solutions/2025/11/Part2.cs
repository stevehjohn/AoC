using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2025._11;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var result = CountPaths();

        return result.ToString();
    }

    private long CountPaths()
    {
        var nodeCount = NodeCount;

        var startId = NodeIds["svr"];

        var outId   = NodeIds["out"];
        
        var dacId   = NodeIds["dac"];
        
        var fftId   = NodeIds["fft"];

        var inedges = new int[nodeCount];

        for (var i = 0; i < nodeCount; i++)
        {
            var node = Nodes[i];

            foreach (var conn in node.Connections)
            {
                inedges[conn.Id]++;
            }
        }

        var queue = new Queue<int>();
        
        var ordered  = new List<int>(nodeCount);

        for (var i = 0; i < nodeCount; i++)
        {
            if (inedges[i] == 0)
            {
                queue.Enqueue(i);
            }
        }

        while (queue.Count > 0)
        {
            var u = queue.Dequeue();

            ordered.Add(u);

            foreach (var conn in Nodes[u].Connections)
            {
                if (--inedges[conn.Id] == 0)
                {
                    queue.Enqueue(conn.Id);
                }
            }
        }

        var cache = new long[nodeCount, 4];

        cache[startId, 0] = 1;

        foreach (var u in ordered)
        {
            for (var state = 0; state < 4; state++)
            {
                var ways = cache[u, state];

                if (ways == 0)
                {
                    continue;
                }

                foreach (var connection in Nodes[u].Connections)
                {
                    var id = connection.Id;

                    var newState = state;

                    if (id == dacId)
                    {
                        newState |= 1;
                    }

                    if (id == fftId)
                    {
                        newState |= 2;
                    }

                    cache[id, newState] += ways;
                }
            }
        }

        return cache[outId, 3];
    }
}
