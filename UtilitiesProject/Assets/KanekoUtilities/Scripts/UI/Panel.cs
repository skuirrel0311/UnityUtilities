using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace KanekoUtilities
{
    //画面いっぱいに出るUI
    [RequireComponent(typeof(CanvasGroup))]
    public class Panel : UGUIImage
    {
        [SerializeField]
        bool activeOnAwake = false;

        [SerializeField]
        float activateDuration = 0.2f;

        [SerializeField]
        float deactivateDuration = 0.5f;
        
        protected CanvasGroup group;
        GameObject rootObject;

        Coroutine alphaControlCoroutine;

        protected virtual void Awake()
        {
            group = GetComponent<CanvasGroup>();
            Image.enabled = activeOnAwake;
            rootObject = transform.GetChild(0).gameObject;
            rootObject.SetActive(activeOnAwake);

            group.alpha = activeOnAwake ? 1.0f : 0.0f;
        }

        public virtual void Activate()
        {
            Image.enabled = true;
            rootObject.SetActive(true);
            group.interactable = true;
            group.blocksRaycasts = true;
            PlayAlphaControlAnimation(0.0f, 1.0f, activateDuration);
        }

        public virtual void Deactivate()
        {
            Image.enabled = false;
            group.interactable = false;
            group.blocksRaycasts = false;
            PlayAlphaControlAnimation(1.0f, 0.0f, deactivateDuration, ()=> rootObject.SetActive(false));
        }

        void PlayAlphaControlAnimation(float startAlpha, float endAlpha, float duration, Action callback = null)
        {
            if (alphaControlCoroutine != null)
            {
                StopCoroutine(alphaControlCoroutine);
            }

            alphaControlCoroutine = StartCoroutine(KKUtilities.FloatLerp(duration, (t) =>
            {
                group.alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            }).OnCompleted(() =>
            {
                if (callback != null) callback();
                alphaControlCoroutine = null;
            }));
        }
    }
}
