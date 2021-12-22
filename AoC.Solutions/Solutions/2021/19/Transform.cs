﻿using AoC.Solutions.Common;
using AoC.Solutions.Exceptions;

namespace AoC.Solutions.Solutions._2021._19;

public class Transform
{
    private readonly Axis[] _mappings = new Axis[3];

    private readonly int[] _deltas = new int[3];

    private readonly Sign[] _signs = new Sign[3];

    private readonly Sign[] _flipResult = new Sign[3];

    public Point TransformPoint(Point origin)
    {
        var point = new Point(origin)
                    {
                        X = ((_signs[0] == Sign.Negative ? -1 : 1)
                             * (_mappings[0] == Axis.X
                                    ? origin.X
                                    : _mappings[0] == Axis.Y
                                        ? origin.Y
                                        : origin.Z)
                             + _deltas[0]) * (_flipResult[0] == Sign.Negative ? -1 : 1),

                        Y = ((_signs[1] == Sign.Negative ? -1 : 1)
                             * (_mappings[1] == Axis.X
                                    ? origin.X
                                    : _mappings[1] == Axis.Y
                                        ? origin.Y
                                        : origin.Z)
                             + _deltas[1]) * (_flipResult[1] == Sign.Negative ? -1 : 1),

                        Z = ((_signs[2] == Sign.Negative ? -1 : 1)
                             * (_mappings[2] == Axis.X
                                    ? origin.X
                                    : _mappings[2] == Axis.Y
                                        ? origin.Y
                                        : origin.Z)
                             + _deltas[2]) * (_flipResult[2] == Sign.Negative ? -1 : 1)
        };

        return point;
    }

    public void CalculateTransform(Point origin, Point target, int xDelta, int yDelta, int zDelta)
    {
        // Getting there... there's an issue with the origin's +/- and the delta's +/-

        TryAxisMapping(origin.X, target.Y, xDelta, Axis.X, Axis.Y);

        TryAxisMapping(origin.Y, target.Z, yDelta, Axis.Y, Axis.Z);

        TryAxisMapping(origin.Z, target.X, zDelta, Axis.Z, Axis.X);
    }

    private void TryAxisMapping(int left, int right, int delta, Axis origin, Axis target)
    {
        if (left + delta == -right)
        {
            _mappings[(int) target] = origin;

            _deltas[(int) target] = delta;

            _signs[(int) target] = Sign.Positive;

            _flipResult[(int) target] = Sign.Negative;

            return;
        }
        
        if (left - delta == right)
        {
            _mappings[(int) target] = origin;

            _deltas[(int) target] = -delta;

            _signs[(int) target] = Sign.Positive;

            _flipResult[(int)target] = Sign.Positive;

            return;
        }

        if (-left + delta == right)
        {
            _mappings[(int) target] = origin;

            _deltas[(int) target] = delta;

            _signs[(int) target] = Sign.Negative;

            _flipResult[(int)target] = Sign.Positive;

            return;
        }

        throw new PuzzleException("You've missed a case.");
    }
}