using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MyScoreManager : SingletonMonobehaviour<MyScoreManager>
{
    [SerializeField]
    NumberText totalScore = null;
    [SerializeField]
    NumberText bestScore = null;
    
    ScoreManager scoreManager;
    ScoreManager ScoreManager
    {
        get
        {
            if (scoreManager != null)
            {
                scoreManager = ScoreManager.Instance;
            }

            return scoreManager;
        }
    }

    public void Init()
    {
        totalScore.SetValue(0);
        bestScore.SetValue(ScoreManager.BestScore);
    }

    /// <summary>
    /// ゲームの状況を見てスコアを増加させる
    /// </summary>
    public void UpdateScore()
    {
        ScoreManager.AddScore(1);

        totalScore.SetValue(ScoreManager.TotalScore);
    }
}
