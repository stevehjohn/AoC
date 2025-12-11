using System.Diagnostics;

namespace AoC.Solutions.Solutions._2025._10;

public sealed class Machine2
{
    public long[] Buttons { get; }
    public int[] Joltage { get; }

    public Machine2(long[] buttons, int[] joltage)
    {
        Buttons = buttons;
        Joltage = joltage;
    }
}

internal struct Vec
{
    public const int Limit = 16;

    private short _v0, _v1, _v2, _v3, _v4, _v5, _v6, _v7, _v8, _v9, _v10, _v11, _v12, _v13, _v14, _v15;

    public short this[int index]
    {
        readonly get => index switch
        {
            0 => _v0, 1 => _v1, 2 => _v2, 3 => _v3,
            4 => _v4, 5 => _v5, 6 => _v6, 7 => _v7,
            8 => _v8, 9 => _v9, 10 => _v10, 11 => _v11,
            12 => _v12, 13 => _v13, 14 => _v14, 15 => _v15,
            _ => throw new ArgumentOutOfRangeException(nameof(index))
        };

        set
        {
            switch (index)
            {
                case 0: _v0 = value; break;
                case 1: _v1 = value; break;
                case 2: _v2 = value; break;
                case 3: _v3 = value; break;
                case 4: _v4 = value; break;
                case 5: _v5 = value; break;
                case 6: _v6 = value; break;
                case 7: _v7 = value; break;
                case 8: _v8 = value; break;
                case 9: _v9 = value; break;
                case 10: _v10 = value; break;
                case 11: _v11 = value; break;
                case 12: _v12 = value; break;
                case 13: _v13 = value; break;
                case 14: _v14 = value; break;
                case 15: _v15 = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(index));
            }
        }
    }

    public static Vec operator +(in Vec a, in Vec b)
    {
        return new Vec
        {
            _v0 = (short) (a._v0 + b._v0),
            _v1 = (short) (a._v1 + b._v1),
            _v2 = (short) (a._v2 + b._v2),
            _v3 = (short) (a._v3 + b._v3),
            _v4 = (short) (a._v4 + b._v4),
            _v5 = (short) (a._v5 + b._v5),
            _v6 = (short) (a._v6 + b._v6),
            _v7 = (short) (a._v7 + b._v7),
            _v8 = (short) (a._v8 + b._v8),
            _v9 = (short) (a._v9 + b._v9),
            _v10 = (short) (a._v10 + b._v10),
            _v11 = (short) (a._v11 + b._v11),
            _v12 = (short) (a._v12 + b._v12),
            _v13 = (short) (a._v13 + b._v13),
            _v14 = (short) (a._v14 + b._v14),
            _v15 = (short) (a._v15 + b._v15)
        };
    }

    public static Vec operator -(in Vec a, in Vec b)
    {
        return new Vec
        {
            _v0 = (short) (a._v0 - b._v0),
            _v1 = (short) (a._v1 - b._v1),
            _v2 = (short) (a._v2 - b._v2),
            _v3 = (short) (a._v3 - b._v3),
            _v4 = (short) (a._v4 - b._v4),
            _v5 = (short) (a._v5 - b._v5),
            _v6 = (short) (a._v6 - b._v6),
            _v7 = (short) (a._v7 - b._v7),
            _v8 = (short) (a._v8 - b._v8),
            _v9 = (short) (a._v9 - b._v9),
            _v10 = (short) (a._v10 - b._v10),
            _v11 = (short) (a._v11 - b._v11),
            _v12 = (short) (a._v12 - b._v12),
            _v13 = (short) (a._v13 - b._v13),
            _v14 = (short) (a._v14 - b._v14),
            _v15 = (short) (a._v15 - b._v15)
        };
    }

    public static Vec operator *(in Vec a, int b)
    {
        return new Vec
        {
            _v0 = (short) (a._v0 * b),
            _v1 = (short) (a._v1 * b),
            _v2 = (short) (a._v2 * b),
            _v3 = (short) (a._v3 * b),
            _v4 = (short) (a._v4 * b),
            _v5 = (short) (a._v5 * b),
            _v6 = (short) (a._v6 * b),
            _v7 = (short) (a._v7 * b),
            _v8 = (short) (a._v8 * b),
            _v9 = (short) (a._v9 * b),
            _v10 = (short) (a._v10 * b),
            _v11 = (short) (a._v11 * b),
            _v12 = (short) (a._v12 * b),
            _v13 = (short) (a._v13 * b),
            _v14 = (short) (a._v14 * b),
            _v15 = (short) (a._v15 * b)
        };
    }
}

public sealed class Solver
{
    private const int Limit = Vec.Limit;

    private readonly Machine2 _machine;
    private readonly int _buttons;
    private readonly int _jolts;

    // reverse[j] = list of buttons that affect jolt j
    private readonly List<int>[] _reverse = new List<int>[Limit];

    public Solver(Machine2 machine)
    {
        _machine = machine;
        _buttons = machine.Buttons.Length;
        _jolts = machine.Joltage.Length;

        Debug.Assert(_buttons + 1 <= Limit);
        Debug.Assert(_jolts + 1 <= Limit);

        for (var i = 0; i < Limit; i++)
        {
            _reverse[i] = [];
        }

        for (var b = 0; b < _buttons; b++)
        {
            var buttonMask = machine.Buttons[b];

            for (var j = 0; j < _jolts; j++)
            {
                if ((buttonMask & (1L << j)) != 0)
                {
                    _reverse[j].Add(b);
                }
            }
        }
    }

    private static int Gcd(int a, int b)
    {
        a = Math.Abs(a);
        b = Math.Abs(b);
        while (b != 0)
        {
            var t = a % b;
            a = b;
            b = t;
        }

        return a;
    }

    private void Simplify(ref Vec v)
    {
        var g = 0;
        for (var i = 0; i < Limit; i++)
        {
            g = Gcd(g, v[i]);
        }

        if (g > 1)
        {
            for (var i = 0; i < Limit; i++)
            {
                v[i] = (short) (v[i] / g);
            }
        }

        var first = 0;
        for (; first < _buttons; first++)
        {
            if (v[first] != 0)
            {
                break;
            }
        }

        if (first < _buttons && v[first] < 0)
        {
            for (var i = 0; i < Limit; i++)
            {
                v[i] = (short) -v[i];
            }
        }
    }

    private (int freeVar, int maxVal) Gaussian(List<Vec> rows, int limit = -1)
    {
        var doClean = limit >= 0;

        var n = rows.Count;

        for (int h = 0, k = 0; h < n && k < _buttons; k++)
        {
            var pivotRow = h;

            for (var i = h; i < n; i++)
            {
                if (rows[i][k] != 0)
                {
                    pivotRow = i;
                    break;
                }
            }

            if (rows[pivotRow][k] == 0)
            {
                continue;
            }

            (rows[h], rows[pivotRow]) = (rows[pivotRow], rows[h]);

            for (var i = 0; i < h; i++)
            {
                if (rows[i][k] != 0)
                {
                    int a = rows[h][k];

                    int b = rows[i][k];

                    var g = Gcd(a, b);

                    rows[i] = rows[i] * (a / g) - rows[h] * (b / g);
                }
            }

            // eliminate below
            for (var i = pivotRow + 1; i < n; i++)
            {
                if (rows[i][k] != 0)
                {
                    int a = rows[h][k];

                    int b = rows[i][k];

                    var g = Gcd(a, b);

                    rows[i] = rows[i] * (a / g) - rows[h] * (b / g);
                }
            }

            h++;
        }

        var candidate = (freeVar: int.MaxValue, maxVal: int.MaxValue);

        Span<int> zeroInds = stackalloc int[Limit];

        var removedZeros = 0;

        for (var j = 0; j < rows.Count; j++)
        {
            var row = rows[j];

            var count = 0;

            var lastIndex = -1;

            var numSame = 0;

            var numGt = 0;

            int rhs = row[Limit - 1];

            for (var b = 0; b < Limit - 1; b++)
            {
                int coeff = row[b];

                if (coeff != 0)
                {
                    count++;

                    lastIndex = b;

                    if (coeff > 0)
                    {
                        numGt++;
                    }

                    var sameSign = coeff >= 0 == rhs >= 0 || rhs == 0;

                    if (sameSign)
                    {
                        numSame++;
                    }
                }
            }

            if (count == 0)
            {
                if (rhs != 0)
                {
                    return (-1, -1);
                }

                rows[j] = rows[^1];

                rows.RemoveAt(rows.Count - 1);

                j--;

                continue;
            }

            if (numSame == 0)
            {
                return (-1, -1);
            }

            if (count == 1)
            {
                int coeff = row[lastIndex];

                if (rhs % coeff != 0)
                {
                    return (-1, -1);
                }

                var val = rhs / coeff;

                if (val < 0)
                {
                    return (-1, -1);
                }

                if (doClean)
                {
                    rows[j] = rows[^1];

                    rows.RemoveAt(rows.Count - 1);

                    j--;
                }
            }
            else
            {
                candidate = MinCandidate(candidate, (index: lastIndex, limit));

                if (numSame == count && (numGt == 0 || numGt == count))
                {
                    for (var b = 0; b < Limit - 1; b++)
                    {
                        int coeff = row[b];

                        if (coeff != 0)
                        {
                            var q = rhs / coeff;

                            if (q == 0)
                            {
                                for (var j2 = 0; j2 < rows.Count; j2++)
                                {
                                    var r2 = rows[j2];

                                    r2[b] = 0;

                                    rows[j2] = r2;
                                }

                                zeroInds[removedZeros++] = b;

                                continue;
                            }

                            candidate = MinCandidate(candidate, (b, q));
                        }
                    }
                }
            }
        }

        if (removedZeros > 0)
        {
            if (! doClean)
            {
                for (var z = 0; z < removedZeros; z++)
                {
                    var zeroInd = zeroInds[z];

                    Vec newRow = default;

                    newRow[zeroInd] = 1;

                    rows.Add(newRow);
                }
            }

            return Gaussian(rows, doClean ? 0 : -1);
        }

        for (var i = 0; i < rows.Count; i++)
        {
            var v = rows[i];

            Simplify(ref v);

            rows[i] = v;
        }

        if (candidate.freeVar != int.MaxValue)
        {
            return candidate;
        }

        return (-1, 0);
    }

    private static (int freeVar, int maxVal) MinCandidate((int freeVar, int maxVal) a, (int index, int limit) b)
    {
        if (b.limit < a.maxVal)
        {
            return (b.index, b.limit);
        }

        if (b.limit > a.maxVal)
        {
            return a;
        }

        if (b.index < a.freeVar)
        {
            return (b.index, b.limit);
        }

        return a;
    }

    private bool FeasibleSlow(List<Vec> rows, int freeVar, int maxVal, int limit)
    {
        for (var val = 0; val <= maxVal; val++)
        {
            var cur = new List<Vec>(rows.Count + 1);

            cur.AddRange(rows);

            Vec eq = default;
            
            eq[freeVar] = 1;
            
            eq[Limit - 1] = (short) val;
            
            cur.Add(eq);

            if (Feasible(cur, limit))
            {
                return true;
            }
        }

        return false;
    }

    private bool Feasible(List<Vec> rows, int limit)
    {
        var (freeVar, maxVal) = Gaussian(rows, limit);

        if (freeVar < 0)
        {
            return maxVal == 0;
        }

        return FeasibleSlow(rows, freeVar, maxVal, limit);
    }

    public int Solve()
    {
        var rows = new List<Vec>(_jolts);

        for (var j = 0; j < _jolts; j++)
        {
            Vec row = default;

            foreach (var b in _reverse[j])
            {
                row[b] = 1;
            }

            row[Limit - 1] = (short) _machine.Joltage[j];
            
            rows.Add(row);
        }

        Gaussian(rows);
        
        var val = 0;
        
        foreach (var j in _machine.Joltage)
        {
            if (j > val) val = j;
        }

        for (;; val++)
        {
            var cur = new List<Vec>(rows.Count + 1);
            
            cur.AddRange(rows);

            Vec sumRow = default;

            for (var b = 0; b < _buttons; b++)
            {
                sumRow[b] = 1;
            }

            sumRow[Limit - 1] = (short) val;
            
            cur.Add(sumRow);

            if (Feasible(cur, val))
            {
                return val;
            }
        }
    }
}