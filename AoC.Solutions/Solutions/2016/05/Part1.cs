using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2016._05;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        return new string(
            GetMatches().Take(8).Select(x => ToHex(x.Sixth)).ToArray());
    }}