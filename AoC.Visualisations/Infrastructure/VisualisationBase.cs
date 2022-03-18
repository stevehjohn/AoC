using AoC.Solutions.Infrastructure;
using Microsoft.Xna.Framework;
using SharpAvi.Output;

namespace AoC.Visualisations.Infrastructure;

public abstract class VisualisationBase<T> : Game, IVisualiser<T>, IMultiPartVisualiser, IRecordableVisualiser
{
    protected bool HasNextState => _stateQueue.Count > 0;
    
    protected GraphicsDeviceManager GraphicsDeviceManager;

    private readonly Queue<T> _stateQueue = new();

    protected Solution Puzzle { get; set; }

    private Task _puzzleTask;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public abstract void SetPart(int part);

    public string OutputAviPath { get; set; }

    private AviWriter _aviWriter;

    private IAviVideoStream _aviStream;

    protected override void Initialize()
    {
        _puzzleTask = new Task(() => Puzzle.GetAnswer(), _cancellationTokenSource.Token);

        _puzzleTask.Start();

        if (! string.IsNullOrWhiteSpace(OutputAviPath))
        {
            _aviWriter = new AviWriter(OutputAviPath)
                         {
                             FramesPerSecond = 30,
                             EmitIndex1 = true
                         };

            _aviStream = _aviWriter.AddVideoStream();

            _aviStream.Width = GraphicsDeviceManager.PreferredBackBufferWidth;

            _aviStream.Height = GraphicsDeviceManager.PreferredBackBufferHeight;
        }

        base.Initialize();
    }

    protected override void EndDraw()
    {
        if (_aviStream != null)
        {
        }

        base.EndDraw();
    }

    public void PuzzleComplete()
    {
        // TODO: Cause visualisation to end in 10, 20 seconds or so.
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