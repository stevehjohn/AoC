using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;
using JetBrains.Annotations;
using Perfolizer.Mathematics.Common;
using System.Collections.Immutable;

namespace AoC.Benchmark;

public class CustomColumnProviders
{
    [PublicAPI] public static readonly IColumnProvider Name = new NameColumnProvider();

    [PublicAPI] public static readonly IColumnProvider Statistics = new StatisticsColumnProvider();

    [PublicAPI] public static readonly IColumnProvider Metrics = new MetricsColumnProvider();

    public static readonly IColumnProvider[] Instance =
    {
        Name,
        Statistics,
        Metrics
    };

    private class NameColumnProvider : IColumnProvider
    {
        public IEnumerable<IColumn> GetColumns(Summary summary)
        {
            yield return new NameColumn();
        }
    }

    private class StatisticsColumnProvider : IColumnProvider
    {
        public IEnumerable<IColumn> GetColumns(Summary summary)
        {
            yield return StatisticColumn.Mean;
            yield return StatisticColumn.Error;
            if (NeedToShow(summary, s => s.Percentiles.P95 > s.Mean + 3.0 * s.StandardDeviation))
                yield return StatisticColumn.P95;
            if (NeedToShow(summary, s =>
                {
                    if (s.N < 3)
                        return false;
                    return ! s.GetConfidenceInterval(ConfidenceLevel.L99, s.N).Contains(s.Median) ||
                           Math.Abs(s.Median - s.Mean) > s.Mean * 0.2;
                }))
                yield return StatisticColumn.Median;
            if (NeedToShow(summary, s => s.StandardDeviation > 1E-09))
                yield return StatisticColumn.StdDev;
            if (summary.Reports != new ImmutableArray<BenchmarkReport>?() && summary.HasBaselines())
            {
                yield return BaselineRatioColumn.RatioMean;
                IColumn stdDevColumn = BaselineRatioColumn.RatioStdDev;
                if (! summary.BenchmarksCases.Select((Func<BenchmarkCase, string>) (b => stdDevColumn.GetValue(summary, b))).All((Func<string, bool>) (value => value == "0.00" || value == "0.01")))
                    yield return BaselineRatioColumn.RatioStdDev;
                if (HasMemoryDiagnoser(summary))
                    yield return BaselineAllocationRatioColumn.RatioMean;
            }
        }

        private static bool NeedToShow(Summary summary, Func<BenchmarkDotNet.Mathematics.Statistics, bool> check) => summary.Reports != new ImmutableArray<BenchmarkReport>?() &&
                                                                                                                     summary.Reports.Any((Func<BenchmarkReport, bool>) (r => r.ResultStatistics != null && check(r.ResultStatistics)));

        private static bool HasMemoryDiagnoser(Summary summary) => summary.BenchmarksCases.Any((Func<BenchmarkCase, bool>) (c => c.Config.HasMemoryDiagnoser()));
    }

    private class MetricsColumnProvider : IColumnProvider
    {
        public IEnumerable<IColumn> GetColumns(Summary summary) => summary.Reports
                                                                          .SelectMany(
                                                                              (Func<BenchmarkReport, IEnumerable<IMetricDescriptor>>) (report =>
                                                                                                                                              report.Metrics.Values.Select(
                                                                                                                                                  (Func<Metric, IMetricDescriptor>) (metric => metric.Descriptor))))
                                                                          .Distinct(MetricDescriptorEqualityComparer.Instance)
                                                                          .Select((Func<IMetricDescriptor, MetricColumn>) (descriptor => new MetricColumn(descriptor)));
    }
}