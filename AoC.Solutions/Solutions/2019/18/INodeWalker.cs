namespace AoC.Solutions.Solutions._2019._18;

public interface INodeWalker
{
    int Steps { get; }

    HashSet<char> Visited { get; }

    int VisitedCount { get; }

    string Signature { get; }

    List<INodeWalker> Walk();
}