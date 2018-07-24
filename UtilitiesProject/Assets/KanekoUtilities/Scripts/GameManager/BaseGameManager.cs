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
    GameStatePanel[] statePanels = null;

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

            if (OnStateChanged != null) OnStateChanged.Invoke(value);
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

    public event Action OnInitialize;
    public event Action OnGameStart;
    public event Action OnContinue;
    public event Action OnGameOver;
    public event Action OnStageClear;
    public event Action<GameState> OnStateChanged;
    
    protected ContinueRequestType isContinueRequested;

    protected override void Awake()
    {
        for(int i = 0;i <statePanels.Length;i++)
        {
            statePanels[i].SetGameMode(Mode);
        }
    }

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
        if (OnInitialize != null) OnInitialize.Invoke();
    }
    protected virtual void GameStart()
    {
        CurrentState = GameState.InGame;

        if (OnGameStart != null) OnGameStart();
    }
    protected virtual void GameOver()
    {
        CurrentState = GameState.GameOver;

        if (OnGameOver != null) OnGameOver();
    }
    protected virtual void Continue()
    {
        ContinueCount++;

        if (OnContinue != null) OnContinue();
    }
    protected virtual void StageClear()
    {
        CurrentState = GameState.StageClear;
        if (OnStageClear != null) OnStageClear.Invoke();
    }

    protected abstract IEnumerator SuggestStart();
    protected abstract IEnumerator SuggestRestart();
    protected abstract IEnumerator SuggestContinue();

    protected abstract bool IsGameOver();
}
