using System.Diagnostics;
using System.Reflection;
using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;

var solutions = Assembly.GetExecutingAssembly()
                        .GetTypes()
                        .Where(t => t.IsSubclassOf(typeof(Solution)) && ! t.IsAbstract)
                        .OrderBy(t => t.Namespace)
                        .ThenBy(t => t.Name);

var totalMs = 0L;

var yearMs = 0L;

int? previousYear = null;

Console.WriteLine();

var answers = File.ReadAllLines($"Solutions{Path.DirectorySeparatorChar}AllAnswers.txt");

foreach (var solution in solutions)
{
    var instance = Activator.CreateInstance(solution) as Solution;

    if (instance == null)
    {
        continue;
    }

    var stopwatch = new Stopwatch();

    stopwatch.Start();

    var answer = instance.GetAnswer();

    stopwatch.Stop();

    CheckAnswer(instance.GetType(), answer);

    var year = int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0");

    if (previousYear == null)
    {
        previousYear = year;
    }
    else if (year != previousYear)
    {
        WriteYearSummary();

        previousYear = year;

        yearMs = 0;
    }

    var displayAnswer = answer.Length < 26
        ? answer
        : $"{answer[..27]}...";

    Console.WriteLine($" {year} {int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0"),2}.{solution.Name[4]}: {displayAnswer,-30} {stopwatch.ElapsedMilliseconds}ms");

    totalMs += stopwatch.ElapsedMilliseconds;

    yearMs += stopwatch.ElapsedMilliseconds;
}

WriteYearSummary();

void CheckAnswer(Type solution, string answer)
{
    var key = $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}";

    var correctAnswerLine = answers.FirstOrDefault(a => a.StartsWith(key));

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

void WriteYearSummary()
{
    Console.WriteLine($"{new string(' ', 43)}------- -------");

    Console.WriteLine($"{new string(' ', 43)}{$"{yearMs}ms".PadRight(7)} {totalMs}ms");

    Console.WriteLine();
}
