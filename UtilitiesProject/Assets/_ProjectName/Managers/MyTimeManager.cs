using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using KanekoUtilities;

public class MyTimeManager : SingletonMonobehaviour<MyTimeManager>
{
    public Coroutine ChangeTimeScale(float targetScale, float duration = 0.3f, EaseType ease = EaseType.Linear)
    {
        float start = Time.timeScale;
        return this.FloatLerp(duration, (t) =>
        {
            Time.timeScale = Mathf.Lerp(start, targetScale,Easing.GetEase(t, ease));
        }, false);
    }
}
