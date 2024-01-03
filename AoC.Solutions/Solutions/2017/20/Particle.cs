using AoC.Solutions.Common;

namespace AoC.Solutions.Solutions._2017._20;

public class Particle
{
    public int Id { get; }

    public Point Position { get; }

    public Point Velocity { get; }

    public Point Acceleration { get; }

    public Particle(int id, Point position, Point velocity, Point acceleration)
    {
        Id = id;

        Position = position;

        Velocity = velocity;

        Acceleration = acceleration;
    }

    public static Particle Parse(int id, string data)
    {
        var parts = data[3..^1].Split(">, ");

        return new Particle(id, Point.Parse(parts[0]), Point.Parse(parts[1][3..]), Point.Parse(parts[2][3..]));
    }

    public void Move()
    {
        Velocity.X += Acceleration.X;

        Velocity.Y += Acceleration.Y;

        Velocity.Z += Acceleration.Z;

        Position.X += Velocity.X;

        Position.Y += Velocity.Y;

        Position.Z += Velocity.Z;
    }
}