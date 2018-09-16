using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class InfiniteModeGameManager : BaseGameManager<InfiniteModeGameManager>
{
    /// <summary>
    /// コンテニューができる上限回数(マイナスなら無限)
    /// </summary>
    public override int MaxContinueCount { get { return 1; } }

    bool isFailedContinue = false;
    InfiniteModeScoreManager scoreManager;

    protected override void Start()
    {
        scoreManager = InfiniteModeScoreManager.Instance;

        //このStartが呼ばれるとゲームループが走る
        base.Start();
    }

    protected override void Init()
    {
        base.Init();
        isFailedContinue = false;
        scoreManager.Init();
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

        if (isContinueRequested != ContinueRequestType.Continue)
        {
            MyAdManager.Instance.ShowGameOverInterstitial();
        }
    }
    protected override IEnumerator SuggestRestart()
    {
        //このコルーチンが終了するとゲームがリセットされる

        //ex GameOverPanelのTapToRestartを押したらリスタート
        yield return uiManager.GameOverPanel.SuggestRestart();
    }
    
    protected override bool IsGameOver()
    {
        //この関数がfalseを返す間はゲームが継続される

        return false;
    }
}
