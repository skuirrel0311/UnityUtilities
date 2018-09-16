using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public partial class StageModeGameManager : BaseGameManager<StageModeGameManager>
{
    /// <summary>
    /// コンテニューができる上限回数(マイナスなら無限)
    /// </summary>
    [HideInInspector]
    public override int MaxContinueCount { get { return 1; } }

    StageModeScoreManager scoreManager;
    bool isFailedContinue = false;

    protected override void Start()
    {
        scoreManager = StageModeScoreManager.Instance;
        //このStartが呼ばれるとゲームループが走る
        base.Start();
    }

    protected override void Init()
    {
        base.Init();
        scoreManager.Init();
        levelAdjuster.SetScore(scoreManager.CurrentLevel);
        isFailedContinue = false;
        //todo:Playerなどの初期化をする

    }
    protected override void GameStart()
    {
        base.GameStart();
        levelAdjuster.StartAdjustment();
        //todo:ゲームスタート時の挙動をここに書く

    }
    protected override void GameOver()
    {
        base.GameOver();
        scoreManager.UpdateBestScore();
        //todo:ゲームオーバー時の挙動をここに書く


    }
    protected override void StageClear()
    {
        base.StageClear();
        scoreManager.CurrentLevel++;
        scoreManager.UpdateBestScore();
        //todo:ステージクリア時の挙動をここに書く

        MyAdManager.Instance.ShowStageClearInterstitial();
    }

    protected override void Continue()
    {
        base.Continue();
        //todo:コンテニュー時の挙動をここに書く

        //失敗した場合はisFailedContinueをtrueにする
        //isFailedContinue = true;
    }

    protected override IEnumerator SuggestStart()
    {
        //このコルーチンが終了するとゲームが開始される

        //ex TitlePanelのボタンを押したらスタート
        yield return uiManager.TitlePanel.SuggestGameStart();
    }
    protected override IEnumerator SuggestContinue()
    {
        //このコルーチンが終了したときにisContinueRequestedがtrueならコンテニューされる

        //ex GameOverPanelのRiviveボタンを押したらコンテニュー、NoThanksを押したらリスタート
        yield return uiManager.GameOverPanel.SuggestContinue((isRequested) =>
        {
            isContinueRequested = isRequested;
        });
        
        if(isContinueRequested != ContinueRequestType.Continue)
        {
            MyAdManager.Instance.ShowGameOverInterstitial();
        }
    }
    protected override IEnumerator SuggestRestart()
    {
        //このコルーチンが終了するとゲームがリセットされる

        //ex GameOver or GameClearPanelのTapToRestartを押したらリスタート
        if (CurrentState == GameState.GameOver)
        {
            yield return uiManager.GameOverPanel.SuggestRestart();
        }
        else if (CurrentState == GameState.StageClear)
        {
            yield return uiManager.StageClearPanel.SuggestRestart();
        }
        else
        {
            yield return null;
        }
    }
    
    protected override bool IsGameOver()
    {
        //この関数がfalseを返す間はゲームが継続される

        return false;
    }
    
    bool IsStageClear()
    {
        return false;
    }
}
