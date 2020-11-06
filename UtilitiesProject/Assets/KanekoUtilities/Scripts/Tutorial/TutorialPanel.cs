using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace KanekoUtilities
{
    public abstract class TutorialPanel : Panel
    {
        protected enum FingerType
        {
            Idle, Hold, Down, Up, 
        }

        [SerializeField]
        protected RectTransform fingerRectTransform = null;

        [SerializeField]
        GameObject[] fingerImages = null;

        Coroutine tutorialAnimationCoroutine;

        void Start()
        {
            StartCoroutine(TutorialAnimation());
        }

        public override void Activate()
        {
            base.Activate();
            StartCoroutine(TutorialAnimation());
        }

        protected abstract IEnumerator TutorialAnimation();

        protected void ChangeImage(FingerType type)
        {
            foreach (var f in fingerImages) f.SetActive(false);
            fingerImages[(int)type].SetActive(true);
        }
    }
}
