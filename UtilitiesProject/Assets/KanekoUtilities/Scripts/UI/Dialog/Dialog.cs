using System;
using UnityEngine;

namespace KanekoUtilities
{
    public interface IDialog
    {
        void Show();
        void Hide();
    }

    public class Dialog : Window, IDialog
    {
        [SerializeField]
        float showAnimationTime = 0.5f;
        [SerializeField]
        float hideAnimationTime = 0.2f;

        UIAnimation showAnimation;
        UIAnimation hideAnimation = UIAnimationUtil.DefaultHideAnimation;

        public MyUnityEvent OnShowAnimationEnd = new MyUnityEvent();
        public MyUnityEvent OnHideAnimationEnd = new MyUnityEvent();

        protected virtual void Awake()
        {
            Container.gameObject.SetActive(false);
            Vector3 startScale = Vector3.one * 0.5f;
            Vector3 endScale = Vector3.one;

            showAnimation = new UIAnimation((parts, t) =>
            {
                parts.transform.localScale = Vector3.LerpUnclamped(startScale, endScale, Easing.OutBack(t, 3.0f));
            });
        }

        protected virtual void Start() { }

        public virtual void Show()
        {
            Container.gameObject.SetActive(true);
            Alpha = 1;
            
            StartCoroutine(showAnimation.GetAnimation(this, showAnimationTime).OnCompleted(() =>
            {
                OnShowAnimationEnd.Invoke();
            }));
        }
        public virtual void Hide()
        {
            StartCoroutine(hideAnimation.GetAnimation(this, hideAnimationTime).OnCompleted(() =>
            {
                Container.gameObject.SetActive(false);
                OnHideAnimationEnd.Invoke();
            }));
        }
    }
}