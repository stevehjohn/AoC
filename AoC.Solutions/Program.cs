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

        var yearMs = 0L;

        int? previousYear = null;

        Console.WriteLine();

        string[] answers = null;

        var results = new Dictionary<(int Yeat, int Day, int Part), (double Microseconds, string Summary)>();
        
        try
        {
            answers = File.ReadAllLines($"Solutions{Path.DirectorySeparatorChar}AllAnswers.txt");
        }
        catch
        {
            //
        }

        if (answers == null)
        {
            answers = File.ReadAllLines($"./Aoc.Solutions/Solutions{Path.DirectorySeparatorChar}AllAnswers.txt");
        }

        var previousDesc = string.Empty;

        foreach (var solution in solutions)
        {
            var year = int.Parse(solution.Namespace?.Split('.')[3].Replace("_", string.Empty) ?? "0");

            var day = int.Parse(solution.Namespace?.Split('.')[4].Replace("_", string.Empty) ?? "0");

            if (new DateTime(year, 12, day) > DateTime.UtcNow)
            {
                continue;
            }

            if (arguments.Length > 0 && arguments[0].ToLower() != "true")
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

            GC.Collect();

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

            var displayAnswer = answer.Length < 26
                                    ? answer
                                    : $"{answer[..26]}...";

            var description = string.Empty;

            if (instance.Description != previousDesc)
            {
                description = instance.Description;

                previousDesc = description;
            }

            var microseconds = Math.Min(stopwatch.Elapsed.TotalMicroseconds, firstTime);

            var part = solution.Name[4] - '0';
            
            var summary = $" {year} {day,2}.{part}: {displayAnswer,-30} {$"{microseconds:N0}μs",-12}  {description}";
            
            results.Add((year, day, part), (microseconds, summary));
            
            Console.WriteLine(summary);

            yearMs += (long) microseconds;
        }

        CleanUp();
        
        WriteYearSummary();

        if (arguments.Length > 0)
        {
            if (arguments[0].ToLower() == "true" || (arguments.Length > 1 && arguments[1].ToLower() == "true"))
            {
                UpdateResults();
            }
        }
        
        return;

        void UpdateResults()
        {
            if (!File.Exists("../results.md"))
            {
                Console.WriteLine("results.md not found, will not update.\n");
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
            Console.WriteLine($"{new string(' ', 43)}-------------");

            if (yearMs < 1_000_000)
            {
                Console.WriteLine($"{new string(' ', 43)}{$"{yearMs / 1_000d:N3}ms", -13}");
            }
            else
            {
                Console.WriteLine($"{new string(' ', 43)}{$"{yearMs / 1_000_000d:N3}s", -13}");
            }

            Console.WriteLine();
        }

        void CleanUp()
        {
            var results = Directory.EnumerateFiles("./").Where(f => f.EndsWith(".result")).ToList();

            foreach (var result in results)
            {
                File.Delete(result);
            }
        }
    }
}