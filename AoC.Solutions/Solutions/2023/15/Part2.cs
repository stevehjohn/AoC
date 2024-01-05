using System.Text;
using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._15;

[UsedImplicitly]
public class Part2 : Base
{
    public override string GetAnswer()
    {
        var steps = ParseInput();

        var boxes = new List<(string Label, int Lense)>[256];

        for (var i = 0; i < 256; i++)
        {
            boxes[i] = [];
        }

        foreach (var step in steps)
        {
            var task = ParseStep(step);

            PerformStep(boxes, task);
        }

        var power = 0;
        
        for (var i = 0; i < 256; i++)
        {
            if (boxes[i].Count > 0)
            {
                power += GetBoxPower(i + 1, boxes[i]);
            }
        }
        
        return power.ToString();
    }

    private static int GetBoxPower(int boxNumber, List<(string Label, int Lense)> box)
    {
        var power = 0;
        
        for (var i = 0; i < box.Count; i++)
        {
            power += boxNumber * (i + 1) * box[i].Lense;
        }

        return power;
    }

    private static void PerformStep(List<(string Label, int Lense)>[] boxes, (string Label, int FocalLength) task)
    {
        var boxNumber = Hash(task.Label);

        var box = boxes[boxNumber];
            
        if (task.FocalLength == 0)
        {
            for (var i = 0; i < box.Count; i++)
            {
                if (box[i].Label == task.Label)
                {
                    box.RemoveAt(i);
                        
                    break;
                }
            }

            return;
        }

        var lensIndex = -1;
            
        for (var i = 0; i < box.Count; i++)
        {
            if (box[i].Label == task.Label)
            {
                lensIndex = i;
                    
                break;
            }
        }

        if (lensIndex == -1)
        {
            box.Add(task);
                
            return;
        }

        box[lensIndex] = task;
    }

    private static (string Label, int FocalLength) ParseStep(string step)
    {
        var label = new StringBuilder();

        int i;
        
        var c = '\0';
        
        for (i = 0; i < step.Length; i++)
        {
            c = step[i];
            
            if (! char.IsLetter(c))
            {
                break;
            }

            label.Append(c);
        }

        if (c == '-')
        {
            return (label.ToString(), 0);
        }

        return (label.ToString(), step[i + 1] - '0');
    }
}