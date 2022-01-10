using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._22;

[UsedImplicitly]
public class Part1 : Base
{
    private const int NumberOfCards = 10;

    private int[] _deck1;
    
    private int[] _deck2;

    private int _currentDeck;

    public override string GetAnswer()
    {
        _deck1 = new int[NumberOfCards];
        
        _deck2 = new int[NumberOfCards];

        foreach (var line in Input)
        {
            if (line.StartsWith("deal into"))
            {
                DealIntoNewStack();

                continue;
            }

            if (line.StartsWith("cut"))
            {
                Cut(int.Parse(line.Substring(4)));

                continue;
            }

            DealWithIncrement(int.Parse(line.Substring(20)));
        }

        return "TESTING";
    }

    private void DealIntoNewStack()
    {
    }

    private void Cut(int position)
    {
    }

    private void DealWithIncrement(int increment)
    {
    }

    private (int[] Source, int[] Target) GetDecks()
    {
        var result = (Source: _currentDeck == 1 ? _deck1 : _deck2, Target: _currentDeck == 1 ? _deck2 : _deck1);

        _currentDeck = 3 - _currentDeck;

        return result;
    }
}