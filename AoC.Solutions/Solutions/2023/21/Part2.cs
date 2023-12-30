using AoC.Solutions.Extensions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._21;

[UsedImplicitly]
public class Part2 : Base
{
    private const long TargetSteps = 26_501_365;

    private const int Buffers = 8;

    private long[] _counts;

    private readonly HashSet<(int X, int Y, int Ux, int Uy)>[] _buffers = new HashSet<(int X, int Y, int Ux, int Uy)>[Buffers];

    private int _source;

    private int _target = Buffers - 1;

    private static readonly (int, int) North = (0, -1);

    private static readonly (int, int) East = (1, 0);

    private static readonly (int, int) South = (0, 1);

    private static readonly (int, int) West = (-1, 0);

    public override string GetAnswer()
    {
        var start = ParseInput();

        Walk(start, Width * 2 + Width / 2 + 1);

        var result = ExtrapolateAnswer();

        return result.ToString();
    }

    private void Walk((int X, int Y) start, int maxSteps)
    {
        for (var i = 0; i < Buffers; i++)
        {
            _buffers[i] = new HashSet<(int X, int Y, int Ux, int Uy)>();
        }

        _buffers[_source].Add((start.X, start.Y, 0, 0));

        _counts = new long[maxSteps];

        _counts[0] = 1;

        var step = 1;

        while (step < maxSteps)
        {
            var count = ProcessStep(_buffers[_source], _buffers[_target]);
            
            _counts[step] = count;

            var copySource = new HashSet<(int X, int Y, int Ux, int Uy)>(_buffers[_source]);
            
            copySource.ExceptWith(_buffers[_source.DecrementRotate(Buffers, 2)]);
            
            var target = new HashSet<(int X, int Y, int Ux, int Uy)>();
            
            count = ProcessStep(copySource, target);
            
            if (step >= Buffers)
            {
                count += _counts[step - Buffers];
            }

            Console.WriteLine($"Step: {step} - {_counts[step]} - {count} Diff: {_counts[step] - count}");

            _counts[step] = count;
            
            Dump([_buffers[_source], _buffers[_target], copySource, target]);

            _source = _source.DecrementRotate(Buffers);

            _target = _target.DecrementRotate(Buffers);

            _buffers[_target].Clear();

            step++;
        }
    }

    private long ProcessStep(HashSet<(int X, int Y, int Ux, int Uy)> source, HashSet<(int X, int Y, int Ux, int Uy)> target)
    {
        var count = 0;
        
        foreach (var position in source)
        {
            count += Move(target, position, North);

            count += Move(target, position, South);

            count += Move(target, position, East);

            count += Move(target, position, West);
        }

        return count;
    }

    private void Dump(HashSet<(int X, int Y, int Ux, int Uy)>[] t)
    {
        Console.WriteLine();
        
        const int offset = 54;

        for (var y = offset; y < Width - offset; y++)
        {
            for (var i = 0; i < t.Length; i++)
            {
                for (var x = offset; x < Height - offset; x++)
                {
                    if (i > 1)
                    {
                        if (t[i].Any(p => p.X == x && p.Y == y))
                        {
                            Console.Write('O');
                        
                            continue;
                        }

                        if (t[i - 2].Any(p => p.X == x && p.Y == y))
                        {
                            Console.ForegroundColor = ConsoleColor.Magenta;
                            
                            Console.Write('O');

                            Console.ForegroundColor = ConsoleColor.Green;
                            
                            continue;
                        }
                    }
                    else
                    {
                        if (t[i].Any(p => p.X == x && p.Y == y))
                        {
                            Console.Write('O');
                        
                            continue;
                        }
                    }

                    if (Map[x, y] == '#')
                    {
                        Console.Write('.');
                    }
                    else
                    {
                        Console.Write(' ');
                    }
                }
                
                Console.Write("    ");
            }

            Console.WriteLine();
        }

        Console.WriteLine();

        Console.ReadKey();
    }

    private int Move(HashSet<(int X, int Y, int Ux, int Uy)> target, (int X, int Y, int Ux, int Uy) position, (int X, int Y) direction)
    {
        position = (position.X + direction.X, position.Y + direction.Y, position.Ux, position.Uy);

        if (position.X < 0)
        {
            position.X = Width - 1;
            position.Ux--;
        }

        if (position.X == Width)
        {
            position.X = 0;
            position.Ux++;
        }

        if (position.Y < 0)
        {
            position.Y = Height - 1;
            position.Uy--;
        }

        if (position.Y == Height)
        {
            position.Y = 0;
            position.Uy++;
        }

        if (Map[position.X, position.Y] == '#')
        {
            return 0;
        }

        return target.Add(position) ? 1 : 0;
    }

    private long ExtrapolateAnswer()
    {
        var halfWidth = Width / 2;

        var delta1 = _counts[halfWidth + Width] - _counts[halfWidth];

        var delta2 = _counts[halfWidth + Width * 2] - _counts[halfWidth + Width];

        var quotient = TargetSteps / Width;

        var result = _counts[halfWidth] + delta1 * quotient + quotient * (quotient - 1) / 2 * (delta2 - delta1);

        return result;
    }
}