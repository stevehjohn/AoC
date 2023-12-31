namespace AoC.Solutions.Solutions._2019._18;

public interface INodeWalker
{
    int Steps { get; }

    List<char> AllVisited { get; }

    int VisitedCount { get; }

    string Signature { get; }

    List<INodeWalker> Walk();
}