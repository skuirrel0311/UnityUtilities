using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class UIManager : SingletonMonobehaviour<UIManager>
{
    [SerializeField]
    TitlePanel titlePanel = null;
    public TitlePanel TitlePanel { get { return titlePanel; } }
    [SerializeField]
    InGamePanel inGamePanel = null;
    public InGamePanel InGamePanel { get { return inGamePanel; } }
    [SerializeField]
    GameOverPanel gameOverPanel = null;
    public GameOverPanel GameOverPanel { get { return gameOverPanel; } }
    [SerializeField]
    StageClearPanel stageClearPanel = null;
    public StageClearPanel StageClearPanel { get { return stageClearPanel; } }

    public void OnInitialized()
    {
        titlePanel.Activate();
        inGamePanel.Deactivate();
        gameOverPanel.Deactivate();
        if(stageClearPanel != null) stageClearPanel.Deactivate();
    }

    public void OnGameStart()
    {
        titlePanel.Deactivate();
        inGamePanel.Activate();
    }

    public void OnGameOver()
    {
        gameOverPanel.Activate();
    }

    public void OnContinue()
    {
        gameOverPanel.Deactivate();
    }

    public void OnStageClear()
    {
        if(stageClearPanel != null) stageClearPanel.Activate();
    }
}
