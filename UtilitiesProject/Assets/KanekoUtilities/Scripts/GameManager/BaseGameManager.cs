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

public enum GameMode
{
    Infinite,
    Stage
}

public enum ContinueRequestType { Continue, NoThanks, TimeOut }

public abstract class BaseGameManager<T> : SingletonMonobehaviour<T> where T : MonoBehaviour
{
    [SerializeField]
    protected TitlePanel titlePanel = null;
    [SerializeField]
    protected InGamePanel inGamePanel = null;
    [SerializeField]
    protected GameOverPanel gameOverPanel = null;

    protected abstract GameMode Mode { get; }

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
    
    protected ContinueRequestType isContinueRequested;
    
    protected override void Start()
    {
        base.Start();
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
        titlePanel.Activate();
        inGamePanel.Deactivate();
        gameOverPanel.Deactivate();
    }
    protected virtual void GameStart()
    {
        CurrentState = GameState.InGame;
        titlePanel.Deactivate();
        inGamePanel.Activate();
    }
    protected virtual void GameOver()
    {
        CurrentState = GameState.GameOver;
        gameOverPanel.Activate();
    }
    protected virtual void Continue()
    {
        ContinueCount++;
        gameOverPanel.Deactivate();
    }
    protected virtual void StageClear()
    {
        CurrentState = GameState.StageClear;
    }

    protected abstract IEnumerator SuggestStart();
    protected abstract IEnumerator SuggestRestart();
    protected abstract IEnumerator SuggestContinue();

    protected abstract bool IsGameOver();
}
