using UnityEngine;
using KanekoUtilities;

public class MyScoreManager<T> : SingletonMonobehaviour<T> where T : MonoBehaviour
{
    [SerializeField]
    AbstractUGUIText totalScoreText = null;
    [SerializeField]
    AbstractUGUIText bestScoreText = null;

    public int BestScore
    {
        get
        {
            return bestScore.GetValue();
        }
        set
        {
            bestScore.SetValue(value);

            if (bestScoreText == null) return;
            bestScoreText.Text = "best: " + value;
        }
    }
    public int TotalScore
    {
        get
        {
            return totalScore;
        }
        set
        {
            totalScore = value;

            if (totalScoreText == null) return;
            totalScoreText.Text = TotalScore.ToString();
        }
    }
    /// <summary>
    /// Initが呼ばれるまでは前回の結果を保持している
    /// </summary>
    public bool IsBestScore { get; private set; }

    public MyUnityEvent<int> OnAddScore = new MyUnityEvent<int>();

    RegisterIntParameter bestScore;
    int totalScore;

    protected override void Awake()
    {
        base.Awake();

        bestScore = new RegisterIntParameter("BestScore", 0);
    }

    public void Init()
    {
        TotalScore = 0;
        IsBestScore = false;

        if(totalScoreText != null) totalScoreText.Text = TotalScore.ToString();
        if(bestScoreText != null) bestScoreText.Text = "best: " + BestScore;
    }

    /// <summary>
    /// ゲームの状況を見てスコアを増加させる
    /// </summary>
    public void UpdateScore()
    {
        int addValue = 1;

        //todo : ComboなどによってaddValueの値を変化させる

        TotalScore += addValue;

        OnAddScore.Invoke(addValue);
    }

    /// <summary>
    /// ベストスコアを更新する（戻り値はベストスコアか？）
    /// </summary>
    public bool UpdateBestScore()
    {
        if (TotalScore <= BestScore)
        {
            IsBestScore = false;
            return false;
        }

        BestScore = TotalScore;
        IsBestScore = true;
        return true;
    }
}
