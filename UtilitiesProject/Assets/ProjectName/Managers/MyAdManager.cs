using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MyAdManager : Singleton<MyAdManager>
{
    public MyAdManager()
    {
        //Condition intervalFromInstall = Conditions.IntervalFromInstall(60.0f, HyperCasual.Calculator.CompareOperation.Greater);
        //Condition intervalFromLastInterstitialAd = Conditions.IntervalFromLastInterstitialAd(30.0f, HyperCasual.Calculator.CompareOperation.Greater);
        //Condition intervalFromOpen = Conditions.IntervalFromOpen(30.0f, HyperCasual.Calculator.CompareOperation.Greater);

        //AllCondition gameOverCondition = new AllCondition(intervalFromInstall, intervalFromOpen, intervalFromLastInterstitialAd);

        ////インストールから６０秒、起動から３０秒、前回の広告から３０秒経っていたら出す
        //AdManager.Instance.AddConditionToShow("GameOver", gameOverCondition);

        ////インストールしてから６０秒以内のみ出さない。それ以外は確定で出す
        //AdManager.Instance.AddConditionToShow("StageClear", intervalFromInstall);
    }

    public void ShowGameOverInterstitial()
    {
        //AdManager.Instance.ShowInterstitialWithKey("GameOver", () => { }, (error) => { });
    }

    public void ShowStageClearInterstitial()
    {
        //AdManager.Instance.ShowInterstitialWithKey("StageClear", () =>{ }, (error) => { });
    }
}
