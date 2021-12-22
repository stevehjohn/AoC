using AoC.Solutions.Common;
using AoC.Solutions.Solutions._2021._19;
using Xunit;

namespace AoC.Tests.Solutions._2021._19;

public class TransformTests
{
    [Theory]
    [InlineData(515, 917, -361, -743, 427, -804, 88, 113, 1104)]
    [InlineData(404, -588, -901, -336, 658, -858, 68, 1246, 43)]
    public void TransformPointCalculatesCorrectAnswer(int x1, int y1, int z1, int x2, int y2, int z2, int dX, int dY, int dZ)
    {
        var transform = new Transform();

        transform.CalculateTransform(new Point(x1, y1, z1), new Point(x2, y2, z2), dX, dY, dZ);

        var result = transform.TransformPoint(new Point(x1, y1, z1));

        Assert.Equal(new Point(x2, y2, z2), result);
    }
}