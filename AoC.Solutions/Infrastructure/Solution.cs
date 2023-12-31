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

        Input = InputProvider.GetInput(nameSpace);
    }

    public abstract string GetAnswer();
}