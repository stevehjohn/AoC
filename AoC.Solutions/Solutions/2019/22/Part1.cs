using JetBrains.Annotations;

namespace AoC.Solutions.Solutions._2019._22;

[UsedImplicitly]
public class Part1 : Base
{
    private const int NumberOfCards = 10007;

    private int[] _deck1;
    
    private int[] _deck2;

    private int _currentDeck;

    public override string GetAnswer()
    {
        _deck1 = new int[NumberOfCards];

        for (var i = 0; i < NumberOfCards; i++)
        {
            _deck1[i] = i;
        }

        _deck2 = new int[NumberOfCards];

        _currentDeck = 1;

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

        var decks = GetDecks();

        return Array.IndexOf(decks.Source, 2019).ToString();
    }

    private void DealIntoNewStack()
    {
        var decks = GetDecks();

        for (var i = 0; i < NumberOfCards; i++)
        {
            decks.Target[NumberOfCards - 1 - i] = decks.Source[i];
        }
    }

    private void Cut(int length)
    {
        var decks = GetDecks();

        int i;

        if (length < 0)
        {
            length = -length;

            for (i = 0; i < NumberOfCards - length; i++)
            {
                decks.Target[i + length] = decks.Source[i];
            }

            for (i = 0; i < length; i++)
            {
                decks.Target[i] = decks.Source[NumberOfCards - length + i];
            }
        }
        else
        {
            for (i = 0; i < NumberOfCards - length; i++)
            {
                decks.Target[i] = decks.Source[i + length];
            }

            for (i = 0; i < length; i++)
            {
                decks.Target[NumberOfCards - length + i] = decks.Source[i];
            }
        }
    }

    private void DealWithIncrement(int increment)
    {
        var decks = GetDecks();

        var position = 0;

        for (var i = 0; i < NumberOfCards; i++)
        {
            decks.Target[position] = decks.Source[i];

            position += increment;

            if (position >= NumberOfCards)
            {
                position -= NumberOfCards;
            }
        }
    }

    private (int[] Source, int[] Target) GetDecks()
    {
        var result = (Source: _currentDeck == 1 ? _deck1 : _deck2, Target: _currentDeck == 1 ? _deck2 : _deck1);

        _currentDeck = 3 - _currentDeck;

        return result;
    }
}