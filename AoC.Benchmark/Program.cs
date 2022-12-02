using BenchmarkDotNet.Running;

namespace AoC.Benchmark;

public class Program
{
    public static void Main(string[] arguments)
    {
        Benchmarks.SetArguments(arguments);

        var config = CustomConfig.Instance;
        
        BenchmarkRunner.Run<Benchmarks>(config);
    }
}
