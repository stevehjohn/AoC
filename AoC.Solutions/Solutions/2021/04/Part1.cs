using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._04;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        var draws = ReadDraws();

        var boards = ReadBoards();

        var (lastDraw, winner) = PlayBingo(draws, boards);

        return (winner.GetUnmarked().Sum() * lastDraw).ToString();
    }

    private static (int, Board) PlayBingo(int[] draws, List<Board> boards)
    {
        foreach (var draw in draws)
        {
            foreach (var board in boards)
            {
                if (board.CheckDraw(draw))
                {
                    return (draw, board);
                }
            }
        }

        throw new PuzzleException("No winning board found.");
    }
}