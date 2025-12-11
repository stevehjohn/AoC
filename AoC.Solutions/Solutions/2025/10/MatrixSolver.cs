namespace AoC.Solutions.Solutions._2025._10;

public sealed class MatrixSolver
{
    private const int Limit = Vector.Limit;

    private readonly Matrix _matrix;
    
    private readonly int _values;
    
    private readonly int _totals;

    private readonly List<int>[] _reverse = new List<int>[Limit];

    public MatrixSolver(Matrix machine)
    {
        _matrix = machine;
        
        _values = machine.Rows.Length;
        
        _totals = machine.Totals.Length;

        for (var i = 0; i < Limit; i++)
        {
            _reverse[i] = [];
        }

        for (var b = 0; b < _values; b++)
        {
            var buttonMask = machine.Rows[b];

            for (var j = 0; j < _totals; j++)
            {
                if ((buttonMask & (1L << j)) != 0)
                {
                    _reverse[j].Add(b);
                }
            }
        }
    }

    public int Solve()
    {
        var rows = new List<Vector>(_totals);

        for (var j = 0; j < _totals; j++)
        {
            Vector row = default;

            foreach (var b in _reverse[j])
            {
                row[b] = 1;
            }

            row[Limit - 1] = (short) _matrix.Totals[j];
            
            rows.Add(row);
        }

        Gaussian(rows);
        
        var val = 0;
        
        foreach (var j in _matrix.Totals)
        {
            if (j > val) val = j;
        }

        for (;; val++)
        {
            var cur = new List<Vector>(rows.Count + 1);
            
            cur.AddRange(rows);

            Vector sumRow = default;

            for (var b = 0; b < _values; b++)
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

    private void Simplify(ref Vector v)
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
        
        for (; first < _values; first++)
        {
            if (v[first] != 0)
            {
                break;
            }
        }

        if (first < _values && v[first] < 0)
        {
            for (var i = 0; i < Limit; i++)
            {
                v[i] = (short) -v[i];
            }
        }
    }

    private (int freeVar, int maxVal) Gaussian(List<Vector> rows, int limit = -1)
    {
        var doClean = limit >= 0;

        var n = rows.Count;

        for (int h = 0, k = 0; h < n && k < _values; k++)
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

    private bool FeasibleSlow(List<Vector> rows, int freeVar, int maxVal, int limit)
    {
        for (var val = 0; val <= maxVal; val++)
        {
            var cur = new List<Vector>(rows.Count + 1);

            cur.AddRange(rows);

            Vector eq = default;
            
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

    private bool Feasible(List<Vector> rows, int limit)
    {
        var (freeVar, maxVal) = Gaussian(rows, limit);

        if (freeVar < 0)
        {
            return maxVal == 0;
        }

        return FeasibleSlow(rows, freeVar, maxVal, limit);
    }
}