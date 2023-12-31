using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2019._12;

public class Moon
{
    public Point Position { get; }

    public Point Velocity { get; }

    public int Energy => KineticEnergy * PotentialEnergy;

    private int KineticEnergy => Math.Abs(Velocity.X) + Math.Abs(Velocity.Y) + Math.Abs(Velocity.Z);

    private int PotentialEnergy => Math.Abs(Position.X) + Math.Abs(Position.Y) + Math.Abs(Position.Z);

    public Moon()
    {
        Position = new Point();

        Velocity = new Point();
    }
}