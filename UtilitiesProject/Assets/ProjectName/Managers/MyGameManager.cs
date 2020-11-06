using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;
#if IMPORT_HYPERCOMMON
using HyperCasual.StageSystem;
#endif

public class MyGameManager : BaseGameManager<MyGameManager>
{
    [SerializeField]
    PlayerFacade player = null;
    public PlayerFacade Player { get { return player; } }
    [SerializeField]
    StageFacade stage = null;
    public StageFacade Stage { get { return stage; } }

    /// <summary>
    /// コンテニューができる上限回数(マイナスなら無限)
    /// </summary>
    public override int MaxContinueCount { get { return 0; } }

    bool isFailedContinue = false;
    bool isGameOver = false;
    bool isStageClear = false;

#if IMPORT_HYPERCOMMON
    StageScoreManager stageScoreManager;
#endif

    protected override void Awake()
    {
        base.Awake();

        Application.targetFrameRate = 60;
        QualitySettings.vSyncCount = 0;
    }

    protected override void Start()
    {
#if IMPORT_HYPERCOMMON
        stageScoreManager = StageScoreManager.Default;
#endif

        //このStartが呼ばれるとゲームループが走る
        base.Start();
    }

    protected override void Init()
    {
        base.Init();
        isFailedContinue = false;
        isGameOver = false;
        isStageClear = false;
        //todo:Playerなどの初期化をする
        player.Init();
        stage.Init();
    }
    protected override void GameStart()
    {
        base.GameStart();
        //todo:ゲームスタート時の挙動をここに書く

        player.OnGameStart();
        stage.OnGameStart();

#if IMPORT_HYPERCOMMON
        stageScoreManager.StartStage(CurrentLevel);
#endif
    }
    protected override void GameOver()
    {
        base.GameOver();
        //todo:ゲームオーバー時の挙動をここに書く

        player.OnGameEnd();
        stage.OnGameEnd();

#if IMPORT_HYPERCOMMON
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

    protected override void StageClear()
    {
        base.StageClear();
        //todo:ステージクリア時の挙動をここに書く

        player.OnGameEnd();
        stage.OnGameEnd();

#if IMPORT_HYPERCOMMON
        stageScoreManager.Success(CurrentLevel, 0, 0);
#endif
        currentLevel.SetValue(CurrentLevel + 1);
    }

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

    public void OnGameOver()
    {
        isGameOver = true;
    }
    protected override bool IsGameOver()
    {
        //この関数がfalseを返す間はゲームが継続される
        return isGameOver;
    }

    public void OnStageClear()
    {
        isStageClear = true;
    }
    bool IsStageClear()
    {
        return isStageClear;
    }

    public void ChangeLevel(int level)
    {
        currentLevel.SetValue(level);
        StopAllCoroutines();
        //StartCoroutine(GameLoop());
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void GotoBackLevel()
    {
        ChangeLevel(CurrentLevel - 1);
    }

    public void GotoNextLevel()
    {
        ChangeLevel(CurrentLevel + 1);
    }

    public void Retry()
    {
        var callback = new MyUnityEvent();
        callback.AddListener(() => ChangeLevel(CurrentLevel));
        MyAdManager.Instance.ShowStageClearInterstitial(callback);
    }

    protected override IEnumerator OneGame()
    {
        Debug.Log("start game on stage mode");
        bool isStageClear = false;

        Init();
        yield return StartCoroutine(SuggestStart());
        GameStart();

        while(true)
        {
            while(!IsGameOver())
            {
                yield return null;
                if(IsStageClear())
                {
                    isStageClear = true;
                    break;
                }
            }

            if(isStageClear) break;
            yield return this.Delay(0.75f, ()=> GameOver());
            if(!CanContinue) break;
            yield return StartCoroutine(SuggestContinue());

            if(isContinueRequested != ContinueRequestType.Continue)
            {
                MyAdManager.Instance.ShowStageClearInterstitial();
                yield break;
            }

            Continue();

            if(isFailedContinue) break;
        }

        if(isStageClear)
        {
            yield return this.Delay(0.75f, ()=>  StageClear());
        }
        yield return StartCoroutine(SuggestRestart());
        
        Retry();
        //MyAdManager.Instance.ShowStageClearInterstitial();
    }
}
