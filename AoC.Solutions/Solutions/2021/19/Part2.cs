using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._19;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        for (var s1 = 0; s1 < Scanners.Count; s1++)
        {
            var originScanner = Scanners[s1];

            for (var s2 = 0; s2 < Scanners.Count; s2++)
            {
                if (s1 == s2 || Scanners[s2].Position != null)
                {
                    continue;
                }

                Scanners[s2].LocateRelativeTo(originScanner);

                //if (Scanners[s2].Position != null)
                //{
                //    Console.WriteLine($"{s1} => {s2}");
                //}
            }
        }

        for (var s = 0; s < Scanners.Count; s++)
        {
            Console.WriteLine(Scanners[s].Position);
        }

        return "TESTING";
    }
}