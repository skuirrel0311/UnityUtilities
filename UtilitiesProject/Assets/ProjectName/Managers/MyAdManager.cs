using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

#if IMPORT_HYPERCOMMON
using HyperCasual.Ads;
using HyperCasual.Conditions;
#endif

public class MyAdManager : Singleton<MyAdManager>
{
    public MyAdManager()
    {
#if IMPORT_HYPERCOMMON
        Condition intervalFromInstall = Conditions.IntervalFromInstall(60.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition intervalFromLastInterstitialAd = Conditions.IntervalFromLastInterstitialAd(30.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition intervalFromOpen = Conditions.IntervalFromOpen(30.0f, HyperCasual.Calculator.CompareOperation.Greater);

        AllCondition gameOverCondition = new AllCondition(intervalFromInstall, intervalFromOpen, intervalFromLastInterstitialAd);

        //インストールから６０秒、起動から３０秒、前回の広告から３０秒経っていたら出す
        AdManager.Instance.AddConditionToShow("GameOver", gameOverCondition);

        //インストールしてから６０秒以内のみ出さない。それ以外は確定で出す
        AdManager.Instance.AddConditionToShow("StageClear", intervalFromInstall);
#endif
    }

    public void ShowGameOverInterstitial()
    {
#if IMPORT_HYPERCOMMON
        if (!AdManager.Instance.ShouldShowAd("GameOver")) return;

        bool enable = AudioManager.Instance.AudioEnable.GetValue();
        AudioManager.Instance.SetEnable(false);

        KKUtilities.Delay(0.5f, () =>
        {
            AdManager.Instance.ShowInterstitialWithKey("GameOver", () =>
            {
                AudioManager.Instance.SetEnable(enable);
            }
            , (error) =>
            {
                AudioManager.Instance.SetEnable(enable);
            });
        }, MyGameManager.Instance);
#endif
    }

    public void ShowStageClearInterstitial()
    {
#if IMPORT_HYPERCOMMON
        if (!AdManager.Instance.ShouldShowAd("StageClear")) return;

        bool enable = AudioManager.Instance.AudioEnable.GetValue();
        AudioManager.Instance.SetEnable(false);

        KKUtilities.Delay(0.5f, () =>
        {
            AdManager.Instance.ShowInterstitialWithKey("StageClear", () =>
            {
                AudioManager.Instance.SetEnable(enable);
            }
            , (error) =>
            {
                AudioManager.Instance.SetEnable(enable);
            });
        }, MyGameManager.Instance);
#endif
    }
}
