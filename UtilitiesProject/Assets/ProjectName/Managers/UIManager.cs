using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

[RequireComponent(typeof(PanelSwitcher))]
public class UIManager : SingletonMonobehaviour<UIManager>
{
    public PanelSwitcher PanelSwitcher { get; private set; }
    
    public TitlePanel TitlePanel { get { return (TitlePanel)PanelSwitcher.GetPanel(PanelType.Title); } }
    public InGamePanel InGamePanel { get { return (InGamePanel)PanelSwitcher.GetPanel(PanelType.InGame); } }
    public GameOverPanel GameOverPanel { get { return (GameOverPanel)PanelSwitcher.GetPanel(PanelType.GameOver); } }
#if STAGE_MODE
    public StageClearPanel StageClearPanel { get { return (StageClearPanel)PanelSwitcher.GetPanel(PanelType.StageClear); } }
#endif

    protected override void Awake()
    {
        base.Awake();
        PanelSwitcher = GetComponent<PanelSwitcher>();
    }

    public void OnInitialized()
    {
        PanelSwitcher.SwitchPanel(PanelType.Title);
    }

    public void OnGameStart()
    {
        PanelSwitcher.SwitchPanel(PanelType.InGame);
    }

    public void OnGameOver()
    {
        PanelSwitcher.AddPanel(PanelType.GameOver);
    }

    public void OnContinue()
    {
        PanelSwitcher.HidePanel(PanelType.GameOver);
    }

    public void OnStageClear()
    {
        PanelSwitcher.AddPanel(PanelType.StageClear);
    }
}
