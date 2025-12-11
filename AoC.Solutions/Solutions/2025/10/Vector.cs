namespace AoC.Solutions.Solutions._2025._10;

public struct Vector
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

    public static Vector operator +(in Vector a, in Vector b)
    {
        return new Vector
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

    public static Vector operator -(in Vector a, in Vector b)
    {
        return new Vector
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

    public static Vector operator *(in Vector a, int b)
    {
        return new Vector
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