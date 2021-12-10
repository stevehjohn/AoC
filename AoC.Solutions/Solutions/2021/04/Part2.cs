using AoC.Solutions.Exceptions;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2021._04;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var draws = ReadDraws();

        var boards = ReadBoards();

        var (lastDraw, loser) = PlayBingo(draws, boards);

        return (loser.GetUnmarked().Sum() * lastDraw).ToString();
    }

    private static (int, Board) PlayBingo(int[] draws, List<Board> boards)
    {
        foreach (var draw in draws)
        {
            foreach (var board in boards.ToList())
            {
                if (board.CheckDraw(draw))
                {
                    if (boards.Count == 1)
                    {
                        return (draw, boards.Single());
                    }

                    boards.Remove(board);
                }
            }
        }

        throw new PuzzleException("No winning board found.");
    }
}