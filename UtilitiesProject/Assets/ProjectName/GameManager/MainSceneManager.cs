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

public class MainSceneManager : SingletonMonobehaviour<MainSceneManager>
{
    public GameState CurrentState { get; private set; }

    public int ContinueCount { get; private set; }
    public bool CanContinue { get { return ContinueCount < 1; } }

    public event Action OnGameStart;
    public event Action OnContinue;
    public event Action OnGameOver;
    
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

    void Init()
    {
        CurrentState = GameState.Ready;
    }

    void GameStart()
    {
        CurrentState = GameState.InGame;

        if(OnGameStart != null) OnGameStart();
    }

    IEnumerator SuggestStart()
    {
        yield return null;
    }

    void GameOver()
    {
        CurrentState = GameState.Result;

        if (OnGameOver != null) OnGameOver();
    }

    IEnumerator SuggestRestart()
    {
        yield return null;
    }

    void Continue()
    {
        ContinueCount++;

        if (OnContinue != null) OnContinue();
    }

    IEnumerator SuggestContinue()
    {
        yield return null;
        isContinueRequested = true;
    }

    public bool IsGameOver()
    {
        return false;
    }
}
