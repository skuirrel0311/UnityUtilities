using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;
#if IMPORT_HYPERCOMMON
using HyperCasual.StageSystem;
#endif

public partial class MyGameManager : BaseGameManager<MyGameManager>
{
    /// <summary>
    /// コンテニューができる上限回数(マイナスなら無限)
    /// </summary>
    public override int MaxContinueCount { get { return 1; } }
    
    bool isFailedContinue = false;
    MyScoreManager scoreManager;

#if IMPORT_HYPERCOMMON && STAGE_MODE
    StageScoreManager stageScoreManager;
#endif

    protected override void Start()
    {
        scoreManager = MyScoreManager.Instance;
        
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
#if IMPORT_HYPERCOMMON && STAGE_MODE
    stageScoreManager.StartStage(CurrentLevel);
#endif
    }
    protected override void GameOver()
    {
        base.GameOver();
        scoreManager.UpdateBestScore();
        //todo:ゲームオーバー時の挙動をここに書く

#if IMPORT_HYPERCOMMON && STAGE_MODE
    stageScoreManager.Fail(CurrentLevel);
#endif
    }
    protected override void Continue()
    {
        base.Continue();

        //todo:コンテニュー時の挙動をここに書く

        //失敗した場合はisFailedContinueをtrueにする
        //isFailedContinue = true;
    }

#if STAGE_MODE
    protected override void StageClear()
    {
        base.StageClear();
        //todo:ステージクリア時の挙動をここに書く
#if IMPORT_HYPERCOMMON && STAGE_MODE
    stageScoreManager.Success(CurrentLevel, scoreManager.TotalScore, 0);
#endif
        currentLevel.SetValue(CurrentLevel + 1);
        scoreManager.UpdateBestScore();
        MyAdManager.Instance.ShowStageClearInterstitial();
    }
#endif

    protected override IEnumerator SuggestStart()
    {
        //このコルーチンが終了するとゲームが開始される

        //ex TitlePanelのボタンを押したらスタート
        yield return base.SuggestStart();
    }
    protected override IEnumerator SuggestContinue()
    {
        //このコルーチンが終了したときにisContinueRequestedがtrueならコンテニューされる

        //ex GameOverPanelのRiviveボタンを押したらコンテニュー、NoThanksを押したらリスタート
        yield return base.SuggestContinue();
    }
    protected override IEnumerator SuggestRestart()
    {
        //このコルーチンが終了するとゲームがリセットされる

        //ex GameOverPanelのTapToRestartを押したらリスタート
        yield return base.SuggestRestart();
    }

    protected override bool IsGameOver()
    {
        //この関数がfalseを返す間はゲームが継続される

        return false;
    }

#if STAGE_MODE
    bool IsStageClear()
    {
        return false;
    }
#endif
}
