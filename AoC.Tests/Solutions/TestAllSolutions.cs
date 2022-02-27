using AoC.Tests.Exceptions;
using Xunit;

namespace AoC.Tests.Solutions;

public class TestAllSolutions
{
    private readonly string[] _answers;

    public TestAllSolutions()
    {
        _answers = File.ReadAllLines($"Solutions{Path.DirectorySeparatorChar}AllAnswers.txt");
    }

    [Theory]
    [ClassData(typeof(PuzzleEnumerator))]
    public void RunTests(Type solution, string answer)
    {
        var key = $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}";

        var correctAnswerLine = _answers.FirstOrDefault(a => a.StartsWith(key));

        if (correctAnswerLine == null)
        {
            throw new TestException($"Please add the correct answer for {key} to AllAnswers.txt.");
        }

        var split = correctAnswerLine.Split(": ");

        if (split[1] != answer)
        {
            throw new TestException($"Incorrect answer for {key}. Expected {split[1]}, actual {answer}.");
        }
    }
}