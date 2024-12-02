using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;
using AoC.Solutions.Solutions._2019.Computer;

namespace AoC.Solutions.Solutions._2019._11;

public abstract class Base : Solution
{
    public override string Description => "Personal plate (CPU used unmodified)";

    protected readonly List<Panel> Panels = [];

    private Cpu _cpu;

    protected void RunRobot(int startColour)
    {
        _cpu = new Cpu();

        _cpu.Initialise(65536);

        _cpu.LoadProgram(Input);

        Panels.Add(new Panel { Colour = startColour, Position = new Point(0, 0) });

        var position = new Point(0, 0);

        _cpu.UserInput.Enqueue(startColour);

        var direction = 0;

        while (_cpu.Run() != CpuState.Halted)
        {
            var panel = Panels.Single(p => p.Position.X == position.X && p.Position.Y == position.Y);

            var output = _cpu.UserOutput.Dequeue();

            panel.Colour = (int) output;

            direction += _cpu.UserOutput.Dequeue() == 1
                             ? 1
                             : -1;

            direction = direction switch
            {
                < 0 => 3,
                > 3 => 0,
                _ => direction
            };

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

            if (! Panels.Any(p => p.Position.X == position.X && p.Position.Y == position.Y))
            {
                Panels.Add(new Panel { Colour = 0, Position = new Point(position.X, position.Y) });
            }

            var newPanel = Panels.Single(p => p.Position.X == position.X && p.Position.Y == position.Y);

            _cpu.UserInput.Enqueue(newPanel.Colour);
        }
    }
}