using System;
using UnityEngine;

namespace KanekoUtilities
{
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

        public MyUnityEvent<int> OnAddScore;

        public void Init()
        {
            TotalScore = 0;
        }

        public void AddScore(int value)
        {
            TotalScore += value;

            if (OnAddScore != null) OnAddScore.Invoke(value);
        }

        public bool UpdateBestScore()
        {
            if (TotalScore <= BestScore) return false;

            BestScore = TotalScore;

            return true;
        }
    }
}