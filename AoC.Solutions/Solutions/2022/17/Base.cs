using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2022._17;

public abstract class Base : Solution
{
    public override string Description => "Pyroclastic flow";

    private const int MapHeight = 10_000;

    private const int WindowSize = 50;

    private string _wind;

    private readonly int[][] _rocks =
    {
        new[]
        {
            0b0011110
        },
        new[]
        {
            0b0001000,
            0b0011100,
            0b0001000
        },
        new[]
        {
            0b0000100,
            0b0000100,
            0b0011100
        },
        new[]
        {
            0b0010000,
            0b0010000,
            0b0010000,
            0b0010000
        },
        new[]
        {
            0b0011000,
            0b0011000
        }
    };

    private readonly int[] _masks =
    {
        0b0011110,
        0b0011100,
        0b0011100,
        0b0010000,
        0b0011000
    };

    private readonly int[] _map = new int[MapHeight];

    private int _highPoint = MapHeight - 1;

    protected int Solve(bool findPattern = false)
    {
        Console.CursorVisible = false;

        _wind = Input[0];

        var rockIndex = 0;

        var windIndex = 0;

        var cycles = findPattern ? MapHeight : 2022;

        for (var i = 0; i < cycles; i++)
        {
            var rock = _rocks[rockIndex].ToArray();

            var mask = _masks[rockIndex];

            rockIndex++;

            if (rockIndex >= _rocks.Length)
            {
                rockIndex = 0;
            }

            var y = _highPoint - 2 - rock.Length;

            while (true)
            {
                var wind = _wind[windIndex];

                windIndex++;

                if (windIndex>= _wind.Length)
                {
                    windIndex = 0;
                }

                if (rockIndex == 3)
                {
                }

                if (wind == '<' && (mask & 0b1000000) == 0)
                {
                    var ok = true;

                    for (var rY = 0; rY < rock.Length; rY++)
                    {
                        if ((rock[rY] << 1 & _map[y + rY]) > 0)
                        {
                            ok = false;

                            break;
                        }
                    }

                    if (ok)
                    {
                        mask <<= 1;

                        for (var rY = 0; rY < rock.Length; rY++)
                        {
                            rock[rY] <<= 1;
                        }
                    }
                }

                if (wind == '>' && (mask & 0b0000001) == 0)
                {
                    var ok = true;

                    for (var rY = 0; rY < rock.Length; rY++)
                    {
                        if ((rock[rY] >> 1 & _map[y + rY]) > 0)
                        {
                            ok = false;

                            break;
                        }
                    }

                    if (ok)
                    {
                        mask >>= 1;

                        for (var rY = 0; rY < rock.Length; rY++)
                        {
                            rock[rY] >>= 1;
                        }
                    }
                }

                var stop = false;

                if (y + rock.Length < MapHeight)
                {
                    for (var rY = 0; rY < rock.Length; rY++)
                    {
                        if ((_map[y + rY + 1] & rock[rY]) > 0)
                        {
                            stop = true;

                            break;
                        }
                    }
                }

                if (stop || y + rock.Length >= MapHeight)
                {
                    for (var rY = 0; rY < rock.Length; rY++)
                    {
                        _map[y + rY] |= rock[rY];
                    }

                    if (y - 1 < _highPoint)
                    {
                        _highPoint = y - 1;
                    }

                    if (findPattern)
                    {
                        var pattern = FindPattern(i);

                        if (pattern > 0)
                        {
                        }
                    }

                    break;
                }

                y++;

                //if (rockIndex == 0 && windIndex == 0)
                //{
                //    Console.SetCursorPosition(0, 1);

                //    for (var sY = _highPoint; sY < Math.Min(_highPoint + 50, MapHeight); sY++)
                //    {
                //        Write(_map[sY]);

                //        if (sY == _highPoint)
                //        {
                //            Console.Write($" {_highPoint} ({i})    ");
                //        }

                //        Console.WriteLine();
                //    }
                //}
            }
        }

        return MapHeight - _highPoint - 1;
    }

    private readonly Dictionary<int, (int Y, int Cycle)> _hashes = new();

    private int _previousCycle;

    private int _previousPeriod;

    private int _startHeight;

    private long FindPattern(int cycle)
    {
        if (MapHeight - _highPoint < WindowSize)
        {
            return 0;
        }

        var hash = new HashCode();

        for (var y = _highPoint; y < _highPoint + WindowSize; y++)
        {
            hash.Add(_map[y]);
        }

        var hashCode = hash.ToHashCode();

        if (_hashes.ContainsKey(hashCode))
        {
            var period = _hashes[hashCode].Y - _highPoint;

            if (_startHeight == 0)
            {
                _startHeight = _hashes[hashCode].Y;
            }

            if (_previousPeriod == 0)
            {
                _previousPeriod = period;

                _previousCycle = cycle;
            }
            else if (_previousPeriod != period)
            {
                var approximation = 1000000000000L / (cycle - _previousCycle) * _previousPeriod;

                return approximation;
            }

            Console.WriteLine($"{_highPoint} seen at {_hashes[hashCode]}, period {period}: Cycle {cycle}");
        }
        else
        {
            _hashes.Add(hash.ToHashCode(), (_highPoint, cycle));
        }

        return 0;
    }

    private void Write(int value)
    {
        Console.Write((value & 0b1000000) > 0 ? '#' : '.');
        Console.Write((value & 0b0100000) > 0 ? '#' : '.');
        Console.Write((value & 0b0010000) > 0 ? '#' : '.');
        Console.Write((value & 0b0001000) > 0 ? '#' : '.');
        Console.Write((value & 0b0000100) > 0 ? '#' : '.');
        Console.Write((value & 0b0000010) > 0 ? '#' : '.');
        Console.Write((value & 0b0000001) > 0 ? '#' : '.');
    }
}