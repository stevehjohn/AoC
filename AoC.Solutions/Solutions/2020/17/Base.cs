
using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2020._17;

public abstract class Base : Solution
{
    public override string Description => "Conway cubes";

    protected readonly HashSet<Point4D> ActiveCubes = [];

    protected void ParseInput()
    {
        ActiveCubes.Clear();

        for (var y = 0; y < Input.Length; y++)
        {
            for (var x = 0; x < Input[y].Length; x++)
            {
                if (Input[y][x] == '#')
                {
                    ActiveCubes.Add(new Point4D(x, y));
                }
            }
        }
    }

    protected void RunCycle(bool exploreW = false)
    {
        if (ActiveCubes.Count == 0)
        {
            return;
        }

        var minX = int.MaxValue;
        var minY = int.MaxValue;
        var minZ = int.MaxValue;
        var minW = int.MaxValue;

        var maxX = int.MinValue;
        var maxY = int.MinValue;
        var maxZ = int.MinValue;
        var maxW = int.MinValue;

        foreach (var cube in ActiveCubes)
        {
            minX = Math.Min(minX, cube.X);
            minY = Math.Min(minY, cube.Y);
            minZ = Math.Min(minZ, cube.Z);
            minW = Math.Min(minW, cube.W);

            maxX = Math.Max(maxX, cube.X);
            maxY = Math.Max(maxY, cube.Y);
            maxZ = Math.Max(maxZ, cube.Z);
            maxW = Math.Max(maxW, cube.W);
        }

        var sizeX = maxX - minX + 3;
        
        var sizeY = maxY - minY + 3;
        
        var sizeZ = maxZ - minZ + 3;
        
        var sizeW = exploreW ? maxW - minW + 3 : 1;

        var strideZ = sizeX * sizeY;
        
        var strideW = strideZ * sizeZ;

        var total = strideW * sizeW;

        var occupied = new bool[total];
        
        var counts = new byte[total];

        foreach (var cube in ActiveCubes)
        {
            var x = cube.X - minX + 1;
            
            var y = cube.Y - minY + 1;
            
            var z = cube.Z - minZ + 1;
            
            var w = exploreW ? cube.W - minW + 1 : 0;

            var index = x + y * sizeX + z * strideZ + w * strideW;

            occupied[index] = true;

            for (var dw = exploreW ? -1 : 0; dw <= (exploreW ? 1 : 0); dw++)
            {
                for (var dz = -1; dz <= 1; dz++)
                {
                    for (var dy = -1; dy <= 1; dy++)
                    {
                        for (var dx = -1; dx <= 1; dx++)
                        {
                            if (dx == 0 && dy == 0 && dz == 0 && dw == 0)
                            {
                                continue;
                            }

                            var neighbor = index + dx + dy * sizeX + dz * strideZ + dw * strideW;

                            counts[neighbor]++;
                        }
                    }
                }
            }
        }

        ActiveCubes.Clear();

        for (var w = 0; w < sizeW; w++)
        {
            for (var z = 0; z < sizeZ; z++)
            {
                for (var y = 0; y < sizeY; y++)
                {
                    for (var x = 0; x < sizeX; x++)
                    {
                        var index = x + y * sizeX + z * strideZ + w * strideW;

                        var neighbors = counts[index];

                        if (neighbors != 3 &&
                            (neighbors != 2 || !occupied[index]))
                        {
                            continue;
                        }

                        ActiveCubes.Add(new Point4D(x + minX - 1, y + minY - 1, z + minZ - 1, exploreW ? w + minW - 1 : 0));
                    }
                }
            }
        }
    }
}
