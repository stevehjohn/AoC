namespace AoC.Solutions.Solutions._2022._13;

public class Part2 : Base
{
    public override string GetAnswer()
    {
        var input = Input.Where(l => !string.IsNullOrWhiteSpace(l)).Select(l => l.Replace("10", ":")).ToList();

        input.Add("[[2]]");
        input.Add("[[6]]");

        input.Sort(Compare);

        var key1 = input.IndexOf("[[2]]") + 1;

        var key2 = input.IndexOf("[[6]]") + 1;

        return (key1 * key2).ToString();
    }
}