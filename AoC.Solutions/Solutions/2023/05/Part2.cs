using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2023._05;

[UsedImplicitly]
public class Part2 : Base
{
    private List<List<(Range Range, long Adjustment)>> _mappings;

    private List<Range> _seeds;
    
    public override string GetAnswer()
    {
        _mappings = new List<List<(Range Range, long Adjustment)>>();

        _seeds = new List<Range>();
        
        ParseInput();

        var seeds = new List<Range>(_seeds);

        foreach (var mapping in _mappings)
        {
            var newSeeds = new List<Range>();

            for (var i = 0; i < mapping.Count; i++)
            {
                var map = mapping[i];
                
                var mapSeeds = new List<Range>();
                
                foreach (var seed in seeds)
                {
                    var overlap = map.Range.Intersects(seed);
                    
                    if (overlap == null)
                    {
                        mapSeeds.Add(seed);
                    
                        continue;
                    }
                    
                    if (map.Range.Contains(seed))
                    {
                        newSeeds.Add(new Range(seed.Start + map.Adjustment, seed.End + map.Adjustment));
                        
                        continue;
                    }

                    if (seed.Contains(map.Range))
                    {
                        mapSeeds.Add(new Range(seed.Start, map.Range.Start - 1));

                        mapSeeds.Add(new Range(seed.End + 1, map.Range.End));

                        newSeeds.Add(new Range(map.Range.Start + map.Adjustment, map.Range.End + map.Adjustment));

                        continue;
                    }

                    newSeeds.Add(new Range(overlap.Start + map.Adjustment, overlap.End + map.Adjustment));
                    
                    if (seed.Start < map.Range.Start)
                    {
                        mapSeeds.Add(new Range(seed.Start, map.Range.Start - 1));
                    }
                    else if (seed.End > map.Range.End)
                    {
                        mapSeeds.Add(new Range(map.Range.End + 1, seed.End));
                    }
                }

                if (i == mapping.Count - 1)
                {
                    newSeeds.AddRange(mapSeeds);
                }
                else
                {
                    seeds = mapSeeds;
                }
            }

            seeds = newSeeds;
        }

        return seeds.Min(s => s.Start).ToString();
    }

    private void ParseInput()
    {
        var seeds = Input[0][6..].Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

        for (var i = 0; i < seeds.Length; i += 2)
        {
            _seeds.Add(new Range(seeds[i], seeds[i] + seeds[i + 1] - 1));
        }

        var mapping = new List<(Range Range, long Adjustment)>();

        foreach (var line in Input[3..])
        {
            if (string.IsNullOrWhiteSpace(line))
            {
                continue;
            }

            if (!char.IsNumber(line[0]))
            {
                _mappings.Add(mapping);

                mapping = new List<(Range Range, long Adjustment)>();

                continue;
            }

            var parts = line.Split(' ', StringSplitOptions.TrimEntries | StringSplitOptions.RemoveEmptyEntries).Select(long.Parse).ToArray();

            mapping.Add((new Range(parts[1], parts[1] + parts[2] - 1), parts[0] - parts[1]));
        }

        _mappings.Add(mapping);
    }
}