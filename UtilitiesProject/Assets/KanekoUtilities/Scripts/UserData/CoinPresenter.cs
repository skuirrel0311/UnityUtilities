using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class CoinPresenter : MonoBehaviour
{
    [SerializeField]
    UGUIImagePool imagePool = null;

    [SerializeField]
    RectTransform coinNumTextPos = null;

    [SerializeField]
    AbstractUGUIText[] coinNumTexts = null;

    Camera mainCamera;

    float screenSizeRate;

    void Start()
    {
        mainCamera = Camera.main;
        float width = mainCamera.ViewportToWorldPoint(new Vector3(1.0f, 0.0f, 1.0f)).x * 2.0f;
        if (mainCamera.orthographic)
        {
            screenSizeRate = (width / mainCamera.orthographicSize) * (1080.0f / Screen.width);
        }
        else
        {
            screenSizeRate = Screen.width / 1080.0f;
        }
    }

    public void Init()
    {
        StopAllCoroutines();
        foreach (var c in coinNumTexts)
        {
            c.Text = MyUserDataManager.Instance.CurrentCoinNum.ToString();
        }
    }

    public void PresentCoin(int coinNum, Vector3 origin, int startCoinNum, System.Action callback)
    {
        float popingDuration = 0.5f;
        float earnDuration = 0.75f;
        int showCoinNum = Mathf.RoundToInt(coinNum * 0.1f);
        showCoinNum = Mathf.Min(15, showCoinNum);
        var maxDelayFrame = 0;

        for (int i = 0; i < showCoinNum; i++)
        {
            maxDelayFrame = 2 * i;
            StartCoroutine(KKUtilities.DelayFrame(maxDelayFrame, () => StartCoroutine(EarnCoinAnimation(origin, popingDuration, earnDuration))));
        }

        float delay = popingDuration + earnDuration - 0.25f;
        float duration = 0.08f * showCoinNum;
        StartCoroutine(KKUtilities.Delay(delay, () =>
        {
            CountUpText(startCoinNum, startCoinNum + coinNum, duration);
        }, false));

        this.DelayFrame(maxDelayFrame, () =>
        {
            this.Delay(popingDuration + earnDuration, () => callback.SafeInvoke());
        });
    }

    public void PlayShortAnimation(int coinNum, Vector3 origin, float popingDuration = 0.5f)
    {
        for (int i = 0; i < coinNum; i++)
        {
            StartCoroutine(KKUtilities.Delay(0.1f * i, () => StartCoroutine(EarnCoinShortAnimation(origin, popingDuration)), false));
        }

        int startCoinNum = MyUserDataManager.Instance.CurrentCoinNum;
        StartCoroutine(KKUtilities.Delay(popingDuration, () => CountUpText(startCoinNum, startCoinNum + coinNum, 0.5f), false));
        MyUserDataManager.Instance.EarnCoin(coinNum);
    }

    public void CountUpText(int start, int end, float duration, bool vibrate = true)
    {
        if (vibrate)
        {
            StartCoroutine(VibrateCoroutine(duration * 0.8f));
        }
        foreach (var c in coinNumTexts)
        {
            StartCoroutine(KKUtilities.FloatLerp(duration, (t) =>
            {
                c.Text = ((int)Mathf.Lerp(start, end, t)).ToString();
            }, false));
        }
    }

    IEnumerator VibrateCoroutine(float duration)
    {
        var manager = MyVibrationManager.Instance;
        
        yield return KKUtilities.FloatLerp(duration, (_) =>
        {
            manager.Vibrate(VibrationType.SelectionChange);
        });
    }

    IEnumerator EarnCoinAnimation(Vector3 origin, float popingDuration, float earnDuration)
    {
        var image = imagePool.GetInstance();
        var rec = ((RectTransform)image.transform);
        rec.position = origin;

        float Radius = Random.Range(130.0f, 150.0f) * screenSizeRate;
        Vector3 midwayPos = rec.position + (-new Vector2(Random.Range(-1.0f, 1.0f), Random.value).normalized * Radius).ToXYVector3();

        yield return KKUtilities.FloatLerp(popingDuration, (t) =>
        {
            rec.position = Vector3.Lerp(origin, midwayPos, Easing.OutQuad(t));
        });

        yield return KKUtilities.FloatLerp(earnDuration, (t) =>
        {
            rec.position = Vector3.Lerp(midwayPos, coinNumTextPos.position, Easing.OutCubic(t));
        });

        imagePool.ReturnInstance(image);
    }

    IEnumerator EarnCoinShortAnimation(Vector3 origin, float popingDuration)
    {
        var image = imagePool.GetInstance();

        origin = mainCamera.WorldToScreenPoint(origin);
        var rec = ((RectTransform)image.transform);
        rec.position = origin;

        float Radius = 150.0f * screenSizeRate;
        Vector3 midwayPos = rec.position + Vector3.up * Radius;

        yield return KKUtilities.FloatLerp(popingDuration, (t) =>
        {
            rec.position = Vector3.Lerp(origin, midwayPos, Easing.OutQuad(t));
        });

        yield return KKUtilities.FloatLerp(0.3f, (t) =>
        {
            image.transform.localScale = Vector3.one * Mathf.Lerp(1.0f, 0.0f, t);
        });

        imagePool.ReturnInstance(image);
    }
}
