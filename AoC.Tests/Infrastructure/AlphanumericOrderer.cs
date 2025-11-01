using JetBrains.Annotations;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace AoC.Tests.Infrastructure;

[UsedImplicitly]
public class AlphanumericOrderer : ITestCaseOrderer
{
    public IEnumerable<TTestCase> OrderTestCases<TTestCase>(IEnumerable<TTestCase> testCases) where TTestCase : ITestCase
    {
        return testCases.OrderBy(tc => tc.TestMethod.Method.Name).ThenBy(tc => tc.DisplayName);
    }
}