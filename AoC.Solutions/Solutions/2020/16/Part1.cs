using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2020._16;

[UsedImplicitly]
public class Part1 : Base
{
    public override string GetAnswer()
    {
        ParseInput();

        var invalid = 0;

        foreach (var ticket in OtherTickets)
        {
            foreach (var field in ticket)
            {
                var valid = false;

                foreach (var rule in Rules)
                {
                    if (field >= rule.Rule1.Minimum && field <= rule.Rule1.Maximum || field >= rule.Rule2.Minimum && field <= rule.Rule2.Maximum)
                    {
                        valid = true;

                        break;
                    }
                }

                if (! valid)
                {
                    invalid += field;
                }
            }
        }

        return invalid.ToString();
    }
}