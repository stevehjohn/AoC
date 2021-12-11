using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._11;

[UsedImplicitly]
public class Part1 : Solution
{
    private Cpu _cpu;

    private readonly List<Panel> _panels = new();

    public override string GetAnswer()
    {
        _cpu = new Cpu();

        _cpu.Initialise(65536);

        _cpu.LoadProgram(Input);

        _panels.Add(new Panel { Colour = 0, Position = new Point(0, 0) });

        var position = new Point(0, 0);

        _cpu.UserInput.Enqueue(0);

        var direction = 0;

        while (_cpu.Run() != CpuState.Halted)
        {
            var panel = _panels.Single(p => p.Position.X == position.X && p.Position.Y == position.Y);

            var output = _cpu.UserOutput.Dequeue();

            if (panel.Colour != output)
            {
                panel.Colour = (int) output;
            }

            direction += _cpu.UserOutput.Dequeue() == 1
                             ? 1
                             : -1;

            if (direction < 0)
            {
                direction = 3;
            }
            else if (direction > 3)
            {
                direction = 0;
            }

            switch (direction)
            {
                case 0:
                    position.Y++;
                    break;
                case 1:
                    position.X++;
                    break;
                case 2:
                    position.Y--;
                    break;
                case 3:
                    position.X--;
                    break;
            }

            if (! _panels.Any(p => p.Position.X == position.X && p.Position.Y == position.Y))
            {
                _panels.Add(new Panel { Colour = 0, Position = new Point(position.X, position.Y ) });
            }

            var newPanel = _panels.Single(p => p.Position.X == position.X && p.Position.Y == position.Y);

            _cpu.UserInput.Enqueue(newPanel.Colour);
        }

        return _panels.Count.ToString();
    }
}