using AoC.Solutions.Exceptions;
using AoC.Solutions.Infrastructure;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace AoC.Solutions;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] arguments)
    {
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

        var previousDesc = string.Empty;

        foreach (var solution in solutions)
        {
            if (arguments.Length == 1)
            {
                if ($"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}" != arguments[0])
                {
                    continue;
                }
            }

            var instance = Activator.CreateInstance(solution) as Solution;

            if (instance == null)
            {
                continue;
            }

            GC.Collect();

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

            var description = string.Empty;

            if (instance.Description != previousDesc)
            {
                description = instance.Description;

                previousDesc = description;
            }

            Console.WriteLine(
                $" {year} {int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0"),2}.{solution.Name[4]}: {displayAnswer,-30} {$"{stopwatch.ElapsedMilliseconds}ms".PadRight(6)}  {description}");

            totalMs += stopwatch.ElapsedMilliseconds;

            yearMs += stopwatch.ElapsedMilliseconds;
        }

        WriteYearSummary();

        void CheckAnswer(Type solution, string answer)
        {
            var key =
                $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}";

            var correctAnswerLine = answers.FirstOrDefault(a => a.StartsWith(key));

            if (correctAnswerLine == null)
            {
                Console.WriteLine($"Please add the correct answer for {key} to AllAnswers.txt.");

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
    }
}