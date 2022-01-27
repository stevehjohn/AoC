namespace AoC.Solutions.Solutions._2018._12;

public class Part1 : Base
{
    private List<int> _potsWithPlants = new();

    public override string GetAnswer()
    {
        ParseInput();

        return "TESTING";
    }

    private void ParseInput()
    {
        var initialState = Input[0][15..];

        for (var i = 0; i < initialState.Length; i++)
        {
            if (initialState[i] == '#')
            {
                _potsWithPlants.Add(i);
            }
        }

        foreach (var line in Input.Skip(2))
        {
            
        }
    }
}