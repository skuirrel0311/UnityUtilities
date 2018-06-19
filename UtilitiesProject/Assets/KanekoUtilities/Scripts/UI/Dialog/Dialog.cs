using System;
using UnityEngine;

namespace KanekoUtilities
{
    public interface IDialog
    {
        void Show();
        void Hide();
    }

    public class Dialog : UIParts, IDialog
    {
        [SerializeField]
        Window window = null;

        [SerializeField]
        UGUIImage panel = null;

        [SerializeField]
        bool activeOnAwake = false;

        [SerializeField]
        float showAnimationTime = 0.3f;
        [SerializeField]
        float hideAnimationTime = 0.5f;

        UIAnimation showAnimation;
        UIAnimation hideAnimation = UIAnimationUtil.DefaultHideAnimation;

        public MyUnityEvent OnShowAnimationEnd = new MyUnityEvent();
        public MyUnityEvent OnHideAnimationEnd = new MyUnityEvent();

        protected virtual void Awake()
        {
            panel.gameObject.SetActive(activeOnAwake);
            window.gameObject.SetActive(activeOnAwake);

            Vector3 startScale = Vector3.one * 0.3f;
            Vector3 endScale = Vector3.one;

            showAnimation = new UIAnimation((parts, t) =>
            {
                parts.transform.localScale = Vector3.LerpUnclamped(startScale, endScale, Easing.OutBack(t, 3.0f));
            });
        }

        protected virtual void Start() { }

        public virtual void Show()
        {
            panel.gameObject.SetActive(true);
            window.gameObject.SetActive(true);
            window.Alpha = 1;

            StartCoroutine(UIAnimationUtil.DefaultShowAnimation.GetAnimation(panel, showAnimationTime * 0.5f));

            StartCoroutine(showAnimation.GetAnimation(window, showAnimationTime).OnCompleted(() =>
            {
                OnHideAnimationEnd.Invoke();
            }));
        }
        public virtual void Hide()
        {
            StartCoroutine(hideAnimation.GetAnimation(panel, hideAnimationTime));

            StartCoroutine(hideAnimation.GetAnimation(window, hideAnimationTime).OnCompleted(() =>
            {
                panel.gameObject.SetActive(false);
                window.gameObject.SetActive(false);
                OnHideAnimationEnd.Invoke();
            }));
        }

        public override Color Color
        {
            get
            {
                return window.Color;
            }

            set
            {
                window.Color = value;
            }
        }
        public override float Alpha
        {
            get
            {
                return window.Alpha;
            }

            set
            {
                window.Alpha = value;
            }
        }
    }
}