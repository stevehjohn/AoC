using AoC.Solutions.Infrastructure;
using Microsoft.Xna.Framework;

namespace AoC.Visualisations.Infrastructure;

public abstract class VisualisationBase<T> : Game, IVisualiser<T>, IMultiPartVisualiser
{
    protected bool HasNextState => _stateQueue.Count > 0;

    private readonly Queue<T> _stateQueue = new();

    protected Solution Puzzle { get; set; }

    private Task _puzzleTask;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public abstract void SetPart(int part);

    protected override void Initialize()
    {
        _puzzleTask = new Task(() => Puzzle.GetAnswer(), _cancellationTokenSource.Token);

        _puzzleTask.Start();

        base.Initialize();
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
        _cancellationTokenSource.Cancel();

        base.OnExiting(sender, args);
    }

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