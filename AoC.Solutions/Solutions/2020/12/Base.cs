using AoC.Solutions.Common;
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._12;

public abstract class Base : Solution
{
    public override string Description => "Evergreen ship sailing";

    protected int Navigate(bool useWaypoint)
    {
        var direction = 90;

        var shipPosition = new Point();

        var waypointPosition = new Point(10, -1);

        foreach (var line in Input)
        {
            var value = int.Parse(line[1..]);

            switch (line[0])
            {
                case 'N':
                    shipPosition.Y -= useWaypoint ? 0 : value;
                    waypointPosition.Y -= useWaypoint ? value : 0;

                    break;

                case 'S':
                    shipPosition.Y += useWaypoint ? 0 : value;
                    waypointPosition.Y += useWaypoint ? value : 0;

                    break;

                case 'E':
                    shipPosition.X += useWaypoint ? 0 : value;
                    waypointPosition.X += useWaypoint ? value : 0;

                    break;

                case 'W':
                    shipPosition.X -= useWaypoint ? 0 : value;
                    waypointPosition.X -= useWaypoint ? value : 0;

                    break;

                case 'L':
                    if (useWaypoint)
                    {
                        while (value > 0)
                        {
                            var dX = waypointPosition.X - shipPosition.X;
                            var dY = waypointPosition.Y - shipPosition.Y;

                            waypointPosition.X = dY + shipPosition.X;
                            waypointPosition.Y = -dX + shipPosition.Y;

                            value -= 90;
                        }
                    }
                    else
                    {
                        direction -= value;

                        if (direction < 0)
                        {
                            direction += 360;
                        }
                    }

                    break;

                case 'R':
                    if (useWaypoint)
                    {
                        while (value > 0)
                        {
                            var dX = waypointPosition.X - shipPosition.X;
                            var dY = waypointPosition.Y - shipPosition.Y;

                            waypointPosition.X = -dY + shipPosition.X;
                            waypointPosition.Y = dX + shipPosition.Y;

                            value -= 90;
                        }
                    }
                    else
                    {
                        direction += value;

                        if (direction >= 360)
                        {
                            direction -= 360;
                        }
                    }

                    break;

                default:
                    if (useWaypoint)
                    {
                        var dX = waypointPosition.X - shipPosition.X;
                        var dY = waypointPosition.Y - shipPosition.Y;

                        shipPosition.X += dX * value;
                        shipPosition.Y += dY * value;

                        waypointPosition.X += dX * value;
                        waypointPosition.Y += dY * value;
                    }
                    else
                    {
                        switch (direction)
                        {
                            case 90:
                                shipPosition.X += value;
                                break;

                            case 180:
                                shipPosition.Y += value;
                                break;

                            case 270:
                                shipPosition.X -= value;
                                break;

                            default:
                                shipPosition.Y -= value;
                                break;
                        }
                    }

                    break;
            }
        }

        return Math.Abs(shipPosition.X) + Math.Abs(shipPosition.Y);
    }
}