using AoC.Solutions.Infrastructure;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AoC.Benchmark.Infrastructure;

public class DescriptionColumn : IColumn
{
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
    {
        if (benchmarkCase.Parameters.Count == 0 || benchmarkCase.Parameters[0].Value == null)
        {
            return "Unknown";
        }

        var instance = Activator.CreateInstance(benchmarkCase.Parameters[0].Value.GetType()) as Solution;

        if (instance == null)
        {
            return "Unknown";
        }

        return instance.Description;
    }

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);

    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;

    public bool IsAvailable(Summary summary) => true;

    public string Id => "Description";

    public string ColumnName => "Description";

    public bool AlwaysShow => true;

    public ColumnCategory Category => ColumnCategory.Job;

    public int PriorityInCategory => 1;

    public bool IsNumeric => false;

    public UnitType UnitType => UnitType.Dimensionless;

    public string Legend => "The AoC puzzle title";
}