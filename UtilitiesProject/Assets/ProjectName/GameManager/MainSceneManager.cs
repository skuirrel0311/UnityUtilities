using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public partial class MainSceneManager : SingletonMonobehaviour<MainSceneManager>
{
    /// <summary>
    /// コンテニューができる上限回数(マイナスなら無限)
    /// </summary>
    [HideInInspector]
    public int MaxContinueCount = 0;

    void Init()
    {
        CurrentState = GameState.Ready;
        if (OnInitialize != null) OnInitialize.Invoke();

        //todo:Playerなどの初期化をする
    }
    void GameStart()
    {
        CurrentState = GameState.InGame;

        if (OnGameStart != null) OnGameStart();

        //todo:ゲームスタート時の挙動をここに書く
    }
    void GameOver()
    {
        CurrentState = GameState.Result;

        if (OnGameOver != null) OnGameOver();

        //todo:ゲームオーバー時の挙動をここに書く
    }
    void Continue()
    {
        ContinueCount++;

        if (OnContinue != null) OnContinue();

        //todo:コンテニュー時の挙動をここに書く
    }

    IEnumerator SuggestStart()
    {
        //このコルーチンが終了するとゲームが開始される

        //仮に３秒待つ
        //todo:Swipeされたらとかに変える
        yield return new WaitForSeconds(30.0f);
    }
    IEnumerator SuggestRestart()
    {
        //このコルーチンが終了するとゲームがリセットされる

        //仮に３秒待つ
        //todo:（ボタンが押されたらとかに変える）
        yield return new WaitForSeconds(3.0f);
    }
    IEnumerator SuggestContinue()
    {
        //このコルーチンが終了したときにisContinueRequestedがtrueならコンテニューされる

        //仮に３秒待つ
        //todo:（ボタンが押されたらとかに変える）
        yield return new WaitForSeconds(3.0f);
        isContinueRequested = true;
    }

    public bool IsGameOver()
    {
        //この関数がfalseを返す間はゲームが継続される

        return false;
    }
}
