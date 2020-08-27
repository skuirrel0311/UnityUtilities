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

    public bool IsAvailable { get; set; }

    public MyAdManager()
    {
        IsAvailable = true;

#if IMPORT_HYPERCOMMON
        Condition intervalFromLastInterstitialAd10 = ConditionFactory.IntervalFromLastInterstitialAd(10.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition intervalFromOpen = ConditionFactory.IntervalFromOpen(10.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition intervalFromRewardVideo = ConditionFactory.IntervalFromLastRewardAdSuccess(10.0f, HyperCasual.Calculator.CompareOperation.Greater);
        Condition playCountFromInstall = ConditionFactory.PlayCountFromInstall(3, HyperCasual.Calculator.CompareOperation.Greater);

        AllCondition GameOverAdCondition = new AllCondition(intervalFromOpen, intervalFromLastInterstitialAd10);
        AllCondition ClearAdCondition = new AllCondition(intervalFromOpen, intervalFromLastInterstitialAd10, intervalFromRewardVideo, playCountFromInstall);

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

    public void ShowChallengeModeInterstitial(MyUnityEvent callback = null)
    {
        ShowInterstitial(StageClearKey, callback);
    }

    void ShowInterstitial(string key, MyUnityEvent callback)
    {
        if (!IsAvailable)
        {
            callback.SafeInvoke();
            return;
        }

#if IMPORT_HYPERCOMMON
        AudioManager.Instance.SetMute(true);
        AdManager.Instance.ShowInterstitialWithKey(key, () =>
        {
        AudioManager.Instance.SetMute(false);
            callback.SafeInvoke();
        }
        , (error) =>
        {
        AudioManager.Instance.SetMute(false);
            callback.SafeInvoke();
            Debug.Log(error);
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

    public void ShowRewardVideo(string key, System.Action onSuccess, System.Action onFailuer)
    {
#if IMPORT_HYPERCOMMON
        AudioManager.Instance.SetMute(true);
        AdManager.Instance.ShowRewardBasedVideoWithKey (key, (_) =>
        {
            AudioManager.Instance.SetMute(false);
            onSuccess.SafeInvoke();
        }, (_) =>
        {
            AudioManager.Instance.SetMute(false);
            onFailuer.SafeInvoke();
        });
#endif
    }
}