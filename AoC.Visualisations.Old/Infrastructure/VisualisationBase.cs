﻿using AoC.Solutions.Infrastructure;
using Microsoft.Xna.Framework;
using SharpAvi.Codecs;
using SharpAvi.Output;
using System.Drawing.Imaging;
using Point = System.Drawing.Point;
using Rectangle = System.Drawing.Rectangle;

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

                    EndVideo();

                    Exit();
                });
            }

            return _stateQueue.Count > 0;
        }
    }

    protected virtual bool VisualisationFinished => true;

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

            _aviStream = _aviWriter.AddEncodingVideoStream(new UncompressedVideoEncoder(GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight),
                                                           true,
                                                           GraphicsDeviceManager.PreferredBackBufferWidth, GraphicsDeviceManager.PreferredBackBufferHeight);
        }

        base.Initialize();
    }

    protected override unsafe void EndDraw()
    {
        if (_aviStream != null)
        {
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
        }

        base.EndDraw();
    }

    private void EndVideo()
    {
        if (_aviWriter != null)
        {
            _aviStream = null;

            _aviWriter.Close();
        }
    }

    public void PuzzleComplete()
    {
        _quitWhenQueueEmpty = true;
    }

    protected override void OnExiting(object sender, EventArgs args)
    {
        _cancellationTokenSource.Cancel();

        EndVideo();

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