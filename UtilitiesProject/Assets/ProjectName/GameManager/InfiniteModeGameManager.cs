using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public partial class InfiniteModeGameManager : BaseGameManager<InfiniteModeGameManager>
{
    [SerializeField]
    GameOverPanel gameOverPanel = null;

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
    protected override void Continue()
    {
        base.Continue();
        //todo:コンテニュー時の挙動をここに書く
    }

    protected override IEnumerator SuggestStart()
    {
        //このコルーチンが終了するとゲームが開始される
        
        //todo:Tapされたらとかに変える
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
        yield return gameOverPanel.SuggestRestart();
    }
    
    protected override bool IsGameOver()
    {
        //この関数がfalseを返す間はゲームが継続される

        return true;
    }
}
