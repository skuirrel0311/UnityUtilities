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
    const string GameOverKey = "GameOver";
    const string StageClearKey = "StageClear";

    public MyAdManager()
    {
#if IMPORT_HYPERCOMMON
        Condition intervalFromLastInterstitialAd25 = ConditionFactory.IntervalFromLastInterstitialAd(25.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition intervalFromLastInterstitialAd30 = ConditionFactory.IntervalFromLastInterstitialAd(30.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition intervalFromOpen = ConditionFactory.IntervalFromOpen(10.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition intervalFromRewardVideo = ConditionFactory.IntervalFromLastRewardAdSuccess(25.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition playCountFromInstall = ConditionFactory.PlayCountFromInstall(3, HyperCasual.Calculator.CompareOperation.Greater);

        AllCondition GameOverAdCondition = new AllCondition(intervalFromOpen, intervalFromLastInterstitialAd30);
        AllCondition ClearAdCondition = new AllCondition(intervalFromOpen, intervalFromLastInterstitialAd30, intervalFromRewardVideo, playCountFromInstall);

        AdManager.Instance.SetConditionToShow(GameOverKey, GameOverAdCondition);
        AdManager.Instance.SetConditionToShow(StageClearKey, ClearAdCondition);
#endif
    }

    public void ShowGameOverInterstitial(MyUnityEvent callback = null)
    {
        ShowInterstitial(GameOverKey, callback);
    }

    public void ShowStageClearInterstitial(MyUnityEvent callback = null)
    {
        ShowInterstitial(StageClearKey, callback);
    }

    void ShowInterstitial(string key, MyUnityEvent callback)
    {
#if IMPORT_HYPERCOMMON
        bool enable = AudioManager.Instance.Enable;
        AudioManager.Instance.SetEnable(false);

        AdManager.Instance.ShowInterstitialWithKey(key, () =>
        {
            AudioManager.Instance.SetEnable(enable);
            callback.SafeInvoke();
        }
        , (error) =>
        {
            AudioManager.Instance.SetEnable(enable);
            callback.SafeInvoke();
        });
#endif
    }

    public bool IsRewardVideoLoaded
    {
        get
        {
#if IMPORT_HYPERCOMMON
            return AdManager.Instance.IsRewardBasedVideoLoaded();
#else
            return true;
#endif
        }
    }

    public void ShowRewardVideo(System.Action onSuccess, System.Action onFailuer)
    {
#if IMPORT_HYPERCOMMON
        AdManager.Instance.ShowRewardBasedVideo((_) =>
        {
            onSuccess.SafeInvoke();
        }, (_) =>
        {
            onFailuer.SafeInvoke();
        });
#endif
    }
}