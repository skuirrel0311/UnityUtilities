using System;
using UnityEngine;
using KanekoUtilities;

public abstract class GameStatePanel : Panel
{
    public enum EventType
    {
        Initialize,
        GameStart,
        Continue,
        GameOver,
        StageClear
    }

    protected abstract EventType[] ActivateStates { get; }
    protected abstract EventType[] DeactivateStates { get; }

    GameMode currentGameMode;

    public void SetGameMode(GameMode mode)
    {
        currentGameMode = mode;
    }

    protected virtual void Start()
    {
        for (int i = 0; i < ActivateStates.Length; i++)
        {
            SetEvent(ActivateStates[i], true);
        }
        for (int i = 0; i < DeactivateStates.Length; i++)
        {
            SetEvent(DeactivateStates[i], false);
        }
    }

    void SetEvent(EventType state, bool activate)
    {
        switch (currentGameMode)
        {
            case GameMode.Infinite:
                SetEventToInfiniteModeGameManager(state, activate);
                break;
            case GameMode.Stage:
                SetEventToStageModeGameManager(state, activate);
                break;
        }
    }

    void SetEventToInfiniteModeGameManager(EventType type, bool activate)
    {
        InfiniteModeGameManager gameManager = InfiniteModeGameManager.Instance;
        if (gameManager == null) return;
        switch (type)
        {
            case EventType.Initialize:
                if (activate) gameManager.OnInitialize += Activate;
                else gameManager.OnInitialize += Deactivate;
                break;
            case EventType.GameStart:
                if (activate) gameManager.OnGameStart += Activate;
                else gameManager.OnGameStart += Deactivate;
                break;
            case EventType.GameOver:
                if (activate) gameManager.OnGameOver += Activate;
                else gameManager.OnGameOver += Deactivate;
                break;
            case EventType.Continue:
                if (activate) gameManager.OnContinue += Activate;
                else gameManager.OnContinue += Deactivate;
                break;
        }
    }
    void SetEventToStageModeGameManager(EventType type, bool activate)
    {
        StageModeGameManager gameManager = StageModeGameManager.Instance;
        if (gameManager == null) return;
        switch (type)
        {
            case EventType.Initialize:
                if (activate) gameManager.OnInitialize += Activate;
                else gameManager.OnInitialize += Deactivate;
                break;
            case EventType.GameStart:
                if (activate) gameManager.OnGameStart += Activate;
                else gameManager.OnGameStart += Deactivate;
                break;
            case EventType.GameOver:
                if (activate) gameManager.OnGameOver += Activate;
                else gameManager.OnGameOver += Deactivate;
                break;
            case EventType.Continue:
                if (activate) gameManager.OnContinue += Activate;
                else gameManager.OnContinue += Deactivate;
                break;
            case EventType.StageClear:
                if (activate) gameManager.OnStageClear += Activate;
                else gameManager.OnStageClear += Deactivate;
                break;
        }
    }
}
