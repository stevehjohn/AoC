using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019._01;
using Xunit;

namespace AoC.Tests.Solutions;

public class TestAllSolutions
{
    private readonly string[] _answers;

    public TestAllSolutions()
    {
        _answers = File.ReadAllLines($"Solutions{Path.DirectorySeparatorChar}AllAnswers.txt");

    }

    [Fact]
    public void RunTests()
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

            CheckAnswer(solution, answer);
        }
    }

    void CheckAnswer(Type solution, string answer)
    {
        var key = $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}";

        var correctAnswerLine = _answers.FirstOrDefault(a => a.StartsWith(key));

        if (correctAnswerLine == null)
        {
            Console.WriteLine($"Please add correct answer for {key} to AllAnswers.txt.");

            return;
        }

        var split = correctAnswerLine.Split(": ");

        if (split[1] != answer)
        {
            throw new IncorrectAnswerException($"Incorrect answer for {key}. Expected {split[1]}, actual {answer}.");
        }
    }
}