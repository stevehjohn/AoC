namespace AoC.Games.Games.Deflectors;

public static class ScoreKeeper
{
    private const string HighScoreFile = "high-score.txt";

    private static int _highScore = -1;

    public static int GetHighScore()
    {
        if (_highScore == -1)
        {
            var text = File.ReadAllText(HighScoreFile);

            _highScore = int.Parse(text);
        }

        return _highScore;
    }

    public static bool CheckHighScore(int score)
    {
        if (score > _highScore)
        {
            _highScore = score;
            
            File.WriteAllText(HighScoreFile, _highScore.ToString());

            return true;
        }

        return false;
    }
}