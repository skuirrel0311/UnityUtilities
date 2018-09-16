using UnityEngine;
using KanekoUtilities;

public class MyScoreManager<T> : SingletonMonobehaviour<T> where T : MonoBehaviour
{
    [SerializeField]
    protected AbstractUGUIText totalScoreText = null;
    [SerializeField]
    protected AbstractUGUIText bestScoreText = null;

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

    public virtual void Init()
    {
        TotalScore = 0;
        IsBestScore = false;

        if (totalScoreText != null) totalScoreText.Text = TotalScore.ToString();
        if (bestScoreText != null) bestScoreText.Text = "best: " + BestScore;
    }

    public void AddScore(int value)
    {
        TotalScore += value;
        OnAddScore.Invoke(value);
    }

    /// <summary>
    /// ベストスコアを更新する（戻り値はベストスコアか？）
    /// </summary>
    public bool UpdateBestScore()
    {
        IsBestScore = TotalScore > BestScore;

        if (IsBestScore) BestScore = TotalScore;

        return IsBestScore;
    }
}
