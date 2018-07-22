using System;
using System.Collections;
using UnityEngine;
using KanekoUtilities;

public enum GameState
{
    Ready,
    InGame,
    Result
}

public partial class MainSceneManager : SingletonMonobehaviour<MainSceneManager>
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

            if (OnStateChanged != null) OnStateChanged.Invoke(value);
        }
    }
    
    public int ContinueCount { get; private set; }
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
    public event Action<GameState> OnStateChanged;
    
    bool isContinueRequested;

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

    IEnumerator OneGame()
    {
        Init();

        yield return StartCoroutine(SuggestStart());
        GameStart();

        while (true)
        {
            while (!IsGameOver())
            {
                yield return null;
            }
            
            if (!CanContinue) break;

            yield return StartCoroutine(SuggestContinue());

            if (!isContinueRequested) break;

            Continue();
        }

        GameOver();
        yield return StartCoroutine(SuggestRestart());
    }
}
