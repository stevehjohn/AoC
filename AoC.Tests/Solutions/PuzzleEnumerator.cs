using System.Collections;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019._01;

namespace AoC.Tests.Solutions;

public class PuzzleEnumerator : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        var solutions = typeof(Part1).Assembly
                                     .GetTypes()
                                     .Where(t => t.IsSubclassOf(typeof(Solution)) && ! t.IsAbstract)
                                     .OrderBy(t => t.Namespace)
                                     .ThenBy(t => t.Name);

        foreach (var solution in solutions)
        {
            var instance = Activator.CreateInstance(solution) as Solution;

            var answer = instance?.GetAnswer();

            yield return new object[] { solution, answer };
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}