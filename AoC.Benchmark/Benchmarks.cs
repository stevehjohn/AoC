using AoC.Solutions.Infrastructure;
using BenchmarkDotNet.Attributes;
using System.Reflection;

namespace AoC.Benchmark;

[MemoryDiagnoser]
//[SimpleJob(RunStrategy.Throughput, invocationCount: 1, warmupCount: 1, launchCount: 1, targetCount: 1)]
public class Benchmarks
{
    private static string[] _arguments;

    public static void SetArguments(string[] arguments)
    {
        _arguments = arguments;
    }

    [Benchmark]
    [ArgumentsSource(nameof(Puzzles))]
    public string GetAnswer(Type solution)
    {
        var instance = Activator.CreateInstance(solution) as Solution;

        if (instance == null)
        {
            throw new NullReferenceException($"Could not instantiate type {solution.FullName}");
        }

        return instance.GetAnswer();
    }

    public IEnumerable<Type> Puzzles()
    {
        // ReSharper disable once PossibleNullReferenceException
        var solutions = Assembly.GetAssembly(typeof(Solution))
                                .GetTypes()
                                .Where(t => t.IsSubclassOf(typeof(Solution)) && ! t.IsAbstract)
                                .OrderBy(t => t.Namespace)
                                .ThenBy(t => t.Name);

        foreach (var solution in solutions)
        {
            if (_arguments != null && _arguments.Length == 1)
            {
                var solutionKey = $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0"):D2}.{solution.Name[4]}";

                if (solutionKey.Substring(0, _arguments[0].Length) != _arguments[0])
                {
                    continue;
                }
            }

            yield return solution;
        }
    }
}