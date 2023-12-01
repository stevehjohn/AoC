using AoC.Solutions.Common.Ocr;
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
            var year = int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0");

            var day = int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0");

            if (new DateTime(year, 12, day) > DateTime.UtcNow)
            {
                continue;
            }

            if (arguments.Length == 1)
            {
                var solutionKey = $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0"):D2}.{solution.Name[4]}";

                if (solutionKey.Substring(0, arguments[0].Length) != arguments[0])
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

            if (instance.OcrOutput.HasValue)
            {
                var matrixParser = new MatrixParser(instance.OcrOutput.Value);

                answer = matrixParser.Parse(answer);
            }

            if (previousYear == null)
            {
                previousYear = year;
            }
            else if (year != previousYear)
            {
                WriteYearSummary(arguments.Length == 0);

                previousYear = year;

                yearMs = 0;
            }
            
            CheckAnswer(instance.GetType(), answer);

            var displayAnswer = answer.Length < 26
                                    ? answer
                                    : $"{answer[..26]}...";

            var description = string.Empty;

            if (instance.Description != previousDesc)
            {
                description = instance.Description;

                previousDesc = description;
            }

            var microseconds = stopwatch.Elapsed.TotalMicroseconds;
            
            Console.WriteLine($" {year} {int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0"),2}.{solution.Name[4]}: {displayAnswer,-30} {$"{microseconds:N0}μs",-12}  {description}");

            totalMs += (long) microseconds;

            yearMs += (long) microseconds;
        }

        WriteYearSummary(arguments.Length == 0);

        void CheckAnswer(Type solution, string answer)
        {
            if (answer == "TESTING")
            {
                var temp = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine(" This seems to be an in progress puzzle.");

                Console.ForegroundColor = temp;

                return;
            }

            var key = $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}";

            var correctAnswerLine = answers.FirstOrDefault(a => a.StartsWith(key));

            if (correctAnswerLine == null)
            {
                var temp = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine($" Please add the correct answer for {key} to AllAnswers.txt.");

                Console.ForegroundColor = temp;

                return;
            }

            var split = correctAnswerLine.Split(": ");

            if (split[1] != answer && answer != "TESTING")
            {
                var temp = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkRed;

                Console.WriteLine($" Incorrect answer for {key}. Expected {split[1]}, actual {answer}.");

                Console.ForegroundColor = temp;
            }
        }

        void WriteYearSummary(bool showTotal)
        {
            Console.Write($"{new string(' ', 43)}-------------");

            Console.WriteLine(showTotal ? " -------------" : string.Empty);

            Console.Write($"{new string(' ', 43)}{$"{yearMs / 1000:N0}ms",-13}");

            Console.WriteLine(showTotal ? $" {totalMs / 1000:N0}ms" : string.Empty);

            Console.WriteLine();
        }
    }
}