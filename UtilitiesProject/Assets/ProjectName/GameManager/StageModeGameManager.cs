using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class StageModeGameManager : BaseGameManager<StageModeGameManager>
{
    [SerializeField]
    GameOverPanel gameOverPanel = null;
    [SerializeField]
    StageClearPanel stageClearPanel = null;

    /// <summary>
    /// コンテニューができる上限回数(マイナスなら無限)
    /// </summary>
    [HideInInspector]
    public override int MaxContinueCount { get { return 1; } }
    
    protected override void Init()
    {
        base.Init();
        //todo:Playerなどの初期化をする
    }
    protected override void GameStart()
    {
        base.GameStart();
        //todo:ゲームスタート時の挙動をここに書く
    }
    protected override void GameOver()
    {
        base.GameOver();
        //todo:ゲームオーバー時の挙動をここに書く
    }
    protected override void StageClear()
    {
        base.StageClear();
        //todo:ステージクリア時の挙動をここに書く
    }

    protected override void Continue()
    {
        base.Continue();
        //todo:コンテニュー時の挙動をここに書く
    }

    protected override IEnumerator SuggestStart()
    {
        //このコルーチンが終了するとゲームが開始される

        //仮に３秒待つ
        //todo:Swipeされたらとかに変える

        while (true)
        {
            if (IsGameOver()) break;

            yield return null;
        }

        yield return new WaitForSeconds(3.0f);
    }
    protected override IEnumerator SuggestContinue()
    {
        //このコルーチンが終了したときにisContinueRequestedがtrueならコンテニューされる
        
        //ex
        yield return gameOverPanel.SuggestContinue((isRequested) =>
        {
            isContinueRequested = isRequested;
        });
        
    }
    protected override IEnumerator SuggestRestart()
    {
        //このコルーチンが終了するとゲームがリセットされる
        
        //ex
        if (CurrentState == GameState.GameOver)
        {
            yield return gameOverPanel.SuggestRestart();
        }
        else if (CurrentState == GameState.StageClear)
        {
            yield return stageClearPanel.SuggestRestart();
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
