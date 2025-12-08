using AoC.Solutions.Common.Ocr;
using AoC.Solutions.Infrastructure;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
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
            var year = int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0");

            var day = int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0");

            if (arguments.Length > 0
                && ! arguments[0].Equals("true", StringComparison.InvariantCultureIgnoreCase)
                && ! arguments[0].Equals("sa", StringComparison.InvariantCultureIgnoreCase))
            {
                var solutionKey =
                    $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0"):D2}.{solution.Name[4]}";

                if (solutionKey[..arguments[0].Length] != arguments[0])
                {
                    continue;
                }
            }

            if (Activator.CreateInstance(solution) is not Solution instance)
            {
                continue;
            }

            var firstTime = double.MaxValue;

            var stopwatch = new Stopwatch();

            var answer = string.Empty;

            if (arguments.Length != 1 || (arguments.Length == 1 && arguments[0].Length < 5))
            {
                stopwatch.Start();

                answer = instance.GetAnswer();

                stopwatch.Stop();

                firstTime = stopwatch.Elapsed.TotalMicroseconds;

                instance = Activator.CreateInstance(solution) as Solution;

                if (instance == null)
                {
                    continue;
                }
            }

            GC.Collect(GC.MaxGeneration, GCCollectionMode.Forced, true, true);

            // ReSharper disable once CompareOfFloatsByEqualityOperator
            if (firstTime == double.MaxValue || firstTime < TimeSpan.FromSeconds(10).TotalMicroseconds)
            {
                stopwatch.Reset();

                stopwatch.Start();

                answer = instance.GetAnswer();

                stopwatch.Stop();
            }

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
                CleanUp();

                WriteYearSummary();

                previousYear = year;

                yearMs = 0;

                GC.Collect(GC.MaxGeneration, GCCollectionMode.Aggressive, true, true);
            }

            CheckAnswer(instance.GetType(), answer);

            var description = string.Empty;

            if (instance.Description != previousDesc)
            {
                description = instance.Description;

                previousDesc = description;
            }

            var microseconds = Math.Min(stopwatch.Elapsed.TotalMicroseconds, firstTime);

            var part = solution.Name[4];

            var summary = $" {year} {day,2}.{part}: {$"{microseconds:N0}μs",-12}  {description}";

            Console.WriteLine(summary);

            if (showAnswer)
            {
                Console.WriteLine($"            {answer}");
            }

            if (! answer.Equals("Unknown", StringComparison.InvariantCultureIgnoreCase))
            {
                results.Add((year, day, part), (microseconds, summary));

                yearMs += (long) microseconds;

                totalMs += (long) microseconds;

                count++;
            }
        }

        CleanUp();

        WriteYearSummary();

        Console.WriteLine($" {count} puzzle{(count != 1 ? "s" : string.Empty)} solved in {totalMs / 1_000_000d:N3}s.\n");

        if (arguments.Length > 0)
        {
            if (arguments[0].Equals("true", StringComparison.InvariantCultureIgnoreCase) || (arguments.Length > 1 && arguments[1].Equals("true", StringComparison.InvariantCultureIgnoreCase)))
            {
                UpdateResults();
            }
        }

        return;

        void UpdateResults()
        {
            const string resultsFileName = "./results.md";

            if (! File.Exists(resultsFileName))
            {
                Console.WriteLine(" results.md not found, will not update.\n");
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

                    var time = int.Parse(line[12..].Split(' ', StringSplitOptions.TrimEntries)[0][..^2], NumberStyles.AllowThousands);

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

                Console.WriteLine($" {updated} time(s0 were updated in results.md.\n");
            }
        }

        void RecalculateTotals(List<string> file)
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
                    file[i] = $" {puzzles} puzzle{(count != 1 ? "s" : string.Empty)} solved in {overall / 1_000_000d:N3}s.";
                }

                if (line.StartsWith(" 20") && year == 0)
                {
                    sum = 0;

                    year = int.Parse(line[1..5]);
                }

                if (line.StartsWith(" 20") && year != 0)
                {
                    var time = int.Parse(line[12..].Split(' ', StringSplitOptions.TrimEntries)[0][..^2], NumberStyles.AllowThousands);

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

        void CheckAnswer(Type solution, string answer)
        {
            if (answer == "Unknown")
            {
                var temp = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Blue;

                Console.WriteLine(" This seems to be an in progress puzzle.");

                Console.ForegroundColor = temp;

                return;
            }

            var key =
                $"{int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0")}.{int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0")}.{solution.Name[4]}";

            var correctAnswerLine = answers.FirstOrDefault(a => a.StartsWith(key));

            if (correctAnswerLine == null)
            {
                var temp = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.DarkYellow;

                Console.WriteLine($" Please add the correct answer for {key} to AllAnswers.clear.");

                Console.ForegroundColor = temp;

                return;
            }

            var split = correctAnswerLine.Split(": ");

            if (split[1] != answer)
            {
                var temp = Console.ForegroundColor;

                Console.ForegroundColor = ConsoleColor.Magenta;

                Console.Write($" Incorrect answer for {key}. Expected {split[1]}, actual {answer}");

                if (long.TryParse(split[1], out var l) && long.TryParse(answer, out var r))
                {
                    Console.Write($", difference: {l - r}");
                }

                Console.WriteLine(".");

                Console.ForegroundColor = temp;
            }
        }

        void WriteYearSummary()
        {
            Console.WriteLine($"{new string(' ', 12)}-------------");

            Console.WriteLine(yearMs < 1_000_000
                ? $"{new string(' ', 12)}{$"{yearMs / 1_000d:N3}ms",-13}"
                : $"{new string(' ', 12)}{$"{yearMs / 1_000_000d:N3}s",-13}");

            Console.WriteLine();
        }

        void CleanUp()
        {
            var clear = Directory.EnumerateFiles("./").Where(f => f.EndsWith(".result")).ToList();

            foreach (var result in clear)
            {
                File.Delete(result);
            }
        }
    }
}