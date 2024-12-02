using AoC.Solutions.Infrastructure;

namespace AoC.Solutions.Solutions._2016._10;

public abstract class Base : Solution
{
    public override string Description => "Balance bots";

    private readonly Dictionary<int, List<int>> _bots = new();

    private readonly Dictionary<int, int> _outputs = new();

    protected void InitialiseBots()
    {
        var instructions = Input.Where(l => l.StartsWith("value"));

        foreach (var instruction in instructions)
        {
            var parts = instruction.Split(' ', StringSplitOptions.TrimEntries);

            var bot = int.Parse(parts[5]);

            GiveBotChip(bot, int.Parse(parts[1]));
        }
    }

    protected int RunBots(bool isPart2)
    {
        while (true)
        {
            var bots = _bots.Where(b => b.Value.Count == 2).ToList();

            if (bots.Count == 0)
            {
                break;
            }

            foreach (var bot in bots)
            {
                var instruction = Input.Single(l => l.StartsWith($"bot {bot.Key} "));

                var parts = instruction.Split(' ', StringSplitOptions.TrimEntries);

                var low = bot.Value.Min();

                var high = bot.Value.Max();

                if (low == 17 && high == 61 && ! isPart2)
                {
                    return bot.Key;
                }

                if (parts[5] == "bot")
                {
                    GiveBotChip(int.Parse(parts[6]), low);
                }
                else
                {
                    GiveOutputChip(int.Parse(parts[6]), low);
                }

                bot.Value.Remove(low);

                if (parts[10] == "bot")
                {
                    GiveBotChip(int.Parse(parts[11]), high);
                }
                else
                {
                    GiveOutputChip(int.Parse(parts[11]), high);
                }

                bot.Value.Remove(high);
            }
        }

        return _outputs[0] * _outputs[1] * _outputs[2];
    }

    private void GiveBotChip(int bot, int chip)
    {
        if (! _bots.TryGetValue(bot, out var value))
        {
            value = [];
            
            _bots.Add(bot, value);
        }

        value.Add(chip);
    }

    private void GiveOutputChip(int output, int chip)
    {
        _outputs.Add(output, chip);
    }
}