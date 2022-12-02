using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AoC.Benchmark;

public class NameColumn : IColumn
{
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase)
    {
        var fullName = benchmarkCase.Parameters[0].Value.GetType().FullName;

        if (string.IsNullOrWhiteSpace(fullName))
        {
            return "Unknown";
        }

        var parts = fullName.Replace("_", string.Empty).Replace("Part", string.Empty).Split(".");

        return $"{parts[3]}.{parts[4]}.{parts[5]}";
    }

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style) => GetValue(summary, benchmarkCase);

    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;

    public bool IsAvailable(Summary summary) => true;

    public string Id => "Puzzle";

    public string ColumnName => "Puzzle";

    public bool AlwaysShow => true;

    public ColumnCategory Category => ColumnCategory.Job;

    public int PriorityInCategory => 0;

    public bool IsNumeric => false;

    public UnitType UnitType => UnitType.Dimensionless;

    public string Legend => "The AoC puzzle in format YYYY.DD.X where X is part 1 or 2";
}