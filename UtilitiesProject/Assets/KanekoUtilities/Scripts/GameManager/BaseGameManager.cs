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

    protected LevelAdjuster levelAdjuster;
    protected UIManager uiManager;
    protected ContinueRequestType isContinueRequested;

    protected override void Start()
    {
        base.Start();
        levelAdjuster = LevelAdjuster.Instance;
        uiManager = UIManager.Instance;
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
        levelAdjuster.Init();
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

    protected abstract IEnumerator SuggestStart();
    protected abstract IEnumerator SuggestRestart();
    protected abstract IEnumerator SuggestContinue();

    protected abstract bool IsGameOver();
}
