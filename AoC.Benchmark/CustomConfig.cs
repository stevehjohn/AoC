using BenchmarkDotNet.Analysers;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Exporters;
using BenchmarkDotNet.Exporters.Csv;
using BenchmarkDotNet.Filters;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Loggers;
using BenchmarkDotNet.Order;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Validators;
using System.Globalization;

namespace AoC.Benchmark;

public class CustomConfig : IConfig
{
    public static readonly IConfig Instance = new CustomConfig();

    private static readonly Conclusion[] EmptyConclusion = Array.Empty<Conclusion>();

    private CustomConfig()
    {
    }

    public IEnumerable<IColumnProvider> GetColumnProviders() => CustomColumnProviders.Instance;

    public IEnumerable<IExporter> GetExporters()
    {
        yield return CsvExporter.Default;
        yield return MarkdownExporter.GitHub;
        yield return HtmlExporter.Default;
    }

    public IEnumerable<ILogger> GetLoggers()
    {
        if (LinqPadLogger.IsAvailable)
            yield return LinqPadLogger.Instance;
        else
            yield return ConsoleLogger.Default;
    }

    public IEnumerable<IAnalyser> GetAnalysers()
    {
        yield return EnvironmentAnalyser.Default;
        yield return OutliersAnalyser.Default;
        yield return MinIterationTimeAnalyser.Default;
        yield return MultimodalDistributionAnalyzer.Default;
        yield return RuntimeErrorAnalyser.Default;
        yield return ZeroMeasurementAnalyser.Default;
        yield return BaselineCustomAnalyzer.Default;
        yield return HideColumnsAnalyser.Default;
    }

    public IEnumerable<IValidator> GetValidators()
    {
        yield return BaselineValidator.FailOnError;
        yield return SetupCleanupValidator.FailOnError;
        yield return JitOptimizationsValidator.FailOnError;
        yield return RunModeValidator.FailOnError;
        yield return GenericBenchmarksValidator.DontFailOnError;
        yield return DeferredExecutionValidator.FailOnError;
        yield return ParamsAllValuesValidator.FailOnError;
    }

    public IOrderer Orderer => null;

    public ConfigUnionRule UnionRule => ConfigUnionRule.Union;

    public CultureInfo CultureInfo => null;

    public ConfigOptions Options => ConfigOptions.Default;

    public SummaryStyle SummaryStyle => SummaryStyle.Default;

    public TimeSpan BuildTimeout => TimeSpan.FromSeconds(120.0);

    public string ArtifactsPath => Path.Combine(Directory.GetCurrentDirectory(), "BenchmarkDotNet.Artifacts");

    public IReadOnlyList<Conclusion> ConfigAnalysisConclusion => EmptyConclusion;

    public IEnumerable<Job> GetJobs() => Array.Empty<Job>();

    public IEnumerable<BenchmarkLogicalGroupRule> GetLogicalGroupRules() => Array.Empty<BenchmarkLogicalGroupRule>();

    public IEnumerable<IDiagnoser> GetDiagnosers() => Array.Empty<IDiagnoser>();

    public IEnumerable<HardwareCounter> GetHardwareCounters() => Array.Empty<HardwareCounter>();

    public IEnumerable<IFilter> GetFilters() => Array.Empty<IFilter>();

    public IEnumerable<IColumnHidingRule> GetColumnHidingRules() => Array.Empty<IColumnHidingRule>();
}