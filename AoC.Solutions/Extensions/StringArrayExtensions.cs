namespace AoC.Solutions.Extensions;

public static class StringArrayExtensions
{
    extension(string[] array)
    {
        public char[,] To2DArray()
        {
            var twoDimensionalArray = new char[array[0].Length, array.Length];
        
            var y = 0;

            foreach (var line in array)
            {
                for (var x = 0; x < line.Length; x++)
                {
                    twoDimensionalArray[x, y] = line[x];
                }

                y++;
            }

            return twoDimensionalArray;
        }
    }
}