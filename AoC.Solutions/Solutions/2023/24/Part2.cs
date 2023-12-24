using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._24;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        Solve();
        
        return "Unknown";
    }

    private void Solve()
    {
        var area = 100;

        for (var x = -area; x < area + 1; x++)
        {
            for (var y = -area; y < area + 1; y++)
            {
                for (var z = -area; z < area + 1; z++)
                {
                    var velocity = new DoublePoint(x, y, z);

                    var h1 = (Hail[0].Position, Hail[0].Velocity + velocity);
                    var h2 = (Hail[1].Position, Hail[1].Velocity + velocity);

                    var c1 = CollidesInFutureXy(h1, h2);
                    var c2 = CollidesInFutureXz(h1, h2);
                    var c3 = CollidesInFutureYz(h1, h2);
                    
                    if (c1 != null && c2 != null && c3 != null)
                    {
                        var all = true;

                        var reference = c1;
                        
                        for (var i = 2; i < Hail.Count; i++)
                        {
                            var hi = (Hail[i].Position, Hail[i].Velocity + velocity);

                            c1 = CollidesInFutureXy(h1, hi);
                            c2 = CollidesInFutureXz(h1, hi);
                            c3 = CollidesInFutureYz(h1, hi);

                            if (c1 == null || c2 == null || c3 == null)
                            {
                                all = false;
                                
                                break;
                            }

                            if (! reference.Equals(c1))
                            {
                                all = false;
                                
                                break;
                            }
                        }

                        if (all)
                        {
                            Console.WriteLine($"{reference} {velocity}: {reference.Value.Time}");
                        }
                    }
                }
            }
        }
    }
}