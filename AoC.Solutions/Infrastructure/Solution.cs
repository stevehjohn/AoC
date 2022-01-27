using AoC.Solutions.Common.Ocr;

namespace AoC.Solutions.Infrastructure;

public abstract class Solution
{
    protected string[] Input;

    public abstract string Description { get; }

    public virtual Variant? OcrOutput => null;

    protected Solution()
    {
        var nameSpace = GetType().Namespace ?? string.Empty;

        var parts = nameSpace.Split('.');

        var path = parts.Skip(2).Select(s => s.Replace("_", string.Empty)).ToArray();

        Input = File.ReadAllLines($"{string.Join(Path.DirectorySeparatorChar, path)}{Path.DirectorySeparatorChar}input.txt");
    }

    public abstract string GetAnswer();
}