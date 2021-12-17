using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._08;

[UsedImplicitly]
public class Part1 : Base
{ 
    public override string GetAnswer()
    {
        var data = Input[0];

        var fewestZeroLayer = new string('0', 150);

        var index = 0;

        while (index < data.Length)
        {
            var layer = data.Skip(index).Take(150).ToArray();

            if (layer.Count(c => c == '0') < fewestZeroLayer.Count(c => c == '0'))
            {
                fewestZeroLayer = new string(layer);
            }

            index += 150;
        }

        return (fewestZeroLayer.Count(c => c == '1') * fewestZeroLayer.Count(c => c == '2')).ToString();
    }
}