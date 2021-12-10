namespace AoC.Infrastructure;

public abstract class Solution
{
    protected string[] Input;

    protected Solution()
    {
        var nameSpace = GetType().Namespace ?? string.Empty;

        var parts = nameSpace.Split('.');

        var path = parts.Skip(1).Select(s => s.Replace("_", string.Empty)).ToArray();

        Input = File.ReadAllLines($"{string.Join(Path.DirectorySeparatorChar, path)}{Path.DirectorySeparatorChar}input.txt");
    }

    public abstract string GetAnswer();
}