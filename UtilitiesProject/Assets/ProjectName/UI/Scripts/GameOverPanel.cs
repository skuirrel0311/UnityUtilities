using System;
using System.Collections;
using UnityEngine;
using KanekoUtilities;

public class GameOverPanel : GameStatePanel
{
    [SerializeField]
    ContinueButton continueButton = null;
    [SerializeField]
    GameObject tapToRestartContainer = null;
    [SerializeField]
    UGUIButton tapToRestartButton = null;

    public IEnumerator SuggestContinue(Action<ContinueRequestType> onSelected)
    {
        //コンテニューする場合
        //コンテニューボタンが押された

        //コンテニューしない場合
        //NoThanksボタンを押された、タイムアップした

        bool isEnd = false;

        continueButton.gameObject.SetActive(true);

        continueButton.OnClickEvent.RemoveAllListeners();
        continueButton.AddListener(() =>
        {
            isEnd = true;
            onSelected.SafeInvoke(ContinueRequestType.Continue);
        });


        continueButton.StartCountDown(() =>
        {
            isEnd = true;
            onSelected.SafeInvoke(ContinueRequestType.TimeOut);
        },
        () =>
        {
            isEnd = true;
            onSelected.SafeInvoke(ContinueRequestType.NoThanks);
        });

        yield return new WaitUntil(() => isEnd);
        
        continueButton.gameObject.SetActive(false);
    }

    public IEnumerator SuggestRestart()
    {
        tapToRestartContainer.SetActive(true);

        yield return KKUtilities.WaitAction(tapToRestartButton.OnClickEvent, () =>
        {
            tapToRestartContainer.SetActive(false);
        });
    }
}