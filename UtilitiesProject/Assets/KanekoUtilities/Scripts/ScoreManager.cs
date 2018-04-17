using System;
using UnityEngine;
using KanekoUtilities;

public class ScoreManager : Singleton<ScoreManager>
{
    const string bestScoreSaveKey = "bestScore";
    public int TotalScore { get; private set; }
    public int BestScore
    {
        get
        {
            return PlayerPrefs.GetInt(bestScoreSaveKey, 0);
        }
        set
        {
            PlayerPrefs.SetInt(bestScoreSaveKey, value);
        }
    }

    ScoreText text;

    public Action<int> OnAddScore;

    public void Init()
    {
        TotalScore = 0;
    }

    public void SetText(ScoreText text)
    {
        this.text = text;
    }

    /// <summary>
    /// 現在のゲームの状況を考慮してスコアを足す
    /// </summary>
    public void AddScore()
    {
        int value = 100;

        AddScore(value);
    }

    /// <summary>
    /// 現在のゲームの状況を考慮せずスコアを足す
    /// </summary>
    public void AddScore(int value)
    {
        TotalScore += value;

        if (OnAddScore != null) OnAddScore.Invoke(value);
        if(text != null) text.Value = TotalScore;
    }

    public bool UpdateBestScore()
    {
        if (TotalScore <= BestScore) return false;

        BestScore = TotalScore;

        return true;
    }
}
