using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    public class DisplayFader : SingletonMonobehaviour<DisplayFader>
    {
        [SerializeField]
        Image panel = null;

        Coroutine fadeCoroutine;

        IEnumerator Fade(Color startColor, Color endColor, float duration, Action endCallback, bool finishedPanelActive)
        {
            panel.color = startColor;
            panel.gameObject.SetActive(true);

            yield return StartCoroutine(KKUtilities.FloatLerp(duration, (t) =>
            {
                panel.color = Color.Lerp(startColor, endColor, t);
            })
            );

            fadeCoroutine = null;
            panel.gameObject.SetActive(finishedPanelActive);
            if (endCallback != null) endCallback.Invoke();
        }
        /// <summary>
        /// 次第にはっきりする
        /// </summary>
        public void FadeIn(float duration, Color fadeColor, Action endCallback = null)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(Fade(fadeColor, Color.clear, duration, endCallback, false));
        }
        /// <summary>
        /// 徐々に見えなくする
        /// </summary>
        public void FadeOut(float duration, Color fadeColor, Action endCallback = null)
        {
            if (fadeCoroutine != null) StopCoroutine(fadeCoroutine);

            fadeCoroutine = StartCoroutine(Fade(Color.clear, fadeColor, duration, endCallback, true));
        }
    }
}