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

        continueButton.OnClick.RemoveAllListeners();
        continueButton.OnClick.AddListener(() =>
        {
            isEnd = true;
            if (onSelected != null) onSelected(ContinueRequestType.Continue);
        });


        continueButton.StartCountDown(() =>
        {
            isEnd = true;
            if (onSelected != null) onSelected(ContinueRequestType.TimeOut);
        },
        () =>
        {
            isEnd = true;
            if (onSelected != null) onSelected(ContinueRequestType.NoThanks);
        });

        yield return new WaitUntil(() => isEnd);
        
        continueButton.gameObject.SetActive(false);
    }

    public IEnumerator SuggestRestart()
    {
        tapToRestartContainer.SetActive(true);

        yield return KKUtilities.WaitAction(tapToRestartButton.OnClick, () =>
        {
            tapToRestartContainer.SetActive(false);
        });
    }
}