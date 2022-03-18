namespace AoC.Solutions.Infrastructure;

public interface IVisualiser<in T>
{
    void PuzzleStateChanged(T state);

    void PuzzleComplete();
}