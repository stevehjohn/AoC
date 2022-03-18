using System.Drawing.Imaging;
using AoC.Solutions.Infrastructure;
using Microsoft.Xna.Framework;
using SharpAvi;
using SharpAvi.Output;
using Point = System.Drawing.Point;

namespace AoC.Visualisations.Infrastructure;

public abstract class VisualisationBase<T> : Game, IVisualiser<T>, IMultiPartVisualiser, IRecordableVisualiser
{
    protected bool HasNextState
    {
        get
        {
            if (_quitWhenQueueEmpty && _stateQueue.Count == 0)
            {
                Task.Delay(new TimeSpan(0, 0, 10)).ContinueWith(_ => EndVideo());
            }

            return _stateQueue.Count > 0;
        }
    }

    protected GraphicsDeviceManager GraphicsDeviceManager;

    private readonly Queue<T> _stateQueue = new();

    protected Solution Puzzle { get; set; }

    private Task _puzzleTask;

    private readonly CancellationTokenSource _cancellationTokenSource = new();

    public abstract void SetPart(int part);

    public string OutputAviPath { get; set; }

    private AviWriter _aviWriter;

    private IAviVideoStream _aviStream;

    private bool _quitWhenQueueEmpty;

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

            _aviStream.BitsPerPixel = BitsPerPixel.Bpp24;
        }

        base.Initialize();
    }

    protected override void EndDraw()
    {
        if (_aviStream != null)
        {
            using (var bitmap = new Bitmap(GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight, PixelFormat.Format24bppRgb))
            {
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    var bounds = Window.ClientBounds;

                    graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, new Size(bounds.Size.X, bounds.Size.Y));
                }

                using (var stream = new MemoryStream())
                {
                    bitmap.Save(stream, ImageFormat.Bmp);

                    _aviStream.WriteFrame(true, stream.ToArray());
                }
            }
        }

        base.EndDraw();
    }

    private void EndVideo()
    {
        if (_aviWriter != null)
        {
            _aviStream = null;

            _aviWriter.Close();
            
            Application.Exit();
        }
    }

    public void PuzzleComplete()
    {
        _quitWhenQueueEmpty = true;
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
        EndVideo();

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