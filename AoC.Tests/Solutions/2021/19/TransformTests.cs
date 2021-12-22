using AoC.Solutions.Common;
using AoC.Solutions.Solutions._2021._19;
using Xunit;

namespace AoC.Tests.Solutions._2021._19;

public class TransformTests
{
    [Fact]
    public void TransformPointCalculatesCorrectAnswer()
    {
        var transform = new Transform();

        transform.CalculateTransform(new Point(515, 917, -361), new Point(-743, 427, -804), 88, 113, 1104);

        var result = transform.TransformPoint(new Point(515, 917, -361));

        Assert.Equal(new Point(-743, 427, -804), result);
    }
}