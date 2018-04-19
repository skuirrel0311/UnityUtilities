using System;
using UnityEngine;
using KanekoUtilities;

public enum GameState
{
    Title,
    InGame,
    Result
}

public class MainSceneManager : SingletonMonobehaviour<MainSceneManager>
{
    public GameState CurrentState { get; private set; }

    public event Action OnGameStart;
    public event Action OnGameOver;
    public event Action OnContinue;
    public event Action OnResetGame;
    
    protected override void Start()
    {
        base.Start();
        Init();
    }

    public void GameStart()
    {
        CurrentState = GameState.InGame;
        if (OnGameStart != null) OnGameStart.Invoke();
    }

    void Init()
    {
    }

    public void GameOver()
    {
        CurrentState = GameState.Result;
        if (OnGameOver != null) OnGameOver.Invoke();
    }

    public void Continue()
    {
        CurrentState = GameState.InGame;
        if (OnContinue != null) OnContinue.Invoke();
    }

    public void ResetGame()
    {
        CurrentState = GameState.Title;
        Init();
        if (OnResetGame != null) OnResetGame.Invoke();
    }
}
