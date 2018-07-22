using System;
using UnityEngine;
using KanekoUtilities;

public abstract class GameStatePanel : Panel
{
    protected abstract GameState ActivateState { get; }
    protected abstract GameState DeactivateState { get; }

    protected virtual void Start()
    {
        SetStateEvent(ActivateState, true);
        SetStateEvent(DeactivateState, false);
    }

    void SetStateEvent(GameState state, bool activate)
    {
        MainSceneManager sceneManager = MainSceneManager.Instance;
        switch (state)
        {
            case GameState.Ready:
                if (activate) sceneManager.OnInitialize += Activate;
                else sceneManager.OnInitialize += Deactivate;
                break;
            case GameState.InGame:
                if (activate) sceneManager.OnGameStart += Activate;
                else sceneManager.OnGameStart += Deactivate;
                break;
            case GameState.Result:
                if (activate) sceneManager.OnGameOver += Activate;
                else sceneManager.OnGameOver += Deactivate;
                break;
        }
    }
}
