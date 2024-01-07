using AoC.Solutions.Common.Ocr;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019._01;
using AoC.Tests.Exceptions;
using Xunit;
using Xunit.Abstractions;

namespace AoC.Tests.Solutions;

public class TestAllSolutions
{
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly string[] _answers;

    public TestAllSolutions(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        string path;
        
        if (! Path.Exists("./Solutions"))
        {
            path = "./Aoc.Solutions/Solutions/";
            
            _answers = CryptoFileProvider.LoadFile(path, "AllAnswers.clear");
        }
        else
        {
            path = "./Solutions/";
            
            _answers = CryptoFileProvider.LoadFile(path, "AllAnswers.clear");
        }
    }

    [Theory]
    [MemberData(nameof(Solutions))]
    public void RunTests(Type solution)
    {
        var key = $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}";

        var correctAnswerLine = _answers.FirstOrDefault(a => a.StartsWith(key));

        if (correctAnswerLine == null)
        {
            throw new TestException($"Please add the correct answer for {key} to AllAnswers.clear.");
        }

        var instance = Activator.CreateInstance(solution) as Solution;

        if (instance == null)
        {
            throw new TestException($"Could not instantiate {solution.FullName}.");
        }

        var answer = instance.GetAnswer();

        if (instance.OcrOutput.HasValue)
        {
            var matrixParser = new MatrixParser(instance.OcrOutput.Value);

            answer = matrixParser.Parse(answer);
        }

        var split = correctAnswerLine.Split(": ");

        if (split[1] != answer)
        {
            throw new TestException($"Incorrect answer for {key}. Expected {split[1]}, actual {answer}.");
        }

        _testOutputHelper.WriteLine($"Test of '{instance.Description}' passed.");
    }

    public static IEnumerable<object[]> Solutions
    {
        get
        {
            var solutions = typeof(Part1).Assembly
                                         .GetTypes()
                                         .Where(t => t.IsSubclassOf(typeof(Solution)) && ! t.IsAbstract)
                                         .OrderBy(t => t.Namespace)
                                         .ThenBy(t => t.Name);

            foreach (var solution in solutions)
            {
                yield return [solution];
            }
        }
    }
}