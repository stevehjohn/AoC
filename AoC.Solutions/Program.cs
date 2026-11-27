using AoC.Solutions.Common.Ocr;
using AoC.Solutions.Infrastructure;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using AoC.Solutions.Extensions;

namespace AoC.Solutions;

[ExcludeFromCodeCoverage]
public static class Program
{
    public static void Main(string[] arguments)
    {
        var solutions = Assembly.GetExecutingAssembly()
            .GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Solution)) && ! t.IsAbstract)
            .OrderBy(t => t.Namespace, StringComparer.Ordinal)
            .ThenBy(t => t.Name, StringComparer.Ordinal);

        var totalMs = 0L;

        var yearMs = 0L;

        var count = 0;

        int? previousYear = null;

        var showAnswer = false;

        for (var i = 0; i < arguments.Length; i++)
        {
            if (arguments[i].Equals("sa", StringComparison.InvariantCultureIgnoreCase))
            {
                showAnswer = true;
            }
        }

        Console.WriteLine();

        string[] answers;

        var results = new Dictionary<(int Year, int Day, int Part), (double Microseconds, string Summary)>();

        string path;

        if (! Path.Exists("./Solutions"))
        {
            path = "./Aoc.Solutions/Solutions/";

            answers = CryptoFileProvider.LoadFile(path, "AllAnswers.clear");

            if (answers == null)
            {
                answers = ["2015.1.1: ????", "2015.1.2: ????", "2015.2.1: ????", "etc..."];

                File.WriteAllLines("./Aoc.Solutions/Solutions/AllAnswers.clear", answers);
            }
        }
        else
        {
            path = "./Solutions/";

            answers = CryptoFileProvider.LoadFile(path, "AllAnswers.clear");

            if (answers == null)
            {
                answers = ["2015.1.1: ????", "2015.1.2: ????", "2015.2.1: ????", "etc..."];

                File.WriteAllLines("./Solutions/AllAnswers.clear", answers);
            }
        }

        var previousDesc = string.Empty;

        foreach (var solution in solutions)
        {
            var year = int.FastParse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0");

            var day = int.FastParse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0");

            if (arguments.Length > 0
                && ! arguments[0].Equals("true", StringComparison.InvariantCultureIgnoreCase)
                && ! arguments[0].Equals("sa", StringComparison.InvariantCultureIgnoreCase))
            {
                var solutionKey =
                    $"{int.FastParse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.FastParse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0"):D2}.{solution.Name[4]}";

                if (solutionKey[..arguments[0].Length] != arguments[0])
                {
                    continue;
                }
            }

            if (Activator.CreateInstance(solution) is not Solution instance)
            {
                continue;
            }

            var runTwice = arguments.Length != 1 || arguments[0].Length < 5;

            var stopwatch = new Stopwatch();

            var answer = string.Empty;

            var microseconds = double.MaxValue;

            var valid = true;

            if (runTwice)
            {
                stopwatch.Start();

                answer = instance.GetAnswer();

                stopwatch.Stop();

                microseconds = stopwatch.Elapsed.TotalMicroseconds;

                answer = ParseAnswer(instance, answer);

                valid = CheckAnswer(solution, answer, answers, 1);
            }

            if (! runTwice || microseconds < TimeSpan.FromSeconds(10).TotalMicroseconds)
            {
                if (runTwice)
                {
                    if (Activator.CreateInstance(solution) is not Solution secondInstance)
                    {
                        continue;
                    }

                    instance = secondInstance;
                }

                GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);

                stopwatch.Restart();

                var nextAnswer = instance.GetAnswer();

                stopwatch.Stop();

                var nextTime = stopwatch.Elapsed.TotalMicroseconds;

                nextAnswer = ParseAnswer(instance, nextAnswer);

                valid &= CheckAnswer(solution, nextAnswer, answers, runTwice ? 2 : 1);

                if (nextTime < microseconds)
                {
                    microseconds = nextTime;

                    answer = nextAnswer;
                }
            }

            if (previousYear == null)
            {
                previousYear = year;
            }
            else if (year != previousYear)
            {
                CleanUp();

                WriteYearSummary(yearMs);

                previousYear = year;

                yearMs = 0;

                GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
            }

            var description = string.Empty;

            if (instance.Description != previousDesc)
            {
                description = instance.Description;

                previousDesc = description;
            }

            var part = solution.Name[4];

            var summary = $" {year} {day,2}.{part}: {$"{microseconds:N0}μs",-12}  {description}";

            Console.WriteLine(summary);

            if (showAnswer)
            {
                Console.WriteLine($"            {answer}");
            }

            if (valid)
            {
                results.Add((year, day, part), (microseconds, summary));

                yearMs += (long) microseconds;

                totalMs += (long) microseconds;

                count++;
            }
        }

        CleanUp();

        WriteYearSummary(yearMs);

        Console.WriteLine($" {count} puzzle{(count != 1 ? "s" : string.Empty)} solved in {totalMs / 1_000_000d:N3}s.\n");

        if (arguments.Length > 0)
        {
            if (arguments[0].Equals("true", StringComparison.InvariantCultureIgnoreCase) || (arguments.Length > 1 && arguments[1].Equals("true", StringComparison.InvariantCultureIgnoreCase)))
            {
                UpdateResults(results);
            }
        }
    }

    private static string ParseAnswer(Solution instance, string answer)
    {
        if (instance.OcrOutput.HasValue)
        {
            var matrixParser = new MatrixParser(instance.OcrOutput.Value);

            return matrixParser.Parse(answer);
        }

        return answer;
    }

    private static void UpdateResults(Dictionary<(int Year, int Day, int Part), (double Microseconds, string Summary)> results)
    {
        const string resultsFileName = "./results.md";

        if (! File.Exists(resultsFileName))
        {
            Console.WriteLine(" results.md not found, will not update.\n");

            return;
        }

        var file = File.ReadAllLines(resultsFileName).ToList();

        var updated = 0;

        foreach (var result in results)
        {
            var (year, day, part) = result.Key;

            var index = file.FindIndex(l => l.StartsWith($" {year} {day,2}.{(char) part}:"));

            if (index < 0)
            {
                var insert = file.FindLastIndex(l => l.StartsWith("```"));

                if (day == 1 && part == '1')
                {
                    insert--;

                    file.Insert(insert, string.Empty);
                    file.Insert(insert, $"{new string(' ', 12)}{"0ms",-13}");
                    file.Insert(insert, $"{new string(' ', 12)}-------------");
                }
                else
                {
                    insert -= 4;
                }

                file.Insert(insert, result.Value.Summary);

                updated++;
            }
            else
            {
                var line = file[index];

                var time = int.FastParse(line[12..].Split(' ', StringSplitOptions.TrimEntries)[0][..^2]);

                if (result.Value.Microseconds < time)
                {
                    file[index] = result.Value.Summary;

                    updated++;
                }
            }
        }

        if (updated == 0)
        {
            Console.WriteLine(" No times were quicker, will not update results.md.\n");
        }
        else
        {
            RecalculateTotals(file);

            File.WriteAllLines(resultsFileName, file);

            Console.WriteLine($" {updated} time(s) were updated in results.md.\n");
        }
    }

    private static void RecalculateTotals(List<string> file)
    {
        var overall = 0L;

        var year = 0;

        var sum = 0L;

        var puzzles = 0;

        for (var i = 0; i < file.Count; i++)
        {
            var line = file[i];

            if (line.Contains("solved in"))
            {
                file[i] = $" {puzzles} puzzle{(puzzles != 1 ? "s" : string.Empty)} solved in {overall / 1_000_000d:N3}s.";
            }

            if (line.StartsWith(" 20") && year == 0)
            {
                sum = 0;

                year = int.FastParse(line[1..5]);
            }

            if (line.StartsWith(" 20") && year != 0)
            {
                var time = int.FastParse(line[12..].Split(' ', StringSplitOptions.TrimEntries)[0][..^2]);

                sum += time;

                overall += time;

                puzzles++;
            }

            if (line.StartsWith("     ") && year != 0)
            {
                if (sum < 1_000_000)
                {
                    file[i + 1] = $"{new string(' ', 12)}{$"{sum / 1_000d:N3}ms",-13}";
                }
                else
                {
                    file[i + 1] = $"{new string(' ', 12)}{$"{sum / 1_000_000d:N3}s",-13}";
                }

                year = 0;
            }
        }
    }

    private static bool CheckAnswer(Type solution, string answer, string[] answers, int run)
    {
        if (answer.Equals("Unknown", StringComparison.OrdinalIgnoreCase))
        {
            var temp = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Blue;

            Console.WriteLine(" This seems to be an in progress puzzle.");

            Console.ForegroundColor = temp;

            return false;
        }

        var key =
            $"{int.FastParse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.FastParse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}";

        var correctAnswerLine = answers.FirstOrDefault(a => a.StartsWith(key + ": ", StringComparison.Ordinal));

        if (correctAnswerLine == null)
        {
            var temp = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.DarkYellow;

            Console.WriteLine($" Please add the correct answer for {key} to AllAnswers.clear.");

            Console.ForegroundColor = temp;

            return false;
        }

        var split = correctAnswerLine.Split(": ", 2);

        if (split[1] != answer)
        {
            var temp = Console.ForegroundColor;

            Console.ForegroundColor = ConsoleColor.Magenta;

            Console.Write($" Incorrect answer for {key} (run {run}). Expected {split[1]}, actual {answer}");

            if (long.TryParse(split[1], out var l) && long.TryParse(answer, out var r))
            {
                Console.Write($", difference: {l - r}");
            }

            Console.WriteLine(".");

            Console.ForegroundColor = temp;

            return false;
        }

        return true;
    }

    private static void WriteYearSummary(long yearMs)
    {
        Console.WriteLine($"{new string(' ', 12)}-------------");

        Console.WriteLine(yearMs < 1_000_000
            ? $"{new string(' ', 12)}{$"{yearMs / 1_000d:N3}ms",-13}"
            : $"{new string(' ', 12)}{$"{yearMs / 1_000_000d:N3}s",-13}");

        Console.WriteLine();
    }

    private static void CleanUp()
    {
        var clear = Directory.EnumerateFiles("./").Where(f => f.EndsWith(".result")).ToList();

        foreach (var result in clear)
        {
            File.Delete(result);
        }
    }
}
