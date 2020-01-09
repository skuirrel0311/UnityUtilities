using System;
using System.Collections;
using UnityEngine;
using KanekoUtilities;

public enum GameState
{
    Ready,
    InGame,
    GameOver,
    StageClear
}

public enum ContinueRequestType { Continue, NoThanks, TimeOut }

public abstract class BaseGameManager<T> : SingletonMonobehaviour<T> where T : MonoBehaviour
{
    GameState currentState = GameState.Ready;
    public GameState CurrentState
    {
        get
        {
            return currentState;
        }
        set
        {
            currentState = value;
        }
    }

    public abstract int MaxContinueCount { get; }

    public int ContinueCount { get; protected set; }
    public bool CanContinue
    {
        get
        {
            if (MaxContinueCount < 0) return true;
            return ContinueCount < MaxContinueCount;
        }
    }

    protected RegisterIntParameter currentLevel;
    public int CurrentLevel { get { return currentLevel.GetValue(); } }

    protected UIManager uiManager;
    protected ContinueRequestType isContinueRequested;

    protected override void Start()
    {
        base.Start();
        uiManager = UIManager.Instance;
        currentLevel = new RegisterIntParameter("CurrentLevel", 1);

        StartCoroutine(GameLoop());
    }

    IEnumerator GameLoop()
    {
        while (true)
        {
            yield return StartCoroutine(OneGame());
        }
    }

    protected abstract IEnumerator OneGame();

    protected virtual void Init()
    {
        CurrentState = GameState.Ready;
        ContinueCount = 0;
        isContinueRequested = 0;

        uiManager.OnInitialized();
    }
    protected virtual void GameStart()
    {
        CurrentState = GameState.InGame;
        uiManager.OnGameStart();

    }
    protected virtual void GameOver()
    {
        CurrentState = GameState.GameOver;
        uiManager.OnGameOver();
    }
    protected virtual void Continue()
    {
        ContinueCount++;
        uiManager.OnContinue();
    }
    protected virtual void StageClear()
    {
        CurrentState = GameState.StageClear;
        uiManager.OnStageClear();
    }

    protected virtual IEnumerator SuggestStart()
    {
        yield return uiManager.TitlePanel.SuggestGameStart();
    }
    protected virtual IEnumerator SuggestRestart()
    {
        //このコルーチンが終了するとゲームがリセットされる
        if (CurrentState == GameState.GameOver)
        {
            yield return uiManager.GameOverPanel.SuggestRestart();
        }
        else if (CurrentState == GameState.StageClear)
        {
            yield return uiManager.StageClearPanel.SuggestRestart();
        }
        else
        {
            yield return null;
        }
    }
    protected virtual IEnumerator SuggestContinue()
    {
        yield return uiManager.GameOverPanel.SuggestContinue((isRequested) =>
        {
            isContinueRequested = isRequested;
        });

        if (isContinueRequested != ContinueRequestType.Continue)
        {
            MyAdManager.Instance.ShowGameOverInterstitial();
        }
    }

    protected abstract bool IsGameOver();
}
