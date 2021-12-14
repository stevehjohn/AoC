namespace AoC.Solutions.Infrastructure;

public abstract class Solution
{
    protected string[] Input;

    public virtual string Description => "Not yet named";

    protected Solution()
    {
        var nameSpace = GetType().Namespace ?? string.Empty;

        var parts = nameSpace.Split('.');

        var path = parts.Skip(2).Select(s => s.Replace("_", string.Empty)).ToArray();

        Input = File.ReadAllLines($"{string.Join(Path.DirectorySeparatorChar, path)}{Path.DirectorySeparatorChar}input.txt");
    }

    public abstract string GetAnswer();
}