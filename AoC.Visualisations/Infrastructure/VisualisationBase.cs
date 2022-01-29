using AoC.Solutions.Infrastructure;
using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Infrastructure;

public abstract class VisualisationBase<T> : Game, IVisualiser<T>, IMultiPartVisualiser
{
    protected bool HasNextState => _stateQueue.Count > 0;

    private readonly Queue<T> _stateQueue = new();

    public abstract void SetPart(int part);

    public void PuzzleStateChanged(T state)
    {
        if (_stateQueue.Count > 1000)
        {
            Thread.Sleep(1000);
        }

        _stateQueue.Enqueue(state);
    }

    protected T GetNextState()
    {
        return _stateQueue.Dequeue();
    }
}