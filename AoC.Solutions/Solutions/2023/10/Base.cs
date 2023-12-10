using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2023._10;

public abstract class Base : Solution
{
    public override string Description => "Pipe maze";
    
    protected char[][] Map;

    protected (int X, int Y) ParseInput()
    {
        Map = new char[Input.Length + 2][];

        var x = 0;

        var y = 0;

        Map[0] = new char[Input[0].Length + 2];
        
        Map[Input.Length + 1] = new char[Input[0].Length + 2];
        
        Array.Fill(Map[0], '.');
        
        Array.Fill(Map[Input.Length + 1], '.');
        
        for (var i = 0; i < Input.Length; i++)
        {
            Map[i + 1] = new char[Input[i].Length + 2];

            Map[i + 1][0] = '.';
            
            Map[i + 1][Input[i].Length + 1] = '.';
            
            for (var j = 0; j < Input[i].Length; j++)
            {
                Map[i + 1][j + 1] = Input[i][j];

                if (Input[i][j] == 'S')
                {
                    x = j + 1;

                    y = i + 1;
                }
            }
        }

        return (x, y);
    }
}