using System;

[Serializable]
public class Score
{
    public string username;
    public int score;
    public float ellapsedTime;

    public Score(string username, int score, float ellapsedTime)
    {
        this.username = username;
        this.score = score;
        this.ellapsedTime = ellapsedTime;
    }
}