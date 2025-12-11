using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AoC.Solutions.Solutions._2025._10
{
    // Equivalent to the C++ Machine used by Solver
    public sealed class Machine2
    {
        public int Length { get; }
        public long Target { get; }         // not needed for part 2
        public long[] Buttons { get; }
        public int[] Joltage { get; }

        public Machine2(int length, long target, long[] buttons, int[] joltage)
        {
            Length = length;
            Target = target;
            Buttons = buttons;
            Joltage = joltage;
        }
    }

    internal struct Vec
    {
        // LIMIT = 16
        public const int Limit = 16;

        public short V0, V1, V2, V3, V4, V5, V6, V7,
                     V8, V9, V10, V11, V12, V13, V14, V15;

        public short this[int index]
        {
            readonly get => index switch
            {
                0 => V0, 1 => V1, 2 => V2, 3 => V3,
                4 => V4, 5 => V5, 6 => V6, 7 => V7,
                8 => V8, 9 => V9, 10 => V10, 11 => V11,
                12 => V12, 13 => V13, 14 => V14, 15 => V15,
                _ => throw new ArgumentOutOfRangeException(nameof(index))
            };
            set
            {
                switch (index)
                {
                    case 0: V0 = value; break;
                    case 1: V1 = value; break;
                    case 2: V2 = value; break;
                    case 3: V3 = value; break;
                    case 4: V4 = value; break;
                    case 5: V5 = value; break;
                    case 6: V6 = value; break;
                    case 7: V7 = value; break;
                    case 8: V8 = value; break;
                    case 9: V9 = value; break;
                    case 10: V10 = value; break;
                    case 11: V11 = value; break;
                    case 12: V12 = value; break;
                    case 13: V13 = value; break;
                    case 14: V14 = value; break;
                    case 15: V15 = value; break;
                    default: throw new ArgumentOutOfRangeException(nameof(index));
                }
            }
        }

        public static Vec operator +(in Vec a, in Vec b)
        {
            return new Vec
            {
                V0 = (short)(a.V0 + b.V0),
                V1 = (short)(a.V1 + b.V1),
                V2 = (short)(a.V2 + b.V2),
                V3 = (short)(a.V3 + b.V3),
                V4 = (short)(a.V4 + b.V4),
                V5 = (short)(a.V5 + b.V5),
                V6 = (short)(a.V6 + b.V6),
                V7 = (short)(a.V7 + b.V7),
                V8 = (short)(a.V8 + b.V8),
                V9 = (short)(a.V9 + b.V9),
                V10 = (short)(a.V10 + b.V10),
                V11 = (short)(a.V11 + b.V11),
                V12 = (short)(a.V12 + b.V12),
                V13 = (short)(a.V13 + b.V13),
                V14 = (short)(a.V14 + b.V14),
                V15 = (short)(a.V15 + b.V15),
            };
        }

        public static Vec operator -(in Vec a, in Vec b)
        {
            return new Vec
            {
                V0 = (short)(a.V0 - b.V0),
                V1 = (short)(a.V1 - b.V1),
                V2 = (short)(a.V2 - b.V2),
                V3 = (short)(a.V3 - b.V3),
                V4 = (short)(a.V4 - b.V4),
                V5 = (short)(a.V5 - b.V5),
                V6 = (short)(a.V6 - b.V6),
                V7 = (short)(a.V7 - b.V7),
                V8 = (short)(a.V8 - b.V8),
                V9 = (short)(a.V9 - b.V9),
                V10 = (short)(a.V10 - b.V10),
                V11 = (short)(a.V11 - b.V11),
                V12 = (short)(a.V12 - b.V12),
                V13 = (short)(a.V13 - b.V13),
                V14 = (short)(a.V14 - b.V14),
                V15 = (short)(a.V15 - b.V15),
            };
        }

        public static Vec operator *(in Vec a, int b)
        {
            return new Vec
            {
                V0 = (short)(a.V0 * b),
                V1 = (short)(a.V1 * b),
                V2 = (short)(a.V2 * b),
                V3 = (short)(a.V3 * b),
                V4 = (short)(a.V4 * b),
                V5 = (short)(a.V5 * b),
                V6 = (short)(a.V6 * b),
                V7 = (short)(a.V7 * b),
                V8 = (short)(a.V8 * b),
                V9 = (short)(a.V9 * b),
                V10 = (short)(a.V10 * b),
                V11 = (short)(a.V11 * b),
                V12 = (short)(a.V12 * b),
                V13 = (short)(a.V13 * b),
                V14 = (short)(a.V14 * b),
                V15 = (short)(a.V15 * b),
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
                _reverse[i] = new List<int>();
            }

            for (var b = 0; b < _buttons; b++)
            {
                long buttonMask = machine.Buttons[b];

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
                    v[i] = (short)(v[i] / g);
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
                    v[i] = (short)(-v[i]);
                }
            }
        }

        /// <summary>
        /// Performs integer Gaussian elimination with GCD-based scaling.
        /// Returns (freeVar, maxVal).
        /// freeVar &lt; 0 means no free variable, maxVal used as flag.
        /// </summary>
        private (int freeVar, int maxVal) Gaussian(List<Vec> rows, int limit = -1)
        {
            var doClean = limit >= 0;
            var n = rows.Count;
            var m = _buttons;

            // Full elimination (above and below pivots)
            for (int h = 0, k = 0; h < n && k < m; k++)
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
                    continue; // no pivot in this column
                }

                // swap rows[h] <-> rows[pivotRow]
                (rows[h], rows[pivotRow]) = (rows[pivotRow], rows[h]);

                // eliminate above
                for (var i = 0; i < h; i++)
                {
                    if (rows[i][k] != 0)
                    {
                        int a = rows[h][k];
                        int b = rows[i][k];
                        var g = Gcd(a, b);
                        // rows[i] = rows[i] * (a/g) - rows[h] * (b/g)
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

            // Row analysis
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
                        if (coeff > 0) numGt++;

                        var sameSign = ((coeff >= 0) == (rhs >= 0)) || rhs == 0;
                        if (sameSign) numSame++;
                    }
                }

                if (count == 0)
                {
                    // 0 = rhs
                    if (rhs != 0)
                    {
                        return (-1, -1); // no solution
                    }

                    rows[j] = rows[^1];
                    rows.RemoveAt(rows.Count - 1);
                    j--;
                    continue;
                }

                if (numSame == 0)
                {
                    // all coefficients "point" opposite to rhs -> no non-negative solution
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
                    // At least 2 variables in this row
                    candidate = MinCandidate(candidate, (index: lastIndex, limit));

                    if (numSame == count && (numGt == 0 || numGt == count))
                    {
                        // All variables are same sign as rhs
                        for (var b = 0; b < Limit - 1; b++)
                        {
                            int coeff = row[b];
                            if (coeff != 0)
                            {
                                var q = rhs / coeff;
                                if (q == 0)
                                {
                                    // this variable must be zero; we can zero the column
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
                if (!doClean)
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

        private static (int freeVar, int maxVal) MinCandidate(
            (int freeVar, int maxVal) a,
            (int index, int limit) b)
        {
            // Lexicographic min like std::min on pair<int,int>
            if (b.limit < a.maxVal) return (b.index, b.limit);
            if (b.limit > a.maxVal) return a;
            if (b.index < a.freeVar) return (b.index, b.limit);
            return a;
        }

        private bool FeasibleSlow(List<Vec> rows, int freeVar, int maxVal, int limit)
        {
            for (var val = 0; val <= maxVal; val++)
            {
                // copy rows
                var cur = new List<Vec>(rows.Count + 1);
                cur.AddRange(rows);

                Vec eq = default;
                eq[freeVar] = 1;
                eq[Limit - 1] = (short)val;
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
            // Build rows: one per joltage
            var rows = new List<Vec>(_jolts);

            for (var j = 0; j < _jolts; j++)
            {
                Vec row = default;

                foreach (var b in _reverse[j])
                {
                    row[b] = 1;
                }

                row[Limit - 1] = (short)_machine.Joltage[j];
                rows.Add(row);
            }

            // initial reduction
            Gaussian(rows);

            // lower bound on total presses: max(joltage)
            var val = 0;
            foreach (int j in _machine.Joltage)
            {
                if (j > val) val = j;
            }

            for (; ; val++)
            {
                var cur = new List<Vec>(rows.Count + 1);
                cur.AddRange(rows);

                // sum of all button presses == val
                Vec sumRow = default;
                for (var b = 0; b < _buttons; b++)
                {
                    sumRow[b] = 1;
                }

                sumRow[Limit - 1] = (short)val;
                cur.Add(sumRow);

                if (Feasible(cur, val))
                {
                    return val;
                }
            }
        }
    }
}
