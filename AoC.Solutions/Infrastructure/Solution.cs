using AoC.Solutions.Common.Ocr;

namespace AoC.Solutions.Infrastructure;

public abstract class Solution
{
    protected readonly string[] Input;

    public abstract string Description { get; }

    public virtual Variant? OcrOutput => null;

    protected Solution()
    {
        var nameSpace = GetType().Namespace ?? string.Empty;

        var unlock = DateTime.UtcNow.AddHours(-5);

        if (int.Parse(GetType().FullName!.Split('.')[^3][1..]) < unlock.Year || unlock.Day >= int.Parse(GetType().FullName!.Split('.')[^2][1..]))
        {
            Input = InputProvider.GetInput(nameSpace);
        }
    }

    public abstract string GetAnswer();
}