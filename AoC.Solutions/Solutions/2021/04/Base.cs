using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2021._04;

public abstract class Base : Solution
{
    public override string Description => "Squid bingo";

    protected int[] ReadDraws()
    {
        return Input[0].Split(',').Select(int.Parse).ToArray();
    }

    protected List<Board> ReadBoards()
    {
        var boards = new List<Board>();

        var line = 1;

        while (line < Input.Length)
        {
            var data = $"{Input[line + 1]} {Input[line + 2]} {Input[line + 3]} {Input[line + 4]} {Input[line + 5]}";

            boards.Add(new Board(data.Split(' ', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries)
                                     .Select(int.Parse).ToArray()));

            line += 6;
        }

        return boards;
    }

}