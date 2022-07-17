using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : Singleton<ScoreController>
{
    private int _totalScore;
    public void CountScore(int score)
    {
        _totalScore += score;
    }

    public int GetScore()
    {
        return _totalScore;
    }

    public void ResetScore()
    {
        _totalScore = 0;
    }
}
