using AoC.Solutions.Infrastructure;
using Microsoft.Xna.Framework;
#if Windows
using System.Drawing;
using System.Drawing.Imaging;
using SharpAvi.Codecs;
using SharpAvi.Output;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;
#endif

namespace AoC.Visualisations.Infrastructure;

public abstract class VisualisationBase<T> : Game, IVisualiser<T>, IMultiPartVisualiser, IRecordableVisualiser
{
    protected bool HasNextState
    {
        get
        {
            if (_quitWhenQueueEmpty && _stateQueue.Count == 0 && VisualisationFinished)
            {
                Task.Delay(new TimeSpan(0, 0, 10)).ContinueWith(_ =>
                {
                    _cancellationTokenSource.Cancel();

#if Windows
                    EndVideo();
#endif

                    Exit();
                });
            }

            return _stateQueue.Count > 0;
        }
    }

    protected virtual bool VisualisationFinished => true;

    // ReSharper disable once NotAccessedField.Global
    protected GraphicsDeviceManager GraphicsDeviceManager;

    private readonly Queue<T> _stateQueue = new();

    protected Solution Puzzle { get; set; }

    private Task _puzzleTask;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public abstract void SetPart(int part);

    public string OutputAviPath { get; set; }

    protected bool IgnoreQueueLimit = false;
    
#if Windows
    private AviWriter _aviWriter;

    private IAviVideoStream _aviStream;
#endif
    
    private bool _quitWhenQueueEmpty;

    protected override void Initialize()
    {
        _puzzleTask = new Task(() => Puzzle.GetAnswer(), _cancellationTokenSource.Token);
        
        _puzzleTask.Start();
        
#if Windows
        if (! string.IsNullOrWhiteSpace(OutputAviPath))
        {
            _aviWriter = new AviWriter(OutputAviPath)
                         {
                             FramesPerSecond = 30,
                             EmitIndex1 = true
                         };

            _aviStream = _aviWriter.AddEncodingVideoStream(new UncompressedVideoEncoder(GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight),
                                                           true,
                                                           GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight);
        }
#endif

        base.Initialize();
    }

#if Windows
    protected override unsafe void EndDraw()
    {
        if (_aviStream != null)
        {
#pragma warning disable CA1416 // Validate platform compatibility
            using var bitmap = new Bitmap(GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight, PixelFormat.Format32bppRgb);

            var bounds = Window.ClientBounds;

            using (var graphics = Graphics.FromImage(bitmap))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, new Size(bounds.Size.X, bounds.Size.Y));
            }

            var bitmapData = bitmap.LockBits(new Rectangle(0, 0, bounds.Size.X, bounds.Size.Y), ImageLockMode.ReadOnly, PixelFormat.Format32bppRgb);

            var span = new Span<byte>(bitmapData.Scan0.ToPointer(), bitmapData.Stride * bitmapData.Height);

            _aviStream?.WriteFrame(true, span);

            bitmap.UnlockBits(bitmapData);
#pragma warning restore CA1416 // Validate platform compatibility
        }

        base.EndDraw();
    }
#endif

#if Windows
    private void EndVideo()
    {
        if (_aviWriter != null)
        {
            _aviStream = null;

            _aviWriter.Close();
        }
    }
#endif

    public void PuzzleComplete()
    {
        _quitWhenQueueEmpty = true;
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
        _cancellationTokenSource.Cancel();

#if Windows
        EndVideo();
#endif

        base.OnExiting(sender, args);
    }

    public void PuzzleStateChanged(T state)
    {
        if (_stateQueue.Count > 1000 && ! IgnoreQueueLimit)
        {
            Thread.Sleep(1000);
        }

        _stateQueue.Enqueue(state);
    }

    protected T GetNextState()
    {
        return _stateQueue.Dequeue();
    }

    protected T PeekNextState()
    {
        return _stateQueue.Peek();
    }
}