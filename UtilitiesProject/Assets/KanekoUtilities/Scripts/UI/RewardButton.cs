using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public class RewardButton : UGUIButton
    {
        [SerializeField]
        string key = "Reward";

        [SerializeField]
        GameObject loadingImage = null;

        [SerializeField]
        GameObject movieImage = null;
        
        public MyUnityEvent OnFailed = new MyUnityEvent();

        AnimationCurve scaleCurve;
        Coroutine notificationCoroutine;

        bool isShowRewardVideo;

        protected override void Awake()
        {
            Interactable = Button.interactable;

            button.onClick.AddListener(()=>
            {
                if (isShowRewardVideo) return;
                isShowRewardVideo = true;
                MyAdManager.Instance.ShowRewardVideo(key, () =>
                {
                    isShowRewardVideo = false;
                    if(OnClickEvent != null) OnClickEvent.Invoke();
                }, () =>
                {
                    isShowRewardVideo = false;
                    OnFailed.SafeInvoke();
                });
            });

            scaleCurve = MyAnimationCurves.Instance.GetAnimationCurve(MyAnimationCurveType.Notification);
        }

        void OnEnable()
        {
            Refresh();
        }

        public void Refresh()
        {
            if (!MyAdManager.Instance.IsRewardVideoLoaded)
            {
                SetInteractable(false);
                StartCoroutine(InteractableControl());
            }
            else
            {
                SetInteractable(true);
            }
        }

        IEnumerator InteractableControl()
        {
            MyAdManager adManager = MyAdManager.Instance;
            SetInteractable(false);

            yield return new WaitUntil(() => adManager.IsRewardVideoLoaded);
            SetInteractable(true);
        }

        void SetInteractable(bool interactable)
        {
            loadingImage.SetActive(!interactable);
            movieImage.SetActive(interactable);
            Button.interactable = interactable;

            if (interactable)
            {
                notificationCoroutine = StartCoroutine(NotificationAnimation());
            }
            else
            {
                if (notificationCoroutine != null) StopCoroutine(notificationCoroutine);
            }
        }

        IEnumerator NotificationAnimation()
        {
            var wait = new WaitForSeconds(0.75f);
            while (true)
            {
                yield return KKUtilities.FloatLerp(2.0f, (t) =>
                {
                    transform.localScale = Vector3.LerpUnclamped(Vector3.one, Vector3.one * 1.2f, scaleCurve.Evaluate(t));
                });

                yield return wait;
            }
        }
    }
}
