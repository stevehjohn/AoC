using AoC.Solutions.Infrastructure;
using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Infrastructure;

public abstract class VisualisationBase<T> : Game, IVisualiser<T>, IMultiPartVisualiser
{
    protected readonly Queue<T> StateQueue = new();

    public abstract void SetPart(int part);

    public void PuzzleStateChanged(T state)
    {
        if (StateQueue.Count > 1000)
        {
            Thread.Sleep(1000);
        }

        StateQueue.Enqueue(state);
    }
}